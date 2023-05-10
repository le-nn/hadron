namespace Pictagram.Models.Helpers;

public interface IQueryService<TEntity, TSearchOption> {
    Task<SearchResult<TEntity>> SearchAsync(int offset, int fetch, TSearchOption option);
}