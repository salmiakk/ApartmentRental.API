namespace ApartmentRental.Core.Entities;

public class Address : BaseEntity
{
    public string Street { get; set; }
    public string? ApartmentNumber { get; set; }
    public string? BuildingNumber { get; set; }
    public string City { get; set; }
    public string Postcode { get; set; }
    public string Country { get; set; }
}