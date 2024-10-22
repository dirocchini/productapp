using Application.Common;
using Domain.Entity.Repositories;
using Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace Domain.InfraSqlServer.Repositories;


public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
{
    protected readonly IApplicationDbContext _context;

    protected Repository(IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task AddAsync(TEntity obj)
    {
        await _context.GetDbSet<TEntity>().AddAsync(obj);
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _context.GetDbSet<TEntity>().ToListAsync();
    }

    public async Task<TEntity> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await _context.GetDbSet<TEntity>().FindAsync(id);
    }

    public async Task RemoveAsync(TEntity obj)
    {
        _context.GetDbSet<TEntity>().Remove(obj);
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }
}