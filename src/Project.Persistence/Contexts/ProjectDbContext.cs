using Microsoft.EntityFrameworkCore;
using Project.Domain.Entities;

namespace Project.Persistence.Contexts
{
    public class ProjectDbContext : DbContext
    {
        public ProjectDbContext(DbContextOptions<ProjectDbContext> dbContext) : base (dbContext) { }

        public DbSet<Product> Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProjectDbContext).Assembly);
        }

        public string HasPrimaryKey<TEntity>(TEntity entity)
        {
            var entityType = Model.FindEntityType(typeof(TEntity));
            var primaryKey = entityType?.FindPrimaryKey();

            if (primaryKey is null) return string.Empty;

            var keyProperty = primaryKey.Properties.First();
            var keyValue = typeof(TEntity).GetProperty(keyProperty.Name)?.GetValue(entity);

            return keyValue?.ToString() ?? string.Empty;
        }
    }
}
