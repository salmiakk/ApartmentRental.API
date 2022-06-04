namespace ApartmentRental.Core.Entities;

public class Account : BaseEntity
{
    public string Firstname { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public bool IsActive { get; set; }

    public int AddressId { get; set; }
    public Address Address { get; set; }
    
}