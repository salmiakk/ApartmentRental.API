namespace ApartmentRental.Core.DTO;

public class ApartmentBasicInformationResponseDto
{
    public decimal RentAmount { get; set; }
    public int NumberOfRooms { get; set; }
    public decimal SquereMeters { get; set; }
    public bool IsElevatorInBuilding { get; set; }
    public string City { get; set; }
    public string Street { get; set; }

    public ApartmentBasicInformationResponseDto(decimal rentAmount, int numberOfRooms, decimal squereMeters, bool isElevatorInBuilding, string city, string street)
    {
        RentAmount = rentAmount;
        NumberOfRooms = numberOfRooms;
        SquereMeters = squereMeters;
        IsElevatorInBuilding = isElevatorInBuilding;
        City = city;
        Street = street;
    }
}