using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CityTraveler.Domain.DTO;
using CityTraveler.Domain.Entities;
using CityTraveler.Domain.Enums;
using CityTraveler.Domain.Errors;
using CityTraveler.Services.Interfaces;
using CityTraveler.Infrastucture.Data;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using CityTraveler.Services;

namespace CityTraveler.Tests
{
    public class InstitutionServiceTest
    {
        [SetUp]
        public async Task Setup()
        {
            await ArrangeTests.SetupDbContext();
        }

        [Test]
        public async Task GetInstitutionByIdTest()
        {
            var institution = ArrangeTests.ApplicationContext.Entertaiments
                .FirstOrDefault(x => x.Type == EntertainmentType.Institution);
            var service = new InstitutionService(ArrangeTests.ApplicationContext);

            var testInstitution = await service.GetInstitutionById(institution.Id);

            Assert.IsNotNull(institution);
            Assert.IsNotNull(testInstitution);
            Assert.AreEqual(testInstitution, institution);
        }

        [Test]
        public async Task GetInstitutionByCoordinatesTest()
        {
            var institution = ArrangeTests.ApplicationContext.Entertaiments
                .FirstOrDefault(x => x.Type == EntertainmentType.Institution 
                && x.Address.Coordinates != null);
            var coordinatesDto = new CoordinatesDTO()
            {
                Latitude = institution.Address.Coordinates.Latitude,
                Longitude = institution.Address.Coordinates.Longitude
            };
            var service = new InstitutionService(ArrangeTests.ApplicationContext);

            var testInstitution = await service.GetInstitutionByCoordinates(coordinatesDto);

            Assert.IsNotNull(institution);
            Assert.IsNotNull(institution);
            Assert.AreEqual(institution, testInstitution);
        }

        [Test]
        public void GetInstitutionByStreetTest()
        {
            var street = ArrangeTests.ApplicationContext.Streets
                .FirstOrDefault();
            var service = new InstitutionService(ArrangeTests.ApplicationContext);

            var institutions = service.GetInstitutionsByStreet(street.Title);

            Assert.IsNotNull(street);
            Assert.IsNotNull(institutions);
            foreach (var item in institutions)
            {
                Assert.AreEqual(item.Address.Street, street);
            }
        }

        [Test]
        public void GetInstitutionsByStreetTitleTest()
        {
            var streetTitle = ArrangeTests.ApplicationContext.Streets
                .Select(x => x.Title).FirstOrDefault();
            var service = new InstitutionService(ArrangeTests.ApplicationContext);

            var institutions = service.GetInstitutionsByStreet(streetTitle);

            Assert.IsNotNull(streetTitle);
            Assert.IsNotNull(institutions);
            foreach (var item in institutions)
            {
                Assert.AreEqual(item.Address.Street.Title, streetTitle);
            }
        }

        [Test]
        public void GetInstitutionsByTitleTest()
        {
            var institutions = ArrangeTests.ApplicationContext.Entertaiments
                .Where(x => x.Type == EntertainmentType.Institution
                && x.Title.Contains("2")).ToList();
            var service = new InstitutionService(ArrangeTests.ApplicationContext);

            var testInstitutions = service.GetInstitutionsByTitle("2").ToList();

            Assert.IsNotNull(institutions);
            Assert.IsNotNull(institutions);
            Assert.AreEqual(testInstitutions, institutions);
        }

        [Test]
        public void GetInstitutionsTest()
        {
            var institutions = ArrangeTests.ApplicationContext.Entertaiments
                .Where(x => x.Type == EntertainmentType.Institution);
            var service = new InstitutionService(ArrangeTests.ApplicationContext);

            var testInstitutions = service.GetInstitutions(institutions.Select(x => x.Id));

            Assert.IsNotNull(institutions);
            Assert.IsNotNull(testInstitutions);
            Assert.AreEqual(testInstitutions.Count(), institutions.Count());
        }

        [Test]
        public async Task GettestInstitutionByAddressTest()
        {
            var address = ArrangeTests.ApplicationContext.Addresses
                .FirstOrDefault(x => x.Entertaiment.Type == EntertainmentType.Institution);
            var addressDto = new AddressDTO()
            {
                ApartsmentNumber = address.ApartmentNumber,
                HouseNumber = address.HouseNumber,
                StreetTitle = address.Street.Title
            };
            var service = new InstitutionService(ArrangeTests.ApplicationContext);

            var testInstitutions = await service.GetInstitutionByAddress(addressDto);

            Assert.IsNotNull(address);
            Assert.IsNotNull(testInstitutions);
            Assert.AreEqual(testInstitutions, address.Entertaiment);
        }
    }
}
