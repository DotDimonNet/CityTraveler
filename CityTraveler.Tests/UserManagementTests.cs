using AutoMapper;
using CityTraveler.Domain.Entities;
using CityTraveler.Services;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CityTraveler.Tests
{
    public class UserManagementTests
    {
        [SetUp]
        public async Task Setup()
        {
            await ArrangeTests.SetupDbContext();
        }

        [Test]

        public  async Task GetUserByIdTest()
        {
            var userModel =await ArrangeTests.ApplicationContext.Users
                .FirstOrDefaultAsync();
            var service = new UserManagementService(ArrangeTests.ApplicationContext, ArrangeTests.TestMapper);
            var user = await service.GetUserByIdAsync(userModel.Id);

            Assert.IsNotNull(user);
            Assert.AreEqual(user.Id, userModel.Id);
            Assert.AreEqual(user.Profile.Name, userModel.Profile.Name);
            Assert.AreEqual(user.Email, userModel.Email);
          }
       
        [Test]
        public async Task GetUsersTests()
        {
            var usersGuids = ArrangeTests.ApplicationContext.Users
                .Select(x => x.Id);
            var service = new UserManagementService(ArrangeTests.ApplicationContext, ArrangeTests.TestMapper);
            var users = await service.GetUsersAsync(usersGuids);
            
            Assert.IsNotEmpty(users);
            Assert.AreEqual(users.Select(x => x.Id), usersGuids);
        }
        [Test]
        public async Task GetUsersRangeTests()
        {
            var usersRange = ArrangeTests.ApplicationContext.Users
                .Skip(0).Take(10);
            var service = new UserManagementService(ArrangeTests.ApplicationContext, ArrangeTests.TestMapper);
            var users = await service.GetUsersRangeAsync(0,10);

            Assert.IsNotEmpty(users);
            Assert.AreEqual(usersRange.Count(), users.Count());
            Assert.AreEqual(users.Select(x => x.Name), usersRange.Select(x => x.Profile.Name));
        }

        [Test]
        public async Task GetUsersByPropeties()
        {
            var userModel = ArrangeTests.ApplicationContext
                .Users.LastOrDefault();
            var service = new UserManagementService(ArrangeTests.ApplicationContext, ArrangeTests.TestMapper);
            var users = await service.GetUsersByPropetiesAsync(userModel.Profile.Name, "", "", default);

            Assert.IsNotEmpty(users);

            foreach (var user in users)
            {
                Assert.AreEqual(userModel.Profile.Name, user.Name);
                Assert.AreEqual(userModel.Profile.Gender, user.Gender);
                Assert.AreEqual(userModel.Profile.Birthday, user.Birthday);
                Assert.AreEqual(userModel.Email, user.Email);
                Assert.AreEqual(userModel.Id, user.Id);
            }
        }
    }
}
