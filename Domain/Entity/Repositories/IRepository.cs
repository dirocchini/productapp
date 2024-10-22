namespace Domain.Entity.Repositories;

public interface IRepository<T> where T : BaseEntity
{
    Task<T> GetByIdAsync(int id, CancellationToken cancellationToken);

    Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken);

    Task AddAsync(T obj);

    Task RemoveAsync(T obj);

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
