using Newtonsoft.Json;
using Searcher.BLL.Enums;
using Searcher.BLL.Infrastructure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace Searcher.BLL.DTO.SearchSystems
{
    public class BingSearchSystem : BaseSearchSystem
    {
        const string uriBase = "https://api.cognitive.microsoft.com/bing/v7.0/search";

        public BingSearchSystem() { }

        public BingSearchSystem(ApiKeyOptions apiKeyOptions)
        {
            AccessKey = apiKeyOptions.BingKey;
        }

        public override List<SearchResultDto> DeserializeResult()
        {
            dynamic jsonData = JsonConvert.DeserializeObject(TextResult);
            var results = new List<SearchResultDto>();

            foreach (var item in jsonData.webPages?.value)
            {
                results.Add(new SearchResultDto()
                {
                    BrowserType = SearchSystemType.Bing,
                    DateTime = DateTime.Now,
                    Url = item.url,
                    Name = item.name,
                    Snippet = item.snippet
                });
            }
            return results;
        }

        public override void SearchByKeyWord(string keyWord)
        {
            //Thread.Sleep(1000);
            var uriQuery = uriBase + "?q=" + Uri.EscapeDataString(keyWord);
            WebRequest request = WebRequest.Create(uriQuery);
            request.Headers["Ocp-Apim-Subscription-Key"] = AccessKey;
            HttpWebResponse response = (HttpWebResponse)request.GetResponseAsync().Result;
            using (var streamReader = new StreamReader(response.GetResponseStream()))
            {
                TextResult = streamReader.ReadToEnd();
            }
        }
    }
}
