﻿using CityTraveler.Domain.DTO;
using CityTraveler.Domain.Entities;
using System;
using System.Collections.Generic;
using CityTraveler.Infrastucture.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CityTraveler.Services.Extensions;

namespace CityTraveler.Services.Extensions
{
    public static class DTOExtensions
    {
        public static UserDTO ToUserDTO(this ApplicationUserModel userModel)
        {
            return new UserDTO()
            {
                Name = userModel.Profile.Name,
                AvatarSrc = userModel.Profile.AvatarSrc,
                PhoneNumber = userModel.PhoneNumber,
                Email = userModel.Email
            };
        }

        public static ApplicationUserModel ToApplicationUser(this RegisterDTO registerModel)
        {
            var user = new ApplicationUserModel
            {
                Images = new List<UserImageModel>()
                { new UserImageModel
                {
                    IsMain = true,
                    Source = registerModel.AvatarSrc,
                    Title = "Avatar",
                    Description = "My best photo" } },
                UserName = registerModel.UserName,
                Profile = new UserProfileModel()
                {
                    Gender = registerModel.Gender,
                    Name = registerModel.Name,
                    Birthday = registerModel.Birthday,
                    AvatarSrc = registerModel.AvatarSrc,
                    User = new ApplicationUserModel()
                    {
                        UserName = registerModel.UserName,
                        PasswordHash = registerModel.Password,
                        Email = registerModel.Email,
                        PhoneNumber = registerModel.PhoneNumber

                    }
                }
            };
            return user;
        }
        public static EntertaimentModel ToEntertaiment(this EntertainmentDTO entertainmentDTO, StreetModel street)
        {
            try
            {
                if (street == null)
                {
                    street = new StreetModel()
                    {
                        Title = entertainmentDTO.StreetTitle,
                        Description = entertainmentDTO.StreetDescription
                    };
                }
                var result = new EntertaimentModel()
                {
                    Reviews = new List<EntertainmentReviewModel>(),
                    Title = entertainmentDTO.Title,
                    Description = entertainmentDTO.Description,
                    Trips = new List<TripModel>(),
                    AveragePrice = new EntertaimentPriceModel() 
                    {
                        Title = entertainmentDTO.PriceTitle, 
                        Value = entertainmentDTO.PriceValue 
                    },
                    Address = new AddressModel()
                    {
                        Street = street,
                        HouseNumber = entertainmentDTO.Address.HouseNumber,
                        ApartmentNumber = entertainmentDTO.Address.ApartmentNumber,
                        Coordinates = new CoordinatesModel()
                        {
                            Latitude = entertainmentDTO.Address.Coordinates.Latitude,
                            Longitude = entertainmentDTO.Address.Coordinates.Longitude
                        }
                    },
                };
                return result;
            }
            catch (Exception ex)
            { 
                throw new Exception("EntertainmentDTO isn't correct");
            }     
        }
        public static TripModel ToTrip(this TripDTO tripDTO)
        {
            return new TripModel
            {
                TripStart = tripDTO.TripStart,
                TripEnd = tripDTO.TripEnd,
                Title = tripDTO.Title,
                Description = tripDTO.Description,
               // TripStatus = tripDTO.TripStatus,
                TagSting = tripDTO.TagSting,
                Images = new List<TripImageModel>(),
                Reviews = new List<TripReviewModel>(),
                Entertaiment = new List<EntertaimentModel>()
            };
        }
        public static TripModel ToNewTrip(this AddNewTripDTO newTripDTO)
        {
            return new TripModel 
            {
                Title = newTripDTO.Title,
                Description = newTripDTO.Description,
                TripStart = newTripDTO.TripStart,
                Entertaiment = new List<EntertaimentModel>(),
                //TripStatus = newTripDTO.TripStatus
            };
        }

        public static TripModel ToDefaultTrip(this DefaultTripDTO defaultTripDTO)
        {
            return new TripModel
            {
                Title = defaultTripDTO.Title,
                Description = defaultTripDTO.Description,
                TagSting = defaultTripDTO.TagSting,
                AverageRating = defaultTripDTO.AverageRating,
                Price = new TripPriceModel(),
                OptimalSpent = defaultTripDTO.OptimalSpent,
                Images = new List<TripImageModel>(),
                Reviews = new List<TripReviewModel>(),
                Entertaiment = new List<EntertaimentModel>(),
                DafaultTrip =defaultTripDTO.DefaultTrip
            };
                throw new Exception($"EntertainmentDTO isn't correct: {ex.Message}");
            }
            

        }
    }
}
