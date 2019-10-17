namespace Searcher.BLL.Interfaces
{
    public interface IFactory<T, Q>
    {
        T Create(Q input);
    }
}
