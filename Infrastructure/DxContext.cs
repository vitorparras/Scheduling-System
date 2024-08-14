using Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class DxContext : DbContext
    {
        public DxContext(DbContextOptions<DxContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = Guid.NewGuid(),
                    Email = "teste@teste.com",
                    CreatedAt = DateTime.Now,
                    Cep = "123",
                    CPF = "123",
                    Name = "teste",
                    Password = "123456",
                    Tel = "123",
                });
        }

        public DbSet<TokenHistory> TokenHistories { get; set; }

        public DbSet<User> Users { get; set; }

    }
}
