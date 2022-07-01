using ApartmentRental.Core.DTO;

namespace ApartmentRental.Core.Services;

public interface IApartmentService
{
    Task<IEnumerable<ApartmentBasicInformationResponseDto>> GetAllApartmentsBasicInfosAsync();
    Task AddNewApartmentToExistingLandlordAsync(ApartmentCreationRequestDto dto);
    Task<ApartmentBasicInformationResponseDto> GetTheCheapestApartmentAsync();
}