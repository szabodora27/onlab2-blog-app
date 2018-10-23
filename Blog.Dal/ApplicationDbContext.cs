using Blog.Dal.Logging;
using Blog.Model.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Blog.Dal
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<EventLog> EventLogs { get; set; }

        public DbSet<BlogPost> BlogPosts { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Tag> Tags { get; set; }

        public DbSet<Favorite> Favorites { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUser>().ToTable("User");
            modelBuilder.Entity<ApplicationUser>()
                .HasMany(c => c.BlogPosts)
                .WithOne(p => p.CreatedBy);
            modelBuilder.Entity<ApplicationUser>()
                .HasMany(c => c.Favorites)
                .WithOne(f => f.User);

            modelBuilder.Entity<BlogPost>().ToTable("BlogPost");
            modelBuilder.Entity<Comment>().ToTable("Comment");
            modelBuilder.Entity<Category>().ToTable("Category");
            modelBuilder.Entity<Tag>().ToTable("Tag");
            modelBuilder.Entity<Favorite>().ToTable("Favorite");
            modelBuilder.Entity<EventLog>().ToTable("EventLog");

            modelBuilder.Entity<EventLog>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.EventId).HasColumnName("EventID");

                entity.Property(e => e.LogLevel).HasMaxLength(50);

                entity.Property(e => e.Message).HasMaxLength(4000);
            });

            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryId = 1, Name = "Inspiráció", Description = "Egy csók a múzsától, ihlet különleges alkalmakra és hétköznapokra. Ha úgy érzed, kell egy kis friss energia..." },
                new Category { CategoryId = 2, Name = "Munka", Description = "Hogyan férjen bele minden 24 órába? Tippek és ötletek munkához és iskolához, sikertörténetek és 'szeretem a munkám'-cikkek." },
                new Category { CategoryId = 3, Name = "Otthon", Description = "Csinosítjuk, rendben tartjunk, belakjuk, magunkévá tesszük, megosztjuk, haza megyünk." },
                new Category { CategoryId = 4, Name = "Test&Lélek" },
                new Category { CategoryId = 5, Name = "Ünnepek" }
            );

            modelBuilder.Entity<Tag>().HasData(
                new Tag { TagId = 1, Name = "Motiváció", CategoryId = 1},
                new Tag { TagId = 2, Name = "Megint hétfő", CategoryId = 1},
                new Tag { TagId = 3, Name = "Kihívás", CategoryId = 1},
                new Tag { TagId = 4, Name = "Miért ne?", CategoryId = 1},
                new Tag { TagId = 5, Name = "Szeretem a munkám", CategoryId = 2},
                new Tag { TagId = 6, Name = "S.O.S vizsgaidőszak", CategoryId = 2},
                new Tag { TagId = 7, Name = "Tanulás", CategoryId = 2},
                new Tag { TagId = 8, Name = "Állatok", CategoryId = 3},
                new Tag { TagId = 9, Name = "Család", CategoryId = 3},
                new Tag { TagId = 10, Name = "Dekoráció", CategoryId = 3},
                new Tag { TagId = 11, Name = "Gasztro", CategoryId = 3},
                new Tag { TagId = 12, Name = "Kert&Növények", CategoryId = 3},
                new Tag { TagId = 13, Name = "Kreatív", CategoryId = 3},
                new Tag { TagId = 14, Name = "Lakberendezés", CategoryId = 3},
                new Tag { TagId = 15, Name = "Lomtalanítás", CategoryId = 3},
                new Tag { TagId = 16, Name = "Rendszerezés", CategoryId = 3},
                new Tag { TagId = 17, Name = "Takarítás", CategoryId = 3},
                new Tag { TagId = 18, Name = "Divat&Stílus", CategoryId = 4},
                new Tag { TagId = 19, Name = "Egészség", CategoryId = 4},
                new Tag { TagId = 20, Name = "Mozgás", CategoryId = 4},
                new Tag { TagId = 21, Name = "Szépség", CategoryId = 4},
                new Tag { TagId = 22, Name = "Szerelem", CategoryId = 4},
                new Tag { TagId = 23, Name = "Utazás", CategoryId = 4},
                new Tag { TagId = 24, Name = "Zöld", CategoryId = 4},
                new Tag { TagId = 25, Name = "Esküvő", CategoryId = 5},
                new Tag { TagId = 26, Name = "Húsvéti ötlettár", CategoryId = 5},
                new Tag { TagId = 27, Name = "Karácsony", CategoryId = 5},
                new Tag { TagId = 28, Name = "Vendégvárás", CategoryId = 5},
                new Tag { TagId = 29, Name = "Születésnap", CategoryId = 5},
                new Tag { TagId = 30, Name = "Névnap", CategoryId = 5}
            );
        }
    }
}
