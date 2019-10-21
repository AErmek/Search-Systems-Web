using Microsoft.Extensions.Logging;
using Searcher.BLL.DTO;
using Searcher.BLL.Enums;
using Searcher.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Searcher.BLL.Infrastructure.SearchSystems
{
    public abstract class BaseSearchSystem : ISearchSystem
    {
        protected string AccessKey;
        private Exception Exception;
        protected string TextResult;
        protected SearchSystemType SearchSystemType;
        protected ILogger Logger;
        public abstract List<SearchResultDto> DeserializeResult();
        protected abstract void SearchByKeyWord(string keyWord);

        public void Find(object state)
        {
            State stateInfo = (State)state;
            try
            {
                SearchByKeyWord(stateInfo.KeyWord);
                stateInfo.autoResetEvent.Set();
            }
            catch (Exception ex)
            {
                Exception = ex;
                Logger.LogWarning($"{SearchSystemType} Exception in searching ({ex.Message})");
                //Sleep here to give chance to another search systems find something
                Thread.Sleep(10000);
                stateInfo.autoResetEvent.Set();
            }
        }

        public void CheckForException()
        {
            if (Exception != null) throw Exception;
        }
    }
}
