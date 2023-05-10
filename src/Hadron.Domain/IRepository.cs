namespace Pictagram.Models.Helpers;

internal interface IRepository<TEntity, TId> {
    Task<TEntity?> FindAsync(TId EntityId, CancellationToken cancellationToken = default);
    Task AddAsync(TEntity Entity, CancellationToken cancellationToken = default);
    Task SaveAsync(TEntity Entity, CancellationToken cancellationToken = default);
    Task RemoveAsync(TId EntityId, CancellationToken cancellationToken = default);
}