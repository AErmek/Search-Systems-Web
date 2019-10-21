using Searcher.BLL.DTO;
using System.Collections.Generic;

namespace Searcher.BLL.Interfaces
{
    public interface ISearchSystem
    {
        void Find(object state);
        List<SearchResultDto> DeserializeResult();
        void CheckForException();
    }
}
