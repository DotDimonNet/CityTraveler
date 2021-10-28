using CityTraveler.Domain.Entities;
using CityTraveler.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CityTraveler.Infrastucture.Data
{
    public static class InitialData
    {
        public static void SetupEnums(ApplicationContext _context)
        {
            if (!_context.CommentStatuses.Any())
            {
                _context.AddRange(
                    CommentStatus.Liked,
                    CommentStatus.OMG,
                    CommentStatus.Suprised);

                _context.SaveChanges();
            }

            if (!_context.EntertainmentType.Any())
            {
                _context.AddRange(
                    EntertainmentType.Event,
                    EntertainmentType.Institution,
                    EntertainmentType.Landskape);

                _context.SaveChanges();
            }


            if (!_context.TripStatuses.Any())
            {
                _context.AddRange(
                    TripStatus.InProgress,
                    TripStatus.New,
                    TripStatus.Passed);

                _context.SaveChanges();
            }
        }

        public static void SetupData(ApplicationContext context)
        {
            var trips = new List<TripModel>();
            var random = new Random();
            for (int index = 0; index < 100; index++)
            {
                var trip = new TripModel()
                {
                    AverageRating = random.Next(1, index + 2),
                    DafaultTrip = false,
                    Description = $"test_desc_{index}",
                    TripStart = DateTime.Now.AddDays(index),
                    TripEnd = DateTime.Now.AddDays(index + 2),
                    Entertaiment = new List<EntertaimentModel>(),
                    Images = new List<TripImageModel>()
                    {
                        new TripImageModel() { Title = "test_image" }
                    }
                };
                trips.Add(trip);
            }
            context.Trips.AddRange(trips);
            context.SaveChanges();
        }
        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        public static Guid ToGuid(int value)
        {
            byte[] bytes = new byte[16];
            BitConverter.GetBytes(value).CopyTo(bytes, 0);
            return new Guid(bytes);
        }
    }
}
