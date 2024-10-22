using Domain.Entity.Database;

namespace Domain.Entity.Repositories;

public interface ICategoryRepository : IRepository<CategoryEntity>
{
    Task<CategoryEntity> GetByName (string name, CancellationToken cancellationToken);
}
