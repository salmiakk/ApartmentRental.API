using ApartmentRental.Infrastructure.Context;
using ApartmentRental.Infrastructure.Entities;
using ApartmentRental.Infrastructure.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace ApartmentRental.Infrastructure.Repository;

public class LandlordRepository : ILandlordRepository
{
    private readonly MainContext _mainContext;

    public  LandlordRepository(MainContext mainContext)
    {
        _mainContext = mainContext;
    }

    public async Task<IEnumerable<Landlord>> GetAllAsync()
    {
        var landlords = await _mainContext.Landlord.ToListAsync();

        foreach (var landlord in landlords)
        {
            await _mainContext.Entry(landlord).Reference(x=> x.Account).LoadAsync();
            await _mainContext.Entry(landlord).Reference(x=> x.Apartments).LoadAsync();
        }
        return landlords;
    }

    public async Task<Landlord> GetById(int id)
    {
        var landlord = await _mainContext.Landlord.SingleOrDefaultAsync(x => x.Id == id);
        if (landlord != null)
        {
            await _mainContext.Entry(landlord).Reference(x => x.Account).LoadAsync();
            await _mainContext.Entry(landlord).Reference(x => x.Apartments).LoadAsync();
            return landlord;
        }

        throw new EntityNotFoundException();

    }

    public async Task AddAsync(Landlord entity)
    {
        var landlordsToAdd = await _mainContext.Landlord.SingleOrDefaultAsync(x => x.Id == entity.Id);
        if (landlordsToAdd != null) throw new EntityAlreadyExistsException();
        entity.DateOfCreation = DateTime.UtcNow;
        await _mainContext.AddAsync(entity);
        await _mainContext.SaveChangesAsync();
    }

    public async Task Update(Landlord entity)
    {
        var landlordsToUpdate = await _mainContext.Landlord.SingleOrDefaultAsync(x => x.Id == entity.Id);
        if (landlordsToUpdate == null) throw new EntityNotFoundException();

        landlordsToUpdate.Account = entity.Account;
        landlordsToUpdate.AccountId = entity.AccountId;
        landlordsToUpdate.Apartments = entity.Apartments;

        await _mainContext.SaveChangesAsync();
    }

    public async Task DeleteById(int id)
    {
        var landlordsToDelete = await _mainContext.Landlord.SingleOrDefaultAsync(x => x.Id == id);
        if (landlordsToDelete != null)
        {
            _mainContext.Landlord.Remove(landlordsToDelete);
            await _mainContext.SaveChangesAsync();
        }
        throw new EntityNotFoundException();
    }
}