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
             Database.SetCommandTimeout(1000);
        }

        public DbSet<UserProfileModel> UserProfiles { get; set; }
        public DbSet<AddressModel> Addresses { get; set; }
        public DbSet<CommentModel> Comments { get; set; }
        public DbSet<CoordinatesModel> Coordinates { get; set; }
        public DbSet<EventModel> Events { get; set; }
        public DbSet<ImageModel> Images { get; set; }
        public DbSet<RatingModel> Ratings { get; set; }
        public DbSet<InstitutionModel> Institutions { get; set; }
        public DbSet<LandskapeModel> Landskapes { get; set; }
        public DbSet<ReviewModel> Reviews { get; set; }
        public DbSet<StreetModel> Streets { get; set; }
        public DbSet<TripModel> Trips { get; set; }
        public DbSet<PriceModel> Prices { get; set; }

        // enums tables

        public DbSet<CommentStatus> CommentStatuses { get; set; }
        public DbSet<InstitutionType> InstitutionTypes { get; set; }
        public DbSet<PlaceType> PlaceTypes { get; set; }
        public DbSet<TripStatus> TripStatuses { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<UserProfileModel>().HasOne(x => x.User).WithOne(x => x.Profile).HasForeignKey<UserProfileModel>(x => x.Id).OnDelete(DeleteBehavior.Cascade);
            builder.Entity<ApplicationUserModel>().HasMany(x => x.Images).WithOne(x => x.User).HasForeignKey(x => x.Id).OnDelete(DeleteBehavior.Cascade);
            builder.Entity<ApplicationUserModel>().HasMany(x => x.Reviews).WithOne(x => x.User).HasForeignKey(x => x.Id).OnDelete(DeleteBehavior.Cascade);
            builder.Entity<ApplicationUserModel>().HasMany(x => x.Trips).WithMany(x => x.Users);
            builder.Entity<StreetModel>().HasMany(x => x.Addresses).WithOne(x => x.Street).HasForeignKey(x => x.Id).OnDelete(DeleteBehavior.Cascade);
            builder.Entity<ReviewModel>().HasMany(x => x.Comments).WithOne(x => x.Review).HasForeignKey(x => x.Id).OnDelete(DeleteBehavior.Cascade);
            builder.Entity<ReviewModel>().HasMany(x => x.Images).WithOne(x => x.Review).HasForeignKey(x => x.Id).OnDelete(DeleteBehavior.NoAction);
            builder.Entity<ReviewModel>().HasOne(x => x.Rating).WithOne(x => x.Review).HasForeignKey<RatingModel>(x => x.Id).OnDelete(DeleteBehavior.Cascade);
            builder.Entity<AddressModel>().HasOne(x => x.Coordinates).WithOne(x => x.Address).HasForeignKey<CoordinatesModel>(x => x.Id).OnDelete(DeleteBehavior.NoAction);
            builder.Entity<EventModel>().HasOne(x => x.Address).WithOne(x => (EventModel)x.Entertaiment).HasForeignKey<EntertaimentAddressModel>(x => x.Id).OnDelete(DeleteBehavior.Cascade);
            builder.Entity<LandskapeModel>().HasOne(x => x.Address).WithOne(x => (LandskapeModel)x.Entertaiment).HasForeignKey<EntertaimentAddressModel>(x => x.Id).OnDelete(DeleteBehavior.Cascade);
            builder.Entity<InstitutionModel>().HasOne(x => x.Address).WithOne(x => (InstitutionModel)x.Entertaiment).HasForeignKey<EntertaimentAddressModel>(x => x.Id).OnDelete(DeleteBehavior.Cascade);
            builder.Entity<EventModel>().HasMany(x => x.Reviews).WithOne(x => (EventModel)x.Entertaiment).HasForeignKey(x => x.Id).OnDelete(DeleteBehavior.Cascade);
            builder.Entity<LandskapeModel>().HasMany(x => x.Reviews).WithOne(x => (LandskapeModel)x.Entertaiment).HasForeignKey(x => x.Id).OnDelete(DeleteBehavior.Cascade);
            builder.Entity<InstitutionModel>().HasMany(x => x.Reviews).WithOne(x => (InstitutionModel)x.Entertaiment).HasForeignKey(x => x.Id).OnDelete(DeleteBehavior.Cascade);
            builder.Entity<EventModel>().HasMany(x => x.Images).WithOne(x => (EventModel)x.Entertaiment).HasForeignKey(x => x.Id).OnDelete(DeleteBehavior.Cascade);
            builder.Entity<LandskapeModel>().HasMany(x => x.Images).WithOne(x => (LandskapeModel)x.Entertaiment).HasForeignKey(x => x.Id).OnDelete(DeleteBehavior.Cascade);
            builder.Entity<InstitutionModel>().HasMany(x => x.Images).WithOne(x => (InstitutionModel)x.Entertaiment).HasForeignKey(x => x.Id).OnDelete(DeleteBehavior.Cascade);
            builder.Entity<InstitutionModel>().HasMany(x => x.Prices).WithOne(x => x.Institution).HasForeignKey(x => x.Id).OnDelete(DeleteBehavior.Cascade);
            builder.Entity<TripModel>().HasMany(x => x.Images).WithOne(x => x.Trip).HasForeignKey(x => x.Id).OnDelete(DeleteBehavior.Cascade);
            builder.Entity<TripModel>().HasOne(x => x.Price).WithOne(x => x.Trip).HasForeignKey<TripPriceModel>(x => x.Id).OnDelete(DeleteBehavior.Cascade);
            builder.Entity<TripModel>().HasMany(x => x.Institutions).WithMany(x => x.Trips);
            builder.Entity<TripModel>().HasMany(x => x.Landskapes).WithMany(x => x.Trips);
            builder.Entity<TripModel>().HasMany(x => x.Events).WithMany(x => x.Trips);
            builder.Entity<CommentStatus>().HasKey(x => x.ValueId).HasName("PK_CommentStatus");
            builder.Entity<InstitutionType>().HasKey(x => x.ValueId).HasName("PK_InstitutionType");
            builder.Entity<PlaceType>().HasKey(x => x.ValueId).HasName("PK_PlaceType");
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
