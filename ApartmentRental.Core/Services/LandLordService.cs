using ApartmentRental.Core.DTO;
using ApartmentRental.Infrastructure.Entities;
using ApartmentRental.Infrastructure.Repository;

namespace ApartmentRental.Core.Services;

public class LandLordService : ILandLordService
{
    private readonly IAddressService _addressService;
    private readonly ILandlordRepository _landlordRepository;

    public LandLordService(IAddressService addressService, ILandlordRepository landlordRepository)
    {
        _addressService = addressService;
        _landlordRepository = landlordRepository;
    }

    public async Task AddLandLordAsync(LandLordCreationRequestDto dto)
    {
        var addressId = await _addressService.GetAddressIdOrCreateAsync(dto.Country, dto.City, dto.PostCode, dto.Street,
            dto.NumberOfBuilding, dto.ApartmentNumber);

        await _landlordRepository.AddAsync(new Landlord
            {
                Account = new Account
                {
                    AddressId = addressId,
                    Firstname = dto.Name,
                    Surname = dto.Surname,
                    Email = dto.Email,
                    IsActive = true,
                    PhoneNumber = dto.PhoneNumber,
                    DateOfCreation = DateTime.UtcNow,
                    DateOfUpdate = DateTime.UtcNow
                },
                Apartments = new List<Apartment>(),
                DateOfCreation = DateTime.UtcNow,
                DateOfUpdate = DateTime.UtcNow
            });

    }
}