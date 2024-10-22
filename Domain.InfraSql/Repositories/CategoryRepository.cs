using Application.Common;
using Domain.Entity.Database;
using Domain.Entity.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Domain.InfraSqlServer.Repositories;

public class CategoryRepository : Repository<CategoryEntity>, ICategoryRepository
{
    public CategoryRepository(IApplicationDbContext context) : base(context)
    {
    }

    public async Task<CategoryEntity> GetByName(string name, CancellationToken cancellationToken)
    {
        return await _context.Categories.FirstOrDefaultAsync(c => c.Name.Equals(name), cancellationToken);
    }
}