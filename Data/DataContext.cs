using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Threading.Tasks;
using Data.Models;

namespace Data
{
    internal class DataContext : DbContext
    {
        public DataContext()
        {
        }

        public DataContext(string connectionString) : base(connectionString)
        {
        }

        public DbSet<Product> Product { get; set; }

        public DbSet<ProductOption> ProductOption { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

        public override int SaveChanges()
        {
            UpdateCommonProperties();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync()
        {
            UpdateCommonProperties();
            return base.SaveChangesAsync();
        }

        private void UpdateCommonProperties()
        {
            var addedEntities = ChangeTracker.Entries().Where(x => x.State == EntityState.Added || x.State == EntityState.Modified);

            var currentTime = DateTime.Now;
            foreach (var addedEntity in addedEntities)
            {
                if (addedEntity.State == EntityState.Added)
                {
                    ((BaseEntity)addedEntity.Entity).CreatedOn = currentTime;
                }
                ((BaseEntity)addedEntity.Entity).ModifiedOn = currentTime;
            }
        }
    }
}