using Application.Common;
using Domain.Entity;
using Domain.Entity.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Reflection;

namespace Domain.InfraSqlServer.Persistense
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public DbSet<CategoryEntity> Categories { get; set; }
        public DbSet<ProductEntity> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }

        public DatabaseFacade Database
        {
            get { return base.Database; }
            set { }
        }


        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.DateAdded = DateTime.UtcNow;
                        entry.Entity.DateUpdated = DateTime.UtcNow;
                        break;
                    case EntityState.Modified:
                        entry.Entity.DateUpdated = DateTime.UtcNow;
                        break;
                }
            }

            var result = await base.SaveChangesAsync(cancellationToken);

            return result;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }
        public DbSet<T> GetDbSet<T>() where T : class
        {
            return this.Set<T>();
        }
    }
}
