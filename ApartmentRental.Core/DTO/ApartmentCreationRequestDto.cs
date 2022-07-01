namespace ApartmentRental.Core.DTO;

public class ApartmentCreationRequestDto
{
    public decimal RentAmount { get; set; }
    public int NumberOfRooms { get; set; }
    public int SquereMeters { get; set; }
    public int Floor { get; set; }
    public bool IsElevatorInBuilding { get; set; }
    public string City { get; set; }
    public string Street { get; set; }
    public string PostCode { get; set; }
    public string ApartmentNumber { get; set; }
    public string BuildingNumber { get; set; }
    public string Country { get; set; }
    public int LandlordId { get; set; }

    public ApartmentCreationRequestDto(decimal rentAmount, int numberOfRooms, int squereMeters, int floor, bool isElevatorInBuilding, string city, string street, string postCode, string apartmentNumber, string buildingNumber, string country, int landlordId)
    {
        RentAmount = rentAmount;
        NumberOfRooms = numberOfRooms;
        SquereMeters = squereMeters;
        Floor = floor;
        IsElevatorInBuilding = isElevatorInBuilding;
        City = city;
        Street = street;
        PostCode = postCode;
        ApartmentNumber = apartmentNumber;
        BuildingNumber = buildingNumber;
        Country = country;
        LandlordId = landlordId;
    }
}