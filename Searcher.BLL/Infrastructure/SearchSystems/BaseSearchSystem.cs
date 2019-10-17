using System;
using System.Collections.Generic;
using System.Threading;

namespace Searcher.BLL.DTO.SearchSystems
{
    public abstract class BaseSearchSystem
    {
        public string AccessKey { get; protected set; }
        public Exception Exception { get; protected set; }
        public string TextResult { get; set; }

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
                //Sleep here to give chance to another search systems find something
                Thread.Sleep(10000);
                stateInfo.autoResetEvent.Set();
            }
        }
        public abstract void SearchByKeyWord(string keyWord);
        public abstract List<SearchResultDto> DeserializeResult();
    }
}
