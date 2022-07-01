using ApartmentRental.Infrastructure.Context;
using ApartmentRental.Infrastructure.Entities;
using ApartmentRental.Infrastructure.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace ApartmentRental.Infrastructure.Repository;

public class AddressRepository : IAddressRepository
{
    private readonly MainContext _mainContext;

    public AddressRepository(MainContext mainContext)
    {
        _mainContext = mainContext;
    }

    public async Task<IEnumerable<Address>> GetAllAsync()
    {
        var addresses = await _mainContext.Address.ToListAsync();
        return addresses;
    }

    public async Task<Address> GetById(int id)
    {
        var address = await _mainContext.Address.SingleOrDefaultAsync(x => x.Id == id);
        if (address != null)
        {
            return address;
        }

        throw new EntityNotFoundException();
    }

    public async Task AddAsync(Address entity)
    {
        var addressToAdd = await _mainContext.Address.SingleOrDefaultAsync(x => x.Id == entity.Id);
        if (addressToAdd != null) throw new EntityAlreadyExistsException();
        entity.DateOfCreation = DateTime.UtcNow;
        await _mainContext.AddAsync(entity);
        await _mainContext.SaveChangesAsync();
    }

    public async Task Update(Address entity)
    {
        var addressesToUpdate = await _mainContext.Address.SingleOrDefaultAsync(x => x.Id == entity.Id);
        if (addressesToUpdate == null)
        {
            throw new EntityNotFoundException();
        }

        addressesToUpdate.City = entity.City;
        addressesToUpdate.Country = entity.Country;
        addressesToUpdate.Street = entity.Street;
        addressesToUpdate.ApartmentNumber = entity.ApartmentNumber;
        addressesToUpdate.BuildingNumber = entity.BuildingNumber;
        addressesToUpdate.DateOfUpdate = DateTime.UtcNow;

        await _mainContext.SaveChangesAsync();
    }

    public async Task DeleteById(int id)
    {
        var addressesToDelete = await _mainContext.Address.SingleOrDefaultAsync(x => x.Id == id);
        if (addressesToDelete != null)
        {
            _mainContext.Address.Remove(addressesToDelete);
            await _mainContext.SaveChangesAsync();
        }

        throw new EntityNotFoundException();
    }

    public async Task<int> GetAddressIdByItsAttributesAsync(string country, string city, string postcode, string street, string buildingNumber,
        string apartmentNumber)
    {
        var address = await _mainContext.Address.FirstOrDefaultAsync(x =>
            x.Country == country && x.City == city && x.Postcode == postcode && x.Street == street &&
            x.BuildingNumber == buildingNumber && x.ApartmentNumber == apartmentNumber);
        
        return address?.Id ?? 0;
    }

    public async Task<Address> CreateAndGetAsync(Address address)
    {
        address.DateOfCreation = DateTime.UtcNow;
        address.DateOfUpdate = DateTime.UtcNow;
        await _mainContext.AddAsync(address);
        await _mainContext.SaveChangesAsync();

        return address;
    }
}