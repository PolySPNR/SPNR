using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AngleSharp.Html.Parser;
using SPNR.Core.Api.ELibrary.Credentials;
using SPNR.Core.Models;
using SPNR.Core.Models.Works;
using SPNR.Core.Models.Works.PublishData;

namespace SPNR.Core.Api.ELibrary
{
    public class ELibApi
    {
        public int RequestCooldown { get; set; } = 500;
        
        public readonly CookieContainer CookieContainer = new();
        
        private readonly Uri _baseAddress = new("https://www.elibrary.ru");
        private readonly HtmlParser _parser = new();
        private readonly HttpClient _httpClient;

        private DateTime _lastRequestTime;
        
        public ELibApi()
        {
            var httpClientHandler = new HttpClientHandler()
            {
                CookieContainer = CookieContainer
            };

            _httpClient = new HttpClient(httpClientHandler)
            {
                BaseAddress = _baseAddress
            };
            
            _httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/88.0.4324.104 Safari/537.36");
        }

        private bool CheckCooldown()
        {
            var msFromLastRequest = (DateTime.Now - _lastRequestTime).Milliseconds;

            if (msFromLastRequest < RequestCooldown) 
                return false;
            
            _lastRequestTime = DateTime.Now;
            return true;
        }
        
        public async Task<ApiAnswer> Authorize(ELibCredentialsType credentialsType, string cred1, string cred2)
        {
            if (!CheckCooldown())
                return new ApiAnswer()
                {
                    Exception = new Exception("You can't do request yet")
                };
            
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
            
            await _httpClient.GetAsync("/");
            
            var authContent = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                {"login", cred1},
                {"password", cred2}
            });
            
            await _httpClient.PostAsync("/start_session.asp", authContent);
            
            foreach (Cookie cookie in CookieContainer.GetCookies(new Uri("https://www.elibrary.ru")))
            {
                Console.WriteLine(cookie.ToString());
            }

            return new ApiAnswer();
        }

        public async Task<ApiAnswer<List<int>>> GetWorkIds(SearchInfo searchInfo, int pageId = 1)
        {
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

            if (searchInfo.DateFrom != default && searchInfo.DateTo != default && searchInfo.DateTo.Date != DateTime.Now.Date)
            {
                sData["issues"] = $"d{Math.Round((searchInfo.DateTo - searchInfo.DateFrom).TotalDays)}";
            }
            
            if (searchInfo.DateFrom != default)
            {
                sData["begin_year"] = searchInfo.DateFrom.Year.ToString();
            }

            if (searchInfo.DateTo != default)
            {
                sData["end_year"] = searchInfo.DateTo.Year.ToString();
            }
            
            var content = new FormUrlEncodedContent(sData);
            var response = await _httpClient.PostAsync($"/query_results.asp?pagenum={pageId}", content);
            answer.StatusCode = response.StatusCode;
            
            var doc = _parser.ParseDocument(await response.Content.ReadAsStringAsync());
            
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
            var answer = new ApiAnswer<ScientificWork>
            {
                Data = new ScientificWork()
            };

            var response = await _httpClient.GetAsync($"/item.asp?id={id}");
            var doc = _parser.ParseDocument(await response.Content.ReadAsStringAsync());

            var publishInfo =
                doc.QuerySelector(
                    "body > table > tbody > tr > td > table:nth-child(1) > tbody > tr > td:nth-child(2) > table > tbody > tr:nth-child(4) > td:nth-child(1)");
            
            answer.Data.WorkName = publishInfo.QuerySelector(
                "table:nth-child(2) > tbody > tr > td:nth-child(2) > span > b > p")
                .InnerHtml;

            if (publishInfo.QuerySelector(
                    "div > table:nth-child(4) > tbody > tr:nth-child(1) > td > font:nth-child(1)")
                .InnerHtml.Contains("журн")
            )
            {
                answer.Data.PublishType = PublishType.Journal;
                answer.Data.JournalPublish = new JournalPublish()
                {
                    Name = publishInfo.QuerySelector("div > table:nth-child(6) > tbody > tr:nth-child(2) > td:nth-child(2) > a").InnerHtml,
                    Number = publishInfo.QuerySelector("div > table:nth-child(4) > tbody > tr:nth-child(3) > td > a").InnerHtml,
                    Year = int.Parse(publishInfo.QuerySelector("div > table:nth-child(4) > tbody > tr:nth-child(3) > td > font").InnerHtml),
                    ISSN = publishInfo.QuerySelector("div > table:nth-child(6) > tbody > tr:nth-child(2) > td:nth-child(2) > font").InnerHtml
                };

                var pages =
                    (from pageStr in publishInfo
                            .QuerySelector("div > table:nth-child(4) > tbody > tr:nth-child(3) > td > div > font")
                            .InnerHtml
                            .Split('-')
                        select int.Parse(pageStr)).ToList();

                switch (pages.Count)
                {
                    case < 2:
                        answer.Data.JournalPublish.StartPage = pages[0];
                        answer.Data.JournalPublish.EndPage = pages[0];
                        break;
                    case >= 2:
                        answer.Data.JournalPublish.StartPage = pages[0];
                        answer.Data.JournalPublish.EndPage = pages[1];
                        break;
                }

            }
            
            return answer;
        }
    }
}