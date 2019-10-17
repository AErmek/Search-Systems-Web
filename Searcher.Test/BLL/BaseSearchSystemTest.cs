using Microsoft.VisualStudio.TestTools.UnitTesting;
using Searcher.BLL.DTO;
using Searcher.BLL.DTO.SearchSystems;
using Searcher.BLL.Enums;
using System.Collections;
using System.Collections.Generic;

namespace Searcher.Test.BLL
{
    public class SearchResultComparer : IComparer
    {
        public int Compare(object x, object y)
        {
            var first = (SearchResultDto)x;
            var second = (SearchResultDto)y;
            if (first.Name.CompareTo(second.Name) != 0)
            {
                first.Name.CompareTo(second.Name);
            }
            else if (first.Url.CompareTo(second.Url) != 0)
            {
                return first.Url.CompareTo(second.Url);
            }
            else if (first.Snippet.CompareTo(second.Snippet) != 0)
            {
                return first.Snippet.CompareTo(second.Snippet);
            }
            else if (first.BrowserType.CompareTo(second.BrowserType) != 0)
            {
                return first.BrowserType.CompareTo(second.BrowserType);
            }
            return 0;
        }
    }

    [TestClass]
    public class BaseSearchSystemTest
    {
        string textResultYandex;
        string textResultGoogle;
        string textResultBing;
        [TestInitialize]
        public void SetUp()
        {
            textResultYandex =
                "<?xml version=\"1.0\" encoding=\"utf - 8\"?>" +
                "<yandexsearch version=\"1.0\">" +
                "<response>" +
                "<results>" +
                "<grouping>" +
                "<group>" +
                "<doc>" +
                "<url>https://techcrunch.com/</url>" +
                "<title>Title</title>" +
                "<passages>" +
                "<passage>Passage</passage>" +
                "</passages>" +
                "</doc>" +
                "</group>" +
                "</grouping>" +
                "</results>" +
                "</response>" +
                "</yandexsearch>";

            textResultGoogle = "{\"items\":[{\"link\":\"http:...\",\"title\":\"Title\",\"snippet\":\"Snippet\"}]}";
            textResultBing = "{\"webPages\":{value:[{\"url\":\"http:...\",\"name\":\"Title\",\"snippet\":\"Snippet\"}]}}";
        }

        [TestMethod]
        public void Test_YandexDeserialize()
        {
            BaseSearchSystem searchSystem = new YandexSearchSystem() { TextResult = textResultYandex };
            var result = searchSystem.DeserializeResult();

            var manualResult = new List<SearchResultDto>(){
                new SearchResultDto{
                BrowserType = SearchSystemType.Yandex,
                Name = "Title",
                Url = "https://techcrunch.com/",
                Snippet = "Passage",
                DateTime = result[0].DateTime
            }};

            CollectionAssert.AreEqual(manualResult, result, new SearchResultComparer());
        }

        [TestMethod]
        public void Test_GoogleDeserialize()
        {
            BaseSearchSystem searchSystem = new GoogleSearchSystem() { TextResult = textResultGoogle };
            var result = searchSystem.DeserializeResult();

            var manualResult = new List<SearchResultDto>(){
                new SearchResultDto{
                BrowserType = SearchSystemType.Google,
                Name = "Title",
                Url = "http:...",
                Snippet = "Snippet",
                DateTime = result[0].DateTime
            }};

            CollectionAssert.AreEqual(manualResult, result, new SearchResultComparer());
        }

        [TestMethod]
        public void Test_BingDeserialize()
        {
            BaseSearchSystem searchSystem = new BingSearchSystem() { TextResult = textResultBing };
            var result = searchSystem.DeserializeResult();

            var manualResult = new List<SearchResultDto>(){
                new SearchResultDto{
                BrowserType = SearchSystemType.Bing,
                Name = "Title",
                Url = "http:...",
                Snippet = "Snippet",
                DateTime = result[0].DateTime
            }};

            CollectionAssert.AreEqual(manualResult, result, new SearchResultComparer());
        }
    }
}
