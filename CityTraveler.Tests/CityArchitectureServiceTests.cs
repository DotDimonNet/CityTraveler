﻿using CityTraveler.Domain.DTO;
using CityTraveler.Domain.Entities;
using CityTraveler.Domain.Enums;
using CityTraveler.Services;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CityTraveler.Tests
{
    public class CityArchitectureServiceTests
    {
        [SetUp]
        public async Task Setup()
        {
            await ArrangeTests.SetupDbContext();
        }

        [Test]
        public async Task RemoveEntertainmentTest()
        {
            var entertainment = ArrangeTests.ApplicationContext.Entertaiments
                .FirstOrDefault();
            var service = new CityArchitectureService(ArrangeTests.ApplicationContext);

            var isRemoved = await service.RemoveEntertainment(entertainment.Id);

            Assert.IsTrue(isRemoved);
            Assert.IsTrue(!ArrangeTests.ApplicationContext.Entertaiments.Contains(entertainment));
        }

        [Test]
        public async Task RemoveEntertainmentExeptionTest()
        {
            var entertainment = ArrangeTests.ApplicationContext.Entertaiments
                .FirstOrDefault();
            var service = new CityArchitectureService(ArrangeTests.ApplicationContext);

            try
            {
                var isRemoved = await service.RemoveEntertainment(Guid.NewGuid());
            }
            catch (Exception e)
            {
                Assert.AreEqual(e.Message, "Failed to remove entertainment");
            }
        }

        [Test]
        public async Task SetEntertainmentTest()
        {
            var entertainments = ArrangeTests.ApplicationContext.Entertaiments
                .Where(x=>x.Address.Street== ArrangeTests.ApplicationContext.Streets
                .FirstOrDefault());
            var service = new CityArchitectureService(ArrangeTests.ApplicationContext);

            var isSeted = await service.SetEntertaiment(entertainments);

            Assert.IsTrue(isSeted);
            foreach (var item in entertainments)
            {
                Assert.IsTrue(ArrangeTests.ApplicationContext.Entertaiments.Contains(item));
            }
        }

        [Test]
        public async Task UpdateEntertainmentTest()
        {
            var entertainment = ArrangeTests.ApplicationContext.Entertaiments
                .FirstOrDefault();
            entertainment.Title = "Updated";
            var service = new CityArchitectureService(ArrangeTests.ApplicationContext);

            var isUpdated = await service.UpdateEntertainment(entertainment);
            
            var newEntertainment = ArrangeTests.ApplicationContext.Entertaiments
                .FirstOrDefault(x=>x.Id==entertainment.Id);
            Assert.IsTrue(isUpdated);
            Assert.IsTrue(newEntertainment.Title == entertainment.Title);
            Assert.IsFalse(newEntertainment.Modified == entertainment.Modified);
            Assert.AreEqual(newEntertainment.Created, entertainment.Created);
        }

        [Test]
        public async Task UpdateEntertainmentRemoveExeptionTest()
        {
            var newEntertainment = new EntertaimentModel();
            var service = new CityArchitectureService(ArrangeTests.ApplicationContext);

            var isUpdated = await service.UpdateEntertainment(newEntertainment);

            var newEntertainmentAfter = ArrangeTests.ApplicationContext.Entertaiments
                .FirstOrDefault(x => x.Id == newEntertainment.Id);
            Assert.IsTrue(isUpdated);
            Assert.AreEqual(newEntertainmentAfter, newEntertainment);
        }

        public async Task AddEntertainmentTest()
        {
            var entertainment = new EntertainmentDTO();
            var service = new CityArchitectureService(ArrangeTests.ApplicationContext);
            var contextLenght = ArrangeTests.ApplicationContext.Entertaiments.Count();

            var isAdded = await service.AddEntertainment(entertainment);

            var newContextLenght = ArrangeTests.ApplicationContext.Entertaiments.Count();
            Assert.IsTrue(isAdded);
            Assert.IsTrue(contextLenght == newContextLenght - 1);
        }
    }
}