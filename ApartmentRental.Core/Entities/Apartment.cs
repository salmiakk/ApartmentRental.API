using System.ComponentModel.DataAnnotations.Schema;

namespace ApartmentRental.Core.Entities;

public class Apartment : BaseEntity
{
    public decimal Rent { get; set; }
    public int Rooms { get; set; }
    public int Size { get; set; }
    public int Floor { get; set; }
    public bool HasElevator { get; set; }
    
    public int TenantId { get; set; }
    public Tenant Tenant { get; set; }
    
    public int LandlordId { get; set; }
    public Landlord Landlord { get; set; }
    
    public int AddressId { get; set; }
    public Address Address { get; set; }

    public IEnumerable<Image> Images { get; set; }
    
    
}