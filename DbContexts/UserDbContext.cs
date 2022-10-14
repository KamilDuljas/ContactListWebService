using ContactListWebService.Entities;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;

namespace ContactListWebService.DbContexts
{
    public class UserDbContext : DbContext
    {
        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; } = null!;

        public DbSet<Category> Categories { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            
            builder.Entity<User>().HasData(
                new User()
                {
                    UserId = 1,
                    Name = "Wilson",
                    Surname = "Smith",
                    Password = "CfDJ8NexdWr8pwVLtd6Bq9KrAxHnB1yxocpHLyp58_sYx6SnpDu5-h13HgDtfA4TXvO_5YesDkq1i9KvOIu6sxNgQwW4jpPjClzPKt_0ayP8-LMzmjAe9mlhP7AIZMEQo9TSWg",
                    BirthDate = "1990-12-1",
                    Phone = "504-555-555",
                    Email = "smith@onet.pl",
                },
                new User()
                {
                    UserId = 2,
                    Name = "Adam",
                    Surname = "Kowalski",
                    Password = "CfDJ8NexdWr8pwVLtd6Bq9KrAxHnB1yxocpHLyp58_sYx6SnpDu5-h13HgDtfA4TXvO_5YesDkq1i9KvOIu6sxNgQwW4jpPjClzPKt_0ayP8-LMzmjAe9mlhP7AIZMEQo9TSWg",
                    BirthDate = "2001-12-1",
                    Phone = "888-334-333",
                    Email = "jk@poczta.com",
                },
                new User()
                {
                    UserId = 3,
                    Name = "Jan",
                    Surname = "Niezbedny",
                    Password = "CfDJ8NexdWr8pwVLtd6Bq9KrAxHnB1yxocpHLyp58_sYx6SnpDu5-h13HgDtfA4TXvO_5YesDkq1i9KvOIu6sxNgQwW4jpPjClzPKt_0ayP8-LMzmjAe9mlhP7AIZMEQo9TSWg",
                    BirthDate = "2020-01-1",
                    Phone = "111-444-333",
                    Email = "jn2000@gmail.com",
                });

            builder.Entity<Category>()
                .HasData(
                new Category()
                {
                    Id = 1,
                    CategoryType = Category.CategoryEnum.Business,
                    UserId = 1
                },
                new Category()
                {
                    Id = 2,
                    CategoryType = Category.CategoryEnum.Private,
                    UserId = 2
                },
                new Category()
                {
                    Id = 3,
                    CategoryType = Category.CategoryEnum.Other,
                    UserId = 3,
                    Subcategory = "Other than other"
                });

            base.OnModelCreating(builder);
        }
    }
}
