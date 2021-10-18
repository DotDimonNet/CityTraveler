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

            if (!_context.InstitutionTypes.Any())
            {
                _context.AddRange(
                    InstitutionType.Cafe,
                    InstitutionType.Cinema,
                    InstitutionType.Hostel,
                    InstitutionType.Hotel,
                    InstitutionType.Museum,
                    InstitutionType.Restourant,
                    InstitutionType.Shop,
                    InstitutionType.Supermarket,
                    InstitutionType.Theater,
                    InstitutionType.TransportStation);

                _context.SaveChanges();
            }

            if (!_context.PlaceTypes.Any())
            {
                _context.AddRange(
                    PlaceType.Event,
                    PlaceType.Institution,
                    PlaceType.Landscape);

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

        public static void SetupData(ApplicationContext _context)
        {
            
        }
    }
}
