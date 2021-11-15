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
            var users = await service.GetUsersByPropetiesAsync(userModel.Profile.Name, "", "");

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
