using Microsoft.EntityFrameworkCore;
using TCRSServerApp.Data.Entities;

namespace TCRSServerApp.Data
{
    public class TCRSContext : DbContext
    {
        public TCRSContext(DbContextOptions<TCRSContext> options) : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<ContentPost> ContentPosts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
#if DEBUG
            optionsBuilder.LogTo(Console.WriteLine);
#endif
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasData(
                    new User
                    {
                        Id = 1,
                        Email = "john.doe@gmail.com",
                        FirstName = "John",
                        LastName = "Doe",
                        Salt = "iergberihb",
                        Hash = "iewfbukrficruyewreob"
                    }
                );
        }
    }
}
