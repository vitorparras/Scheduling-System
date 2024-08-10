using Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class DxContext : DbContext
    {
        public DxContext(DbContextOptions<DxContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                  .UseInMemoryDatabase("Teste")
                  .EnableSensitiveDataLogging();
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
        }

        #region Sistema
        public DbSet<TokenHistory> TokenHistories { get; set; }

        public DbSet<User> Users { get; set; }

        #endregion
    }
}
