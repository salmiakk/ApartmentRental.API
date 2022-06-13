namespace ApartmentRental.Core.Dto;

public class ApartmentCreationRequestDto
{
    public decimal RentAmount { get; set; }
    public int NumberOfRooms { get; set; }
    public decimal SquereMeters { get; set; }
    public int Floor { get; set; }
    public bool IsElevatorInBuilding { get; set; }
    public string City { get; set; }
    public string Street { get; set; }
    public string ZipCode { get; set; }
    public string ApartmentNumber { get; set; }
    public string BuildingNumber { get; set; }
    public string Country { get; set; }
    public string LandlordId { get; set; }

    public ApartmentCreationRequestDto(decimal rentAmount, int numberOfRooms, decimal squereMeters, int floor, bool isElevatorInBuilding, string city, string street, string zipCode, string apartmentNumber, string buildingNumber, string country, string landlordId)
    {
        RentAmount = rentAmount;
        NumberOfRooms = numberOfRooms;
        SquereMeters = squereMeters;
        Floor = floor;
        IsElevatorInBuilding = isElevatorInBuilding;
        City = city;
        Street = street;
        ZipCode = zipCode;
        ApartmentNumber = apartmentNumber;
        BuildingNumber = buildingNumber;
        Country = country;
        LandlordId = landlordId;
    }
}