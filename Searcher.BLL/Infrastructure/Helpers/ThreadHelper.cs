using Searcher.BLL.DTO.SearchSystems;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Searcher.BLL.DTO.Helpers
{
    public class ThreadHelper
    {
        private AutoResetEvent[] autoResetEvents;
        private readonly List<BaseSearchSystem> baseSearchSystems;

        public ThreadHelper(List<BaseSearchSystem> baseSearchSystems)
        {
            this.baseSearchSystems = baseSearchSystems;
            autoResetEvents = new AutoResetEvent[baseSearchSystems.Count];
            for (int i = 0; i < baseSearchSystems.Count; i++)
            {
                autoResetEvents[i] = new AutoResetEvent(false);
            }
        }

        public List<SearchResultDto> FindAny(string keyWord)
        {
            for (int i = 0; i < baseSearchSystems.Count; i++)
            {
                ThreadPool.QueueUserWorkItem(
                    new WaitCallback(baseSearchSystems[i].Find),
                    new State(autoResetEvents[i], keyWord));
            }

            //15 seconds to Timeout
            int index = WaitHandle.WaitAny(autoResetEvents, 15000, false);

            if (index == WaitHandle.WaitTimeout)
            {
                throw new TimeoutException();
            }
            if (baseSearchSystems[index].Exception != null)
            {
                throw baseSearchSystems[index].Exception;
            }
            return baseSearchSystems[index].DeserializeResult();
        }
    }
}
