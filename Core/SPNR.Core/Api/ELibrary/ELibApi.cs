using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using AngleSharp.Dom;
using AngleSharp.Html.Parser;
using Newtonsoft.Json;
using SPNR.Core.Api.ELibrary.Credentials;
using SPNR.Core.Models;
using SPNR.Core.Models.Works;
using SPNR.Core.Models.Works.Fields;
using SPNR.Core.Models.Works.PublishData;

namespace SPNR.Core.Api.ELibrary
{
    public class ELibApi
    {
        private Uri _baseAddress = new("https://www.elibrary.ru/");
        private readonly HttpClient _httpClient;
        private readonly HtmlParser _parser = new();

        public readonly CookieContainer CookieContainer = new();

        public int RequestCooldown { get; set; } = 1000;
        private DateTime _lastRequestTime;

        public ELibApi()
        {
            var httpClientHandler = new HttpClientHandler
            {
                CookieContainer = CookieContainer
            };

            _httpClient = new HttpClient(httpClientHandler)
            {
                //BaseAddress = _baseAddress
            };

            _httpClient.DefaultRequestHeaders.UserAgent.ParseAdd(
                "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/88.0.4324.104 Safari/537.36");
        }

        public bool CheckCooldown()
        {
            var msFromLastRequest = (int) (DateTime.Now - _lastRequestTime).TotalMilliseconds;

            if (_lastRequestTime == default || msFromLastRequest >= RequestCooldown)
            {
                _lastRequestTime = DateTime.Now;
                return true;
            }
            
            return false;
        }

        public ApiAnswer Authorize(ELibCredentialsType credentialsType, string cred1, string cred2)
        {
            CheckCooldown();
            // if (!CheckCooldown())
            //     return new ApiAnswer
            //     {
            //         Exception = new Exception("You can't do request yet")
            //     };

            try
            {
                var initReq = _httpClient.GetAsync(_baseAddress).Result;

                if (initReq.RequestMessage == null)
                {
                    Console.WriteLine("null 1");
                    return new ApiAnswer();
                }
                
                _baseAddress = new Uri((initReq.RequestMessage.RequestUri ?? _baseAddress).GetLeftPart(UriPartial.Authority));
                
                Console.WriteLine(_baseAddress);
                
                if (credentialsType == ELibCredentialsType.Cookies)
                {
                    CookieContainer.Add(_baseAddress, new CookieCollection
                    {
                        new Cookie("SUserID", cred1),
                        new Cookie("SCookieID", cred2)
                    });
            
                    // TODO check if cookies are still valid
            
                    return new ApiAnswer();
                }
                
                var authContent = new FormUrlEncodedContent(new Dictionary<string, string>
                {
                    {"login", cred1},
                    {"password", cred2}
                });

                _httpClient.PostAsync(_baseAddress + "start_session.asp", authContent).Wait();

                foreach (Cookie cookie in CookieContainer.GetCookies(_baseAddress))
                    Console.WriteLine(cookie.ToString());

                return new ApiAnswer();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return new ApiAnswer();
        }

        public async Task<ApiAnswer<List<int>>> GetWorkIds(SearchInfo searchInfo, int pageId = 1)
        {
            CheckCooldown();
            
            var answer = new ApiAnswer<List<int>>
            {
                Data = new List<int>()
            };

            var sData = new Dictionary<string, string>
            {
                {"querybox_name", ""},
                {"authors_all", $"{string.Join('|', searchInfo.Authors)}|"},
                {"titles_all", ""},
                {"rubrics_all", ""},
                {"changed", "1"},
                {"queryid", ""},
                {"ftext", ""},
                {"where_name", "on"},
                {"where_affiliation", "on"},
                {"where_abstract", "on"},
                {"where_references", "on"},
                {"where_keywords", "on"},
                {"where_fulltext", "on"},
                {"type_article", "on"},
                {"type_disser", "on"},
                {"type_book", "on"},
                {"type_report", "on"},
                {"type_conf", "on"},
                {"type_patent", "on"},
                {"type_preprint", "on"},
                {"search_itemboxid", ""},
                {"search_morph", "on"},
                {"begin_year", "0"},
                {"end_year", "0"},
                {"issues", "all"},
                {"orderby", "rank"},
                {"order", "rev"},
                {"queryboxid", "0"},
                {"save_queryboxid", "0"}
            };

            if (searchInfo.DateFrom != default && searchInfo.DateTo != default &&
                searchInfo.DateTo.Date != DateTime.Now.Date)
                sData["issues"] = $"d{Math.Round((searchInfo.DateTo - searchInfo.DateFrom).TotalDays)}";

            if (searchInfo.DateFrom != default) sData["begin_year"] = searchInfo.DateFrom.Year.ToString();

            if (searchInfo.DateTo != default) sData["end_year"] = searchInfo.DateTo.Year.ToString();

            var content = new FormUrlEncodedContent(sData);
            var response = await _httpClient.PostAsync(_baseAddress + $"query_results.asp?pagenum={pageId}", content);
            answer.StatusCode = response.StatusCode;

            var doc = _parser.ParseDocument(await response.Content.ReadAsStringAsync());

            Console.WriteLine(response.StatusCode);
            Console.WriteLine(response.RequestMessage.RequestUri);
            // Console.WriteLine(content.ReadAsStringAsync().Result);
            Console.WriteLine(doc.Body.InnerHtml);
            
            var table = doc.QuerySelectorAll("#restab > tbody")[0].Children;

            foreach (var tableChild in table)
            {
                if (tableChild.Id == null)
                    continue;

                var id = tableChild.Id.Remove(0, 1);

                answer.Data.Add(int.Parse(id));
            }

            return answer;
        }

        public async Task<ApiAnswer<ScientificWork>> GetWorkInfo(int id)
        {
            CheckCooldown();
            
            var answer = new ApiAnswer<ScientificWork>
            {
                Data = new ScientificWork
                {
                    PublicationInfo = new Dictionary<string, string>(),
                    PublicationMeta = new Dictionary<string, List<string>>(),
                    ELibInfo = new ELibInfo
                    {
                        ELibWorkId = id
                    }
                }
            };

            var response = await _httpClient.GetAsync(_baseAddress + $"item.asp?id={id}");
            var doc = await _parser.ParseDocumentAsync(await response.Content.ReadAsStringAsync());
            
            var publishInfo =
                doc.QuerySelector(
                    "body > table > tbody > tr > td > table:nth-child(1) > tbody > tr > td:nth-child(2) > table > tbody > tr:nth-child(4) > td:nth-child(1)");
            
            answer.Data.WorkName = publishInfo.QuerySelector(
                    "table:nth-child(2) > tbody > tr > td:nth-child(2) > span > b > p")
                .InnerHtml;
            
            // Collect author information
            answer.Data.AuthorNames = publishInfo.QuerySelector("div > table:nth-child(2) > tbody > tr > td:nth-child(2)")
                .QuerySelectorAll("div")
                .Select(d => d.QuerySelector("b > font")?.Text())
                .Select(s => s?.Trim())
                .Where(s => !string.IsNullOrEmpty(s))
                .Select(s => Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(s.ToLower()))
                .ToList();
            
            // Add Publication Info
            var regex = new Regex(@"\>(.*?)\<");
            var pubInfoData = regex.Matches(publishInfo
                    .QuerySelector("div > table:nth-child(4) > tbody").InnerHtml
                    .Replace("\n", "")
                    .Replace("&nbsp;", " "))
                .Where(m => m.Groups.Values.All(g => !string.IsNullOrEmpty(g.Value.Trim())))
                .Select(m => m.Groups[1])
                .Select(g => g.Value)
                .ToList();
            
            for (var i = 0; i < pubInfoData.Count; i += 2)
            {
                if(pubInfoData[i].Contains(":"))
                    answer.Data.PublicationInfo.TryAdd(pubInfoData[i].Replace(": ", "").Trim(), pubInfoData[i + 1].Trim());
                else
                {
                    answer.Data.PublicationInfo.TryAdd(i.ToString(), pubInfoData[i].Trim());
                    i++;
                }
            }

            // Add Publication Meta
            var pubTables = publishInfo.QuerySelectorAll("div > table").ToList().Skip(2).ToList();

            for (var i = 0;
                !(pubTables[i].QuerySelector("tbody > tr:nth-child(1) > td > font").InnerHtml.ToLower().Contains("аннотация") ||
                  pubTables[i].QuerySelector("tbody > tr:nth-child(1) > td > font").InnerHtml.ToLower().Contains("ключевые слова") ||
                  pubTables[i].QuerySelector("tbody > tr:nth-child(1) > td > font").InnerHtml.ToLower().Contains("показатели"));
                i++)
            {
                var key = pubTables[i].QuerySelector("tbody > tr:nth-child(1) > td > font").InnerHtml.Replace(":", "");

                var metaArray = new Regex("<[^>]*>").Replace(
                    pubTables[i].QuerySelector("tbody > tr:nth-child(2) > td:nth-child(2)").InnerHtml
                        .Replace("\n", "")
                        .Replace("&nbsp;", " ")
                        .Replace("<br>", "\n"), "")
                    .Trim()
                    .Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);

                answer.Data.PublicationMeta.TryAdd(key, metaArray.ToList());
            }
            
            // Add ELib fields
            var metricsElement = pubTables.Where(t => t.QuerySelector("tbody > tr:nth-child(1) > td > font") != null)
                .FirstOrDefault(t => t.QuerySelector("tbody > tr:nth-child(1) > td > font").InnerHtml.Contains("БИБЛИОМЕТРИЧЕСКИЕ"));
            
            if(metricsElement == null)
                return answer;
            
            var metrics = metricsElement.QuerySelector("tbody > tr:nth-child(2) > td:nth-child(2) > table:nth-child(2) > tbody")
                .Text()
                .Trim()
                .Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None)
                .Select(s => s.Trim().Replace("\u00ae", ""))
                .Where(s => !string.IsNullOrEmpty(s))
                .ToDictionary(k => k.Split(":")[0], v => v.Split(":")[1].Trim());

            answer.Data.ELibInfo.RINC = metrics["Входит в РИНЦ"] == "да";
            answer.Data.ELibInfo.RINCCore = metrics["Входит в ядро РИНЦ"] == "да";
            answer.Data.ELibInfo.Scopus = metrics["Входит в Scopus"] == "да";
            answer.Data.ELibInfo.WebOfScience = metrics["Входит в Web of Science"] == "да";
            answer.Data.ELibInfo.RawMetrics = metrics;
            
            return answer;
        }
    }
}