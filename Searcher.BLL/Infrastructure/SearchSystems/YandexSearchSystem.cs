using Searcher.BLL.Enums;
using Searcher.BLL.Infrastructure;
using Searcher.BLL.Infrastructure.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;

namespace Searcher.BLL.DTO.SearchSystems
{
    public class YandexSearchSystem : BaseSearchSystem
    {
        const string uriBase = "https://yandex.com/search/xml";
        private readonly string _user;

        public YandexSearchSystem() { }

        public YandexSearchSystem(ApiKeyOptions apiKeyOptions)
        {
            _user = apiKeyOptions.YandexUser;
            AccessKey = apiKeyOptions.YandexKey;
        }

        public override List<SearchResultDto> DeserializeResult()
        {
            var yandexSearch = XmlSerializationUtil.Deserialize<YandexSearch>(TextResult);

            var results = new List<SearchResultDto>();

            foreach (var item in yandexSearch.Response.Results.Group)
            {
                var res = item.Doc;
                results.Add(new SearchResultDto()
                {
                    BrowserType = SearchSystemType.Yandex,
                    DateTime = DateTime.Now,
                    Url = res.Url,
                    Name = res.Title?.InnerText,
                    Snippet = res.Passage == null ? "-" : string.Join("\n", res.Passage.Select(x => x.InnerText))
                });
            }
            return results;
        }

        public override void SearchByKeyWord(string keyWord)
        {
            //Thread.Sleep(1000);
            string url = uriBase + @"?user={0}&key={1}&query={2}&l10n=en&sortby=rlv&filter=strict&groupby=attr%3Dd.mode%3Ddeep.groups-on-page%3D10.docs-in-group%3D3";

            string completeUrl = string.Format(url, _user, AccessKey, keyWord);

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(completeUrl);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            string result = string.Empty;
            using (var streamReader = new StreamReader(response.GetResponseStream()))
            {
                result = streamReader.ReadToEnd();
            }
            //Clean <hlword> tags
            TextResult = Regex.Replace(result, "<.?hlword>", string.Empty);
        }
    }
}
