using Microsoft.EntityFrameworkCore;

namespace TCRSServerApp.Data
{
    public class TCRSContext : DbContext
    {
        public TCRSContext(DbContextOptions<TCRSContext> options) : base(options)
        {
        }

        public DbSet<BannerSettings> BannerSettings { get; set; }

        public DbSet<Entities.Category> Categories { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<ContentPost> ContentPosts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
#if DEBUG
            optionsBuilder.LogTo(Console.WriteLine, new[] { DbLoggerCategory.Database.Command.Name }, LogLevel.Information);
#endif
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<BannerSettings>()
                .HasData(
                    new BannerSettings
                    {
                        BannerId = 1,
                        Visible = false,
                        Message = "*Important Message Goes Here*"
                    }
                );

            modelBuilder.Entity<User>()
                .HasData(
                    new User
                    {
                        UserId = 1,
                        Email = "john.doe@gmail.com",
                        FirstName = "John",
                        LastName = "Doe",
                        Salt = "iergberihb",
                        Hash = "iewfbukrficruyewreob",
                        CreatedOn = DateTime.Now
                    }
                );

            modelBuilder.Entity<Category>()
                .HasData(
                    new Category {
                        CategoryId = 1,
                        Name = "Uncategorized",
                        Slug = "uncategorized"
                    },
                    new Category {
                        CategoryId = 2,
                        Name = "FAQ",
                        Slug = "faq"
                    },
                    new Category {
                        CategoryId = 3,
                        Name = "Documentation",
                        Slug = "documentation"
                    }
                );

            modelBuilder.Entity<ContentPost>()
                .HasOne(cp => cp.Category)
                .WithMany(c => c.ContentPosts)
                .HasForeignKey(cp => cp.CategoryId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
