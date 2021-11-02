﻿using CityTraveler.Domain.Entities;
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

        public void GetUserByIdTest()
        {
            var userModel = ArrangeTests.ApplicationContext.Users
                .FirstOrDefault();
            var service = new UserManagementService(ArrangeTests.ApplicationContext);
            var user = service.GetUserById(userModel.Id);

            Assert.IsNotNull(user);
            Assert.AreEqual(user.Id, userModel.Id);
            Assert.AreEqual(user.Profile.Name, userModel.Profile.Name);
            Assert.AreEqual(user.Email, userModel.Email);
          }
       
        [Test]
        public void GetUsersTests()
        {
            var usersGuids = ArrangeTests.ApplicationContext.Users
                .Select(x => x.Id);
            var service = new UserManagementService(ArrangeTests.ApplicationContext);
            var users = service.GetUsers(usersGuids);
            
            Assert.IsNotEmpty(users);
            Assert.AreEqual(users.Select(x => x.Id), usersGuids);
        }
        [Test]
        public void GetUsersRangeTests()
        {
            var usersRange = ArrangeTests.ApplicationContext.Users
                .Skip(0).Take(10);
            var service = new UserManagementService(ArrangeTests.ApplicationContext);
            var users = service.GetUsersRange(0,10);

            Assert.IsNotEmpty(users);
            Assert.AreEqual(usersRange.Count(), users.Count());
            Assert.AreEqual(users.Select(x => x.Profile.Name), usersRange.Select(x => x.Profile.Name));
        }

        [Test]
        public void GetUsersByPropeties()
        {
            var userModel = ArrangeTests.ApplicationContext
                .Users.LastOrDefault();
            var service = new UserManagementService(ArrangeTests.ApplicationContext);
            var users = service.GetUsersByPropeties(userModel.Profile.Name, "", "", default);

            Assert.IsNotEmpty(users);

            foreach (var user in users)
            {
                Assert.AreEqual(userModel.Profile.Name, user.Profile.Name);
                Assert.AreEqual(userModel.Profile.Gender, user.Profile.Gender);
                Assert.AreEqual(userModel.Profile.Birthday, user.Profile.Birthday);
                Assert.AreEqual(userModel.Email, user.Email);
                Assert.AreEqual(userModel.Id, user.Id);
            }
        }
    }
}
