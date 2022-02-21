using Microsoft.EntityFrameworkCore;
using TodoApp.Models.Entities;

namespace TodoApp.Repositories
{
    public class InMemoryDbContext : DbContext
    {
        public InMemoryDbContext(DbContextOptions<InMemoryDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder
                .Entity<Todo>()
                .Property(e => e.Id)
                .ValueGeneratedOnAdd();
        }

        public DbSet<Todo>? TodoList { get; set; }
    }
}
