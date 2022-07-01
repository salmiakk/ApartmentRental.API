﻿using ApartmentRental.Infrastructure.Entities;
using ApartmentRental.Infrastructure.Repository;

namespace ApartmentRental.Core.Services;

public class AddressService : IAddressService
{
    private readonly IAddressRepository _addressRepository;

    public AddressService(IAddressRepository addressRepository)
    {
        _addressRepository = addressRepository;
    }

    public async Task<int> GetAddressIdOrCreateAsync(string country, string city, string postCode, string street, string buildingNumber,
        string apartmentNumber)
    {
        var id = await _addressRepository.GetAddressIdByItsAttributesAsync(country, city, postCode, street,
            buildingNumber, apartmentNumber);
        if (id != 0)
        {
            return id;
        }

        var address = await _addressRepository.CreateAndGetAsync(new Address
        {
            Country = country,
            City = city,
            Postcode = postCode,
            Street = street,
            BuildingNumber = buildingNumber,
            ApartmentNumber = apartmentNumber
        });
        
        return address.Id;
    }
}