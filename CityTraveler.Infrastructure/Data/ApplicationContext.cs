using CityTraveler.Domain.Entities;
using CityTraveler.Domain.Enums;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CityTraveler.Infrastucture.Data
{
    public class ApplicationContext : IdentityDbContext<ApplicationUserModel, ApplicationUserRole, Guid>, IDbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            if (!Database.ProviderName.Equals("Microsoft.EntityFrameworkCore.InMemory"))
            {
                Database.SetCommandTimeout(1000);
            }
        }

        public DbSet<UserProfileModel> UserProfiles { get; set; }
        public DbSet<AddressModel> Addresses { get; set; }
        public DbSet<CommentModel> Comments { get; set; }
        public DbSet<CoordinatesModel> Coordinates { get; set; }
        public DbSet<ImageModel> Images { get; set; }
        public DbSet<RatingModel> Ratings { get; set; }
        public DbSet<EntertaimentModel> Entertaiments { get; set; }
        public DbSet<ReviewModel> Reviews { get; set; }
        public DbSet<StreetModel> Streets { get; set; }
        public DbSet<TripModel> Trips { get; set; }
        public DbSet<PriceModel> Prices { get; set; }
        public DbSet<TripStatus> Statuses { get; set; }
        // enums tables

        //public DbSet<CommentStatus> CommentStatuses { get; set; }S

        public DbSet<TripStatus> TripStatuses { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<UserProfileModel>().HasOne(x => x.User).WithOne(x => x.Profile).HasForeignKey<ApplicationUserModel>(x => x.UserId).OnDelete(DeleteBehavior.Cascade);
            builder.Entity<ApplicationUserModel>().HasMany(x => x.Images).WithOne(x => x.User).HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Cascade);
            builder.Entity<ApplicationUserModel>().HasMany(x => x.Reviews).WithOne(x => x.User).HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Cascade);
            builder.Entity<ApplicationUserModel>().HasMany(x => x.Trips).WithMany(x => x.Users);
            builder.Entity<StreetModel>().HasMany(x => x.Addresses).WithOne(x => x.Street).HasForeignKey(x => x.StreetId).OnDelete(DeleteBehavior.Cascade);
            builder.Entity<ReviewModel>().HasMany(x => x.Comments).WithOne(x => x.Review).HasForeignKey(x => x.ReviewId).OnDelete(DeleteBehavior.Cascade);
            builder.Entity<ReviewModel>().HasMany(x => x.Images).WithOne(x => x.Review).HasForeignKey(x => x.ReviewId).OnDelete(DeleteBehavior.NoAction);
            builder.Entity<ReviewModel>().HasOne(x => x.Rating).WithOne(x => x.Review).HasForeignKey<RatingModel>(x => x.ReviewId).OnDelete(DeleteBehavior.ClientSetNull);
            builder.Entity<AddressModel>().HasOne(x => x.Coordinates).WithOne(x => x.Address).HasForeignKey<AddressModel>(x => x.CoordinatesId).OnDelete(DeleteBehavior.NoAction);
            builder.Entity<StreetModel>().HasMany(x => x.Coordinates).WithOne(x => x.Street).HasForeignKey(x => x.StreetId).OnDelete(DeleteBehavior.NoAction);
            builder.Entity<EntertaimentModel>().HasOne(x => x.Address).WithOne(x => x.Entertaiment).HasForeignKey<EntertaimentModel>(x => x.AddressId).OnDelete(DeleteBehavior.Cascade);
            builder.Entity<EntertaimentModel>().HasMany(x => x.Reviews).WithOne(x => x.Entertaiment).HasForeignKey(x => x.EntertainmentId).OnDelete(DeleteBehavior.Cascade);
            builder.Entity<TripModel>().HasMany(x => x.Reviews).WithOne(x => x.Trip).HasForeignKey(x => x.TripId).OnDelete(DeleteBehavior.Cascade);
            builder.Entity<EntertaimentModel>().HasMany(x => x.Images).WithOne(x => x.Entertaiment).HasForeignKey(x => x.EntertaimentId).OnDelete(DeleteBehavior.Cascade);
            builder.Entity<EntertaimentModel>().HasOne(x => x.AveragePrice).WithOne(x => x.Entertaiment).HasForeignKey<EntertaimentPriceModel>(x=>x.EntertaimentId).OnDelete(DeleteBehavior.Cascade);
            builder.Entity<TripModel>().HasMany(x => x.Images).WithOne(x => x.Trip).HasForeignKey(x => x.TripId).OnDelete(DeleteBehavior.Cascade);
            builder.Entity<TripModel>().HasOne(x => x.Price).WithOne(x => x.Trip).HasForeignKey<TripPriceModel>(x => x.TripId).OnDelete(DeleteBehavior.Cascade);
            builder.Entity<TripModel>().HasMany(x => x.Entertaiments).WithMany(x => x.Trips);
            builder.Entity<TripStatus>().HasKey(x => x.ValueId).HasName("PK_TripStatus");
            base.OnModelCreating(builder);
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            AddTimestamps();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override int SaveChanges()
        {
            AddTimestamps();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            AddTimestamps();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            AddTimestamps();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void AddTimestamps()
        {
            var entities = ChangeTracker.Entries().Where(x => x.Entity is Entity && (x.State == EntityState.Added || x.State == EntityState.Modified));

            foreach (var entity in entities)
            {
                if (entity.State == EntityState.Added)
                {
                    ((Entity)entity.Entity).Created = DateTime.UtcNow;
                }

                ((Entity)entity.Entity).Modified = DateTime.UtcNow;
            }
        }
    }
}
