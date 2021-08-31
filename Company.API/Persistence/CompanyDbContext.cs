using Company.API.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace Company.API.Persistence
{
    public class CompanyDbContext : DbContext
    {
        public CompanyDbContext(DbContextOptions<CompanyDbContext> options) : base(options)
        {
        }

        public DbSet<Entities.Company> Companies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Entities.Company>(entity =>
            {
                entity
                .HasKey(e => e.CompanyId);
            });

            modelBuilder.Entity<History>(entity =>
            {
                entity.HasKey(e => new { e.CompanyId, e.Created, e.EventType });
            });

            modelBuilder.Entity<HistoryTypeTwo>();
            modelBuilder.Entity<HistoryTypeOne>();

            modelBuilder.Entity<Entities.Company>().HasData(new Entities.Company { CompanyId = 1 }, new Entities.Company { CompanyId = 2 });
            modelBuilder.Entity<Entities.HistoryTypeOne>().HasData(new Entities.HistoryTypeOne { CompanyId = 1, Created = System.DateTime.Now.AddDays(-2), EventType = EventType.CompanyRegistered },
                new Entities.HistoryTypeOne { CompanyId = 1, Created = System.DateTime.Now.AddDays(-1), EventType = EventType.CompanyMadeInsolvent });
            modelBuilder.Entity<Entities.HistoryTypeTwo>().HasData(new Entities.HistoryTypeTwo { TypeTwoData = "TestTwo", CompanyId = 1, Created = System.DateTime.Now.AddDays(-2), EventType = EventType.CompanyRegistered },
                new Entities.HistoryTypeTwo { TypeTwoData = "TestTwo", CompanyId = 1, Created = System.DateTime.Now.AddDays(-1), EventType = EventType.CompanyMadeInsolvent });
        }
    }
}
