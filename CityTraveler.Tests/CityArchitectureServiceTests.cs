using CityTraveler.Domain.DTO;
using CityTraveler.Domain.Entities;
using CityTraveler.Domain.Enums;
using CityTraveler.Services;
using NUnit.Framework;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;

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
            var service = new CityArchitectureService(ArrangeTests.ApplicationContext, ArrangeTests.TestMapper);

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
        public async Task AddEntertainmentsTest()
        {
            var entertainmentsDTO = new List<EntertainmentDTO>()
            {
                new EntertainmentDTO()
                {
                    Address = new EntertainmentAddressDTO()
                    {
                        Coordinates = new CoordinatesDTO(){},
                    },
                    StreetTitle = "12354",
                    Type = EntertainmentType.Event.Id,
                },
                new EntertainmentDTO()
                {
                    Address = new EntertainmentAddressDTO()
                    {
                        Coordinates = new CoordinatesDTO(){},
                    },
                    StreetTitle = "3566",
                    Type = EntertainmentType.Event.Id,
                },
            };
            var contextSize = ArrangeTests.ApplicationContext.Entertaiments.Count();
            var service = new CityArchitectureService(ArrangeTests.ApplicationContext);

            var isSeted = await service.AddEntertainments(entertainmentsDTO);
            var newContexSize = ArrangeTests.ApplicationContext.Entertaiments.Count();

            Assert.IsTrue(isSeted);
            Assert.AreNotEqual(contextSize, newContexSize);
        }

        [Test]
        public async Task UpdateEntertainmentTest()
        {
            var oldEntertainment = ArrangeTests.ApplicationContext.Entertaiments
                .FirstOrDefault();
            var oldTitle = oldEntertainment.Title;
            var oldModifided = oldEntertainment.Modified;
            var oldCreated = oldEntertainment.Created;
            oldEntertainment.Title = "Updated";
            var service = new CityArchitectureService(ArrangeTests.ApplicationContext);

            var isUpdated = await service.UpdateEntertainment(oldEntertainment);
            
            var newEntertainment = ArrangeTests.ApplicationContext.Entertaiments
                .FirstOrDefault(x=>x.Id==oldEntertainment.Id);
            Assert.IsTrue(isUpdated);
            Assert.IsFalse(newEntertainment.Title == oldTitle);
            Assert.IsFalse(newEntertainment.Modified == oldModifided);
            Assert.AreEqual(newEntertainment.Created, oldCreated);
        }

        [Test]
        public async Task UpdateEntertainmentExeptionTest()
        {
            var newEntertainment = new EntertaimentModel();
            var service = new CityArchitectureService(ArrangeTests.ApplicationContext);

            var isUpdated = await service.UpdateEntertainment(newEntertainment);

            var newEntertainmentAfter = ArrangeTests.ApplicationContext.Entertaiments
                .FirstOrDefault(x => x.Id == newEntertainment.Id);
            Assert.IsTrue(isUpdated);
            Assert.AreEqual(newEntertainmentAfter, newEntertainment);
        }

        [Test]
        public async Task AddEntertainmentTest()
        {
            var dto = new EntertainmentDTO()
            {
                Address = new EntertainmentAddressDTO()
                {
                    Coordinates = new CoordinatesDTO()
                    {
                        Latitude = 234,
                        Longitude = 543,
                    },
                    HouseNumber = "23",
                    ApartmentNumber = "34"
                },
                StreetTitle = "2355",
                StreetDescription = "",
                Type = EntertainmentType.Event.Id,
                PriceTitle = "Average price",
                PriceValue = 4567,
                Title = "Lorem",
                Description = "Ipsum"
            };
            var service = new CityArchitectureService(ArrangeTests.ApplicationContext);
            var contextLenght = ArrangeTests.ApplicationContext.Entertaiments.Count();

            var isAdded = await service.AddEntertainment(dto);

            var newContextLenght = ArrangeTests.ApplicationContext.Entertaiments.Count();
            Assert.IsTrue(isAdded);
            Assert.AreNotEqual(contextLenght, newContextLenght);
        }
    }
}