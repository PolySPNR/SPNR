using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using AngleSharp.Html.Parser;
using ER.Shared.Container;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SPNR.Core.Api.ELibrary;
using SPNR.Core.Api.ELibrary.Credentials;
using SPNR.Core.Models;

namespace SPNR.CLI
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            LoadEnvironment();

            // var api = new ELibApi();
            //
            // api.Authorize(ELibCredentialsType.LogPassPair, "WGOS", "0puWGQunjNDNP3Xt").Wait();
            //
            // var answer = api.GetWorkIds(new SearchInfo()
            // {
            //     Authors = new List<string>
            //     {
            //         "Пителинский К В", "Бутакова Н Г"
            //     }
            // }).Result;
            //
            // for (var i = 0; i < 15; i++)
            // {
            //     var work = api.GetWorkInfo(answer.Data[i]).Result.Data;
            //     
            //     Console.WriteLine(JsonConvert.SerializeObject(work, Formatting.Indented));
            //     
            //     Thread.Sleep(api.RequestCooldown);
            // }
            

            //ParseElib().Wait();
            
            var appContainer = new AppContainer<Startup, EntryPoint>();
            appContainer.Initialize();
            appContainer.Run();
        }

        private static async Task<bool> ParseElib()
        {
            var baseAddress = new Uri("https://elibrary.ru");
            var cookieContainer = new CookieContainer();
            var httpClientHandler = new HttpClientHandler()
            {
                CookieContainer = cookieContainer
            };
            var httpClient = new HttpClient(httpClientHandler)
            {
                BaseAddress = baseAddress
            };
            
            cookieContainer.Add(baseAddress, new CookieCollection()
            {
                new Cookie("SUserID", "3846870"),
                new Cookie("SCookieID", "1028109351")
            });
            
            var data = new Dictionary<string, string>()
            {
                {"querybox_name", ""},
                {"authors_all", "Бутакова Н Г|"},
                {"titles_all", ""},
                {"rubrics_all", ""},
                {"changed", "0"},
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
                {"authors", "Бутакова Н Г"},
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
            
            var content = new FormUrlEncodedContent(data);
            var resp = await httpClient.PostAsync("/query_results.asp", content);
            var parser = new HtmlParser();
            var doc = parser.ParseDocument(await resp.Content.ReadAsStringAsync());

            Console.WriteLine(doc.Body.InnerHtml);
            
            var table = doc.QuerySelectorAll("#restab > tbody")[0].Children;

            foreach (var tableChild in table)
            {
                if (tableChild.Id == null)
                    continue;

                var id = tableChild.Id.Remove(0, 1);
                var name = tableChild.QuerySelector("td:nth-child(2) > a > b > span").InnerHtml;

                //var workDescResp = await httpClient.GetAsync($"/item.asp?id={id}");
                //var workDescDoc = parser.ParseDocument(await workDescResp.Content.ReadAsStringAsync());
                
                Console.WriteLine($"ID: {id} | Date: | Name: {name}");
                
                
            }
            
            
            return true;
        }
        
        private static void LoadEnvironment()
        {
            if(!File.Exists(".env"))
                return;

            foreach (var envVar in File.ReadAllLines(".env"))
            {
                var varPair = envVar.Split("=");
                
                if(varPair.Length < 2)
                    continue;
                
                Environment.SetEnvironmentVariable(varPair[0], varPair[1]);
            }
        }
    }
}