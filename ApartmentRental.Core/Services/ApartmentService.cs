using ApartmentRental.Core.Dto;
namespace ApartmentRental.Core.Services;

public class ApartmentService : IApartmentService

{
    public Task<IEnumerable<ApartmentBasicInformationResponseDto>> GetAllApartmentsBasicInfosAsync()
    {
        throw new NotImplementedException();

    }

    public Task AddNewApartmentToExistingLandlordAsync(ApartmentCreationRequestDto dto)
    {
        throw new NotImplementedException();
    }

    public Task<ApartmentBasicInformationResponseDto> GetTheCheapestApartmentAsync()
    {
        throw new NotImplementedException();
    }
}