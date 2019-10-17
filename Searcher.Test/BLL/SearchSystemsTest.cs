using Microsoft.VisualStudio.TestTools.UnitTesting;
using Searcher.BLL.DTO;
using Searcher.BLL.DTO.Helpers;
using Searcher.BLL.DTO.SearchSystems;
using Searcher.BLL.Infrastructure;
using System.Collections;
using System.Collections.Generic;

namespace Searcher.Test.BLL
{
    public class SearchSystemsComparer : IComparer
    {
        public int Compare(object x, object y)
        {
            var first = (BaseSearchSystem)x;
            var second = (BaseSearchSystem)y;
            return first.AccessKey.CompareTo(second.AccessKey);
        }
    }

    [TestClass]
    public class SearchSystemsTest
    {
        ApiKeyOptions keyOptions;
        [TestInitialize]
        public void SetUp()
        {
            keyOptions = new ApiKeyOptions
            {
                BingKey = "bingKey",
                GoogleKey = "googleKey",
                YandexKey = "yandexKey",
                YandexUser = "ermek"
            };
        }

        [TestMethod]
        public void Test_GetSearchSystems()
        {
            SearchSystemsHelper searchSystemsHelper = new SearchSystemsHelper(new SearchSystemFactory());

            var searchSystems = searchSystemsHelper.GetSearchSystems(keyOptions);

            var manualSearchSystems = new List<BaseSearchSystem>
            {
                new YandexSearchSystem(keyOptions),
                new GoogleSearchSystem(keyOptions),
                new BingSearchSystem(keyOptions)
            };

            CollectionAssert.AreEqual(manualSearchSystems, searchSystems, new SearchSystemsComparer());
        }
    }
}
