using Application.Common;
using Domain.Entity.Database;
using Domain.Entity.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Domain.InfraSqlServer.Repositories;

public class ProductRepository : Repository<ProductEntity>, IProductRepository
{
    public ProductRepository(IApplicationDbContext context) : base(context)
    {
    }

    public async Task<List<ProductEntity>> GetAllWithCategoriesAsync(CancellationToken cancellationToken)
    {
        return await _context.Products
            .Include(c => c.ProductCategories).ThenInclude(c => c.Category)
            .ToListAsync(cancellationToken);
    }

    public async Task<ProductEntity> GetByIdWithCategories(int id, CancellationToken cancellationToken)
    {
        return await _context.Products
            .Include(c => c.ProductCategories)
            .ThenInclude(c => c.Category)
            .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
    }

    public async Task<ProductEntity> GetByName(string name, CancellationToken cancellationToken)
    {
        return await _context.Products.FirstOrDefaultAsync(c => c.Name.Equals(name), cancellationToken);
    }
}