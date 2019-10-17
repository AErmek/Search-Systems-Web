using Newtonsoft.Json;
using Searcher.BLL.Enums;
using Searcher.BLL.Infrastructure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace Searcher.BLL.DTO.SearchSystems
{
    public class GoogleSearchSystem : BaseSearchSystem
    {
        const string uriBase = "https://www.googleapis.com/customsearch/v1";
        private readonly string _cx;

        public GoogleSearchSystem() { }

        public GoogleSearchSystem(ApiKeyOptions apiKeyOptions)
        {
            AccessKey = apiKeyOptions.GoogleKey;
            _cx = apiKeyOptions.GoogleCx;
        }

        public override List<SearchResultDto> DeserializeResult()
        {
            dynamic jsonData = JsonConvert.DeserializeObject(TextResult);
            var results = new List<SearchResultDto>();

            foreach (var item in jsonData.items)
            {
                results.Add(new SearchResultDto()
                {
                    BrowserType = SearchSystemType.Google,
                    DateTime = DateTime.Now,
                    Url = item.link,
                    Name = item.title,
                    Snippet = item.snippet
                });
            }
            return results;
        }

        public override void SearchByKeyWord(string keyWord)
        {
            //Thread.Sleep(1000);
            var url = uriBase + @"?key={0}&cx={1}&q={2}";
            var completeUrl = string.Format(url, AccessKey, _cx, keyWord);

            WebRequest request = WebRequest.Create(completeUrl);
            HttpWebResponse response = (HttpWebResponse)request.GetResponseAsync().Result;
            using (var streamReader = new StreamReader(response.GetResponseStream()))
            {
                TextResult = streamReader.ReadToEnd();
            }
        }
    }
}
