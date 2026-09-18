using AutoMapper;
using AutoMapper.QueryableExtensions;
using Core.Dtos;
using Core.Dtos.Account.Address;
using Core.Dtos.Exceptions;
using Core.Dtos.Exceptions.Account;
using Core.Dtos.Exceptions.Account.Address;
using Core.Entities.Identity;
using Core.Interfaces;
using Domain.Entities;
using Domain.Entities.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Services;

public class AddressService(
        UserManager<UserEntity> _userManager,
        ISoftDeleteRepository<UserLocation, int> _userLocationRepo,
        ISoftDeleteRepository<City, int> _cityRepo,
        ISoftDeleteRepository<Region, int> _regionRepo,
        IMapper _mapper
    ) : IAddressService
{
    private const double EarthRadiusKm = 6371.0;

    public async Task AddAddress(Guid userId, AddAddressDto dto)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());
        if (user == null)
            throw new UserNotFoundException("User not found");


        var cities = await _cityRepo.Query().ToListAsync();
        if (cities == null || !cities.Any())
            throw new CityNotFoundException("No cities available");

        City? nearestCity = null;
        double nearestDistance = double.MaxValue;

        foreach (var city in cities)
        {
            if (string.IsNullOrWhiteSpace(city.CenterPosition))
                continue;

            var parts = city.CenterPosition.Split(
                ',',
                StringSplitOptions.RemoveEmptyEntries
            );

            if (parts.Length != 2)
                continue;

            if (!double.TryParse(
                    parts[0].Trim(),
                    System.Globalization.NumberStyles.Float,
                    System.Globalization.CultureInfo.InvariantCulture,
                    out var cityLatitude))
            {
                continue;
            }

            if (!double.TryParse(
                    parts[1].Trim(),
                    System.Globalization.NumberStyles.Float,
                    System.Globalization.CultureInfo.InvariantCulture,
                    out var cityLongitude))
            {
                continue;
            }

            var distance = CalculateDistanceKm(
                dto.Latitude,
                dto.Longitude,
                cityLatitude,
                cityLongitude
            );

            if (distance < nearestDistance)
            {
                nearestDistance = distance;
                nearestCity = city;
            }

            if (distance <= city.Radius)
            {
                nearestCity = city;
                nearestDistance = distance;
                break;
            }
        }

        if (nearestCity == null)
            throw new CityNotFoundException("Unable to determine city");

        var userLocation = new UserLocation
        {
            UserId = userId,
            Address = dto.Address,
            Location = $"{dto.Latitude.ToString(
                System.Globalization.CultureInfo.InvariantCulture
            )},{dto.Longitude.ToString(
                System.Globalization.CultureInfo.InvariantCulture
            )}",
            CityId = nearestCity.Id
        };

        await _userLocationRepo.AddAsync(userLocation);
        await _userLocationRepo.SaveChangesAsync();
    }

    private static double CalculateDistanceKm(
        double latitude1,
        double longitude1,
        double latitude2,
        double longitude2)
    {
        var lat1 = DegreesToRadians(latitude1);
        var lat2 = DegreesToRadians(latitude2);

        var deltaLat = DegreesToRadians(latitude2 - latitude1);
        var deltaLon = DegreesToRadians(longitude2 - longitude1);

        var a =
            Math.Sin(deltaLat / 2) * Math.Sin(deltaLat / 2) +
            Math.Cos(lat1) *
            Math.Cos(lat2) *
            Math.Sin(deltaLon / 2) *
            Math.Sin(deltaLon / 2);

        var c = 2 * Math.Atan2(
            Math.Sqrt(a),
            Math.Sqrt(1 - a)
        );

        return EarthRadiusKm * c;
    }

    private static double DegreesToRadians(double degrees)
    {
        return degrees * Math.PI / 180.0;
    }



    public async Task<List<AddressDto>> ListAddresses(Guid userId)
    {
        var addresses = await _userLocationRepo.Query()
            .Where(ul => ul.UserId == userId && !ul.IsDeleted)
            .ProjectTo<AddressDto>(_mapper.ConfigurationProvider)
            .ToListAsync();

        return addresses;
    }   

    public async Task DeleteAddress(Guid userId, int id)
    {
        var userLocation = await _userLocationRepo.Query()
            .Where(ul => ul.UserId == userId && ul.Id == id)
            .FirstOrDefaultAsync();

        if (userLocation == null)
            throw new AddressNotFoundException("Address not found");

        userLocation.IsDeleted = true;
        await _userLocationRepo.SaveChangesAsync();
    }
}