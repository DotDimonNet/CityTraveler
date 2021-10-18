using CityTraveler.Entities;
using CityTraveler.Infrastructure.Interfaces;
using CityTraveler.Repository;
using CityTraveler.Repository.DbContext;
using CityTraveler.Services;
using CityTraveler.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityTraveler
{
    public static class ScopedDIContainer
    {
        public static void AddAllScopedPairs(ref IServiceCollection services)
        {
            services.AddScoped<IAddress, Address>();
            services.AddScoped<IComment, Comment>();
            services.AddScoped<IContactInfo, ContactInfo>();
            services.AddScoped<ICoordinates, Coordinates>();
            services.AddScoped<IEvent, Event>();
            services.AddScoped<IImage, Image>();
            services.AddScoped<IInstitution, Institution>();
            services.AddScoped<ILandskape, Landskape>();
            services.AddScoped<IPrice, Price>();
            services.AddScoped<IRating, Rating>();
            services.AddScoped<IReview, Review>();
            services.AddScoped<IRoute, Route>();
            services.AddScoped<IStreet, Street>();
            services.AddScoped<ITrip, Trip>();
            services.AddScoped<IUser, User>();
            services.AddScoped<IReview, Review>();
            services.AddScoped<ISyncModel, SyncModel>();
            services.AddTransient<IServiceContext>(x => new ServiceContext());
            services.AddTransient<IDbSyncManager>(x => new DbSyncManager(""));
            services.AddTransient<IDbContext>(x => new DbContext("", x.GetRequiredService(typeof(IDbSyncManager)) as IDbSyncManager));
            services.AddTransient<IAuthService>(x => new AuthService(x.GetRequiredService(typeof(IDbContext)) as IDbContext));
            services.AddTransient<ICityArchitectureService>(x => new CityArchitectureService(
                x.GetRequiredService(typeof(IServiceContext)) as IServiceContext, 
                x.GetRequiredService(typeof(IDbContext)) as IDbContext));
            //services.AddScoped<IEntertainmentService, IEntertainmentService>();
            services.AddTransient<ISocialMediaService>(x => new SocialMediaService(
                x.GetRequiredService(typeof(IServiceContext)) as IServiceContext,
                x.GetRequiredService(typeof(IDbContext)) as IDbContext));
            services.AddTransient<ITripService>(x => new TripService());
            services.AddTransient<IUserManagementService>(x => new UserManagementService());
            services.AddScoped(typeof(IDbRequestManager<>), typeof(DbRequestManager<>));
            services.AddScoped(typeof(IDbCollection<>), typeof(DbCollection<>));
        }
    }
}
