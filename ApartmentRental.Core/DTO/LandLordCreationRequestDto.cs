namespace ApartmentRental.Core.DTO;

public class LandLordCreationRequestDto
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Street { get; set; }
    public string ApartmentNumber { get; set; }
    public string NumberOfBuilding { get; set; }
    public string City { get; set; }
    public string PostCode { get; set; }
    public string Country { get; set; }

    public LandLordCreationRequestDto(string name, string surname, string email, string phoneNumber, string street, string apartmentNumber, string numberOfBuilding, string city, string postCode, string country)
    {
        Name = name;
        Surname = surname;
        Email = email;
        PhoneNumber = phoneNumber;
        Street = street;
        ApartmentNumber = apartmentNumber;
        NumberOfBuilding = numberOfBuilding;
        City = city;
        PostCode = postCode;
        Country = country;
    }
}