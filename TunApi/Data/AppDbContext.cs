using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Text;
using TunApi.Models;

namespace TunApi.Data
{
    public class AppDbContext : IdentityDbContext<IdentityUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Todo> Todo { get; set; }
        public DbSet<TodoFile> TodoFiles { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                foreach (var property in entity.GetProperties())
                {
                    var columnName = ToSnakeCase(property.Name);
                    property.SetColumnName(columnName);
                }
            }

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Todo>()
                .HasMany(t => t.TodoFiles)
                .WithOne(tf => tf.Todo)
                .HasForeignKey(tf => tf.TodoId);

            modelBuilder.Entity<Todo>()
                .Property(t => t.Status)
                .HasConversion<string>();
        }

        private string ToSnakeCase(string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            var sb = new StringBuilder();
            sb.Append(char.ToLower(input[0]));

            for (int i = 1; i < input.Length; i++)
            {
                var c = input[i];
                if (char.IsUpper(c))
                {
                    sb.Append('_');
                    sb.Append(char.ToLower(c));
                }
                else
                {
                    sb.Append(c);
                }
            }

            return sb.ToString();
        }
    }
}