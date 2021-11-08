using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CityTraveler.Domain.Entities;
using CityTraveler.Services;
using Moq;
using NUnit.Framework;

namespace CityTraveler.Tests
{
    public class StatisticServiceTest
    {
        [SetUp]
        public async Task Setup()
        {
            await ArrangeTests.SetupDbContext();
        }

        /*[Test]
        public async Task GetAverageRatingUserTripTest()
        {
            var userId = ArrangeTests.ApplicationContext.Users.FirstOrDefault(x => x.Trips.Count > 0).Id;
            var service = new StatisticService(ArrangeTests.ApplicationContext);

            var userRating = await service.GetAverageRatingUserTrip(userId);
            Assert.IsNotNull(userRating);

        }
        [Test]
        public async Task GetCountPassedUserTripTest()
        {
            var userId = ArrangeTests.ApplicationContext.UserProfiles
                .FirstOrDefault().User.UserId;
            var service = new StatisticService(ArrangeTests.ApplicationContext);

            var userPassTrip = await service.GetCountPassedUserTrip(userId);
            Assert.IsNotNull(userPassTrip);
        }
        [Test]
        public async Task QuantityPassEntertaimentTest()
        {
            var userId = ArrangeTests.ApplicationContext.UserProfiles
                .FirstOrDefault().User.UserId;
            var service = new StatisticService(ArrangeTests.ApplicationContext);

            var userNumberEntertaiments = await service.QuantityPassEntertaiment(userId);
            Assert.IsNotNull(userNumberEntertaiments);
        }
        [Test]
        public async Task GetAverageAgeUser()
        {
        
            var service = new StatisticService(ArrangeTests.ApplicationContext);

            var Age = await service.GetAverageAgeUser();
            Assert.IsNotNull(Age);
        }*/

    }
}
