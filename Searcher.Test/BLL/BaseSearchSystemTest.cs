using Microsoft.VisualStudio.TestTools.UnitTesting;
using Searcher.BLL.DTO;
using Searcher.BLL.Enums;
using Searcher.BLL.Infrastructure;
using Searcher.BLL.Infrastructure.Helpers;
using Searcher.BLL.Infrastructure.SearchSystems;
using Searcher.BLL.Interfaces;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

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
        ApiKeyOptions options;
        [TestInitialize]
        public void SetUp()
        {
            options = new ApiKeyOptions
            {
                YandexKey = "03.965152905:d22d4b2900f386d57c71e5cc744029e7",
                YandexUser = "ermek_akmatov@inbox.ru",
                GoogleKey = "AIzaSyB148E2gOgvQ-uDXrHPUdIncOY0mOQEJSs",
                GoogleCx = "004371770683300358078:ficqkfdm1xk"
            };
        }

        [TestMethod]
        public void Test_YandexSearch()
        {
            var searchSystems = new List<ISearchSystem> { new YandexSearchSystem(null, options.YandexKey, options.YandexUser) };
            SearchExecuter searchExecuter = new SearchExecuter(searchSystems);
            var searchResults = searchExecuter.ExecuteSearch("Pi").Take(1).ToList();

            var manualResult = new List<SearchResultDto>(){
                new SearchResultDto{
                BrowserType = SearchSystemType.Yandex,
                Name = "Пи (число) — Википедия",
                Url = "https://ru.wikipedia.org/wiki/%D0%9F%D0%B8_(%D1%87%D0%B8%D1%81%D0%BB%D0%BE)",
                Snippet = "π {\\displaystyle \\pi }. Десятичная.\nЧисло. π {\\displaystyle \\pi }.",
                DateTime = searchResults[0].DateTime
            }};

            CollectionAssert.AreEqual(manualResult, searchResults, new SearchResultComparer());
        }

        [TestMethod]
        public void Test_GoogleSearch()
        {
            var searchSystems = new List<ISearchSystem> { new GoogleSearchSystem(null, options.GoogleKey, options.GoogleCx) };
            SearchExecuter searchExecuter = new SearchExecuter(searchSystems);
            var searchResults = searchExecuter.ExecuteSearch("game").Take(1).ToList();

            var manualResult = new List<SearchResultDto>(){
                new SearchResultDto{
                BrowserType = SearchSystemType.Google,
                Name = "Connected games",
                Url = "https://www.google.com/appserve/mkt/p/AFnwnKUnXD1ji46geUqp7D9dO_-JdN-nkd-9-tW67x-ubTby1HUZpTyHG9o4tXFXHXLGbfH6wSWIt7VOmmtywryT8NAxxMXmx8Rn4q5-8ygMVFsqZsWrTZ7irqvsJ6ZnL31Ck4qfCHEFR92FfxnoEBgh4-U4jC7TwGNU2y1xfJd9UgrSxF1QUQeNw0jY",
                Snippet = "The next generation of multiplayer games and beyond with Unity and Google \nCloud.",
                DateTime = searchResults[0].DateTime
            }};

            CollectionAssert.AreEqual(manualResult, searchResults, new SearchResultComparer());
        }


        //Bing search was not tested , because trial version was finished
    }
}
