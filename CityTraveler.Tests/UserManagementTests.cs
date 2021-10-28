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
    class UserManagementTests
    {
        [SetUp]
        public async Task Setup()
        {
            await ArrangeTests.SetupDbContext();
        }

        [Test]

        public async Task GetUserByIdTest()
        {
            var userModel = ArrangeTests.ApplicationContext.Users
                .FirstOrDefault();
            var service = new UserManagementService(ArrangeTests.ApplicationContext);
            var user = await service.GetUserById(userModel.Id);

            Assert.IsNotNull(user);
            Assert.AreEqual(user.Id, userModel.Id);
            Assert.AreEqual(user.Profile.Name, userModel.Profile.Name);
            Assert.AreEqual(user.Email, userModel.Email);
          }

        [Test]
        public void GetUsersByBirthdayTests()
        {
            var birthdayOfUser = ArrangeTests.ApplicationContext.Users
                .FirstOrDefault().Profile.Birthday;
            var service = new UserManagementService(ArrangeTests.ApplicationContext);

            var users = service.GetUsersByBirthday(birthdayOfUser);

            Assert.IsNotEmpty(users);

            foreach (var user in users)
            {
                Assert.AreEqual(user.Profile.Birthday, birthdayOfUser);
            }
        }

        [Test]
        public void GetUsersByNameTests()
        {
            var userModel = ArrangeTests.ApplicationContext.Users
                .LastOrDefault();
            var service = new UserManagementService(ArrangeTests.ApplicationContext);
            var users = service.GetUsersByName(userModel.Profile.Name);

            Assert.IsNotEmpty(users);
            foreach (var user in users)
            {
                Assert.AreEqual(user.Profile.Name, userModel.Profile.Name);
            }
        }
        [Test]
        public void GetUsersByGenderTests()
        {
            var userGender = ArrangeTests.ApplicationContext.Users
                .FirstOrDefault().Profile.Gender;
            var service = new UserManagementService(ArrangeTests.ApplicationContext);
            var users = service.GetUsersByGender(userGender);
           
            Assert.IsNotEmpty(users);
            foreach (var user in users)
            {
                Assert.AreEqual(user.Profile.Gender, userGender);
            }
        }
        [Test]
        /*public async Task GetUsersByEmailTests()
        {
            var userModel = ArrangeTests.ApplicationContext.Users
                .FirstOrDefault();
            var service = new UserManagementService(ArrangeTests.ApplicationContext);

            var userResult = await service.GetUserByEmail(userModel.Email);

            Assert.IsNotNull(userResult);
            Assert.AreEqual(userModel.Email, userResult.Email);
        }
        [Test]*/
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
    }
}
