using ApartmentRental.Core.Entities;
using ApartmentRental.Infrastructure.Context;
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

    public async Task<IEnumerable<Address>> GetAll()
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

    public async Task Add(Address entity)
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
}