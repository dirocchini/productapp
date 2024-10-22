using Domain.Entity.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Application.Common
{
    public interface IApplicationDbContext
    {
        DbSet<CategoryEntity> Categories { get; set; }
        DbSet<ProductEntity> Products { get; set; }
        DbSet<ProductCategory> ProductCategories { get; set; }


        DbSet<T> GetDbSet<T>() where T : class;
        DatabaseFacade Database { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
