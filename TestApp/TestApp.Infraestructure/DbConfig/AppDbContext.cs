using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using TestApp.Core.Entities;
using TestApp.Core.Repositories;

namespace TestApp.Infraestructure.DbConfig
{
    public class AppDbContext : DbContext, IUnitOfWork
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Candidate>().HasIndex(c => new { c.Name, c.OfferId });
            modelBuilder.Entity<Employer>().HasMany(e => e.Offers);
            modelBuilder.Entity<Offer>().HasOne(o => o.Employer);
            modelBuilder.Entity<Offer>().HasOne(o => o.OfferType);
            modelBuilder.Entity<UserRole>().HasKey(ur => new { ur.RoleId, ur.UserId });
        }

        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            int result = await base.SaveChangesAsync();
            return result >= 0;
        }

        public DbSet<Employer> Employers { get; set; }
        public DbSet<OfferType> OfferTypes { get; set; }
        public DbSet<Offer> Offers { get; set; }
        public DbSet<Candidate> Candidates { get; set; }
        public DbSet<Role> Roles { get; set; }
    }
}