using CityTraveler.Infrastucture.Data;
using CityTraveler.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityTraveler.Services
{
    public class ServiceContext : IServiceContext
    {
        private readonly ApplicationContext _dbContext;
        public SocialMediaService SocialMediaService { get; set; }
        public EntertainmentService EntertainmentService { get; set; }
        public TripService TripService { get; set; } 
        public UserManagementService UserManagementService { get; set; }
        public CityArchitectureService CityArchitectureService { get; set; }
        public AuthService AuthService { get; set; }
        public StatisticService StatisticService { get; set; }
        public HistoryService HistoryService { get; set; }

        public ServiceContext(ApplicationContext dbContext)
        {
            _dbContext = dbContext;
            SocialMediaService = new SocialMediaService(_dbContext);
            TripService = new TripService(_dbContext);
            UserManagementService = new UserManagementService(_dbContext);
            EntertainmentService = new EntertainmentService(_dbContext);
            CityArchitectureService = new CityArchitectureService(_dbContext);
            StatisticService = new StatisticService(_dbContext);
            HistoryService = new HistoryService(_dbContext);
            // AuthService = new AuthService(_dbContext);

        }
        /*public IServiceMetadata GetService(string serviceName)
        {
            return Services.FirstOrDefault();
        }

        public void SetServices(IEnumerable<IServiceMetadata> services)
        {
              Services = services;
        }*/
    }
}
