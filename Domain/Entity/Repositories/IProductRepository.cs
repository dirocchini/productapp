using Domain.Entity.Database;

namespace Domain.Entity.Repositories;

public interface IProductRepository : IRepository<ProductEntity>
{
    Task<List<ProductEntity>> GetAllWithCategoriesAsync(CancellationToken cancellationToken);
    Task<ProductEntity> GetByName(string name, CancellationToken cancellationToken);
    Task<ProductEntity> GetByIdWithCategories(int id, CancellationToken cancellationToken);
}
