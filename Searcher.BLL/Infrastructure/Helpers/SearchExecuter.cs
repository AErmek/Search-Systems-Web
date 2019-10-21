using Searcher.BLL.DTO;
using Searcher.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Searcher.BLL.Infrastructure.Helpers
{
    public class SearchExecuter
    {
        private AutoResetEvent[] autoResetEvents;
        private readonly List<ISearchSystem> baseSearchSystems;

        public SearchExecuter(List<ISearchSystem> baseSearchSystems)
        {
            this.baseSearchSystems = baseSearchSystems;
            autoResetEvents = new AutoResetEvent[baseSearchSystems.Count];
            for (int i = 0; i < baseSearchSystems.Count; i++)
            {
                autoResetEvents[i] = new AutoResetEvent(false);
            }
        }

        public List<SearchResultDto> ExecuteSearch(string keyWord)
        {
            for (int i = 0; i < baseSearchSystems.Count; i++)
            {
                ThreadPool.QueueUserWorkItem(
                    new WaitCallback(baseSearchSystems[i].Find),
                    new State(autoResetEvents[i], keyWord));
            }

            int index = WaitHandle.WaitAny(autoResetEvents, 20000, false);

            if (index == WaitHandle.WaitTimeout)
            {
                throw new TimeoutException();
            }

            baseSearchSystems[index].CheckForException();
            return baseSearchSystems[index].DeserializeResult();
        }
    }
}
