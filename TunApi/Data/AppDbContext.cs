using Microsoft.EntityFrameworkCore;
using TunApi.Models;

namespace TunApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Todo> Todo { get; set; }
        public DbSet<TodoFile> TodoFiles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Todo>()
                .HasMany(t => t.TodoFiles)
                .WithOne(tf => tf.Todo)
                .HasForeignKey(tf => tf.TodoId);
        }
    }
}