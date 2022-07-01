using ApartmentRental.Core.DTO;
using ApartmentRental.Infrastructure.Entities;
using ApartmentRental.Infrastructure.Repository;

namespace ApartmentRental.Core.Services;

public class ApartmentService : IApartmentService

{
    private readonly IApartmentRepository _apartmentRepository;
    private readonly ILandlordRepository _landlordRepository;
    private readonly IAddressService _addressService;

    public ApartmentService(IApartmentRepository apartmentRepository, ILandlordRepository landlordRepository, IAddressService addressService)
    {
        _apartmentRepository = apartmentRepository;
        _landlordRepository = landlordRepository;
        _addressService = addressService;
    }

    public async Task<IEnumerable<ApartmentBasicInformationResponseDto>> GetAllApartmentsBasicInfosAsync()
    {
        var apartments = await _apartmentRepository.GetAllAsync();
        return apartments.Select(x => new ApartmentBasicInformationResponseDto(
            x.Rent,
            x.Rooms,
            x.Size,
            x.HasElevator,
            x.Address.City,
            x.Address.Street));
    }

    public async Task AddNewApartmentToExistingLandlordAsync(ApartmentCreationRequestDto dto)
    {
        var landlord = await _landlordRepository.GetById(dto.LandlordId);

        var addressId = await _addressService.GetAddressIdOrCreateAsync(dto.Country, dto.City,
            dto.PostCode, dto.Street, dto.BuildingNumber, dto.ApartmentNumber);
        await _apartmentRepository.AddAsync(new Apartment
            {
                AddressId = addressId,
                Floor = dto.Floor,
                LandlordId = landlord.Id,
                HasElevator = dto.IsElevatorInBuilding,
                Rent = dto.RentAmount,
                Size = dto.SquereMeters,
                Rooms = dto.NumberOfRooms
            }
        );
    }

    public async Task<ApartmentBasicInformationResponseDto> GetTheCheapestApartmentAsync()
    {
        var apartments = await _apartmentRepository.GetAllAsync();

        var cheapestOne = apartments.MinBy(x => x.Rent);

        if (cheapestOne is null) return null;
        
        return new ApartmentBasicInformationResponseDto(
            cheapestOne.Rent,
            cheapestOne.Rooms,
            cheapestOne.Size,
            cheapestOne.HasElevator,
            cheapestOne.Address.City,
            cheapestOne.Address.Street);
    }
}