using System.Threading;

namespace Searcher.BLL.DTO
{
    public class State
    {
        public AutoResetEvent autoResetEvent;
        public string KeyWord;

        public State(AutoResetEvent autoResetEvent, string keyWord)
        {
            this.autoResetEvent = autoResetEvent;
            KeyWord = keyWord;
        }
    }
}
