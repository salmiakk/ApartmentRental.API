using ApartmentRental.Infrastructure.Context;
using ApartmentRental.Infrastructure.Entities;
using ApartmentRental.Infrastructure.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace ApartmentRental.Infrastructure.Repository;

public class ApartmentRepository : IApartmentRepository
{    
    private readonly MainContext _mainContext;

    public ApartmentRepository(MainContext mainContext)
    {
        _mainContext = mainContext;
    }
    public async Task<IEnumerable<Apartment>> GetAllAsync()
    {
        var apartments = await _mainContext.Apartment.ToListAsync();

        foreach (var apartment in apartments)
        {
            await _mainContext.Entry(apartment).Reference(x => x.Address).LoadAsync();
        }

        return apartments;
    }

    public async Task<Apartment> GetById(int id)
    {
        var apartment = await _mainContext.Apartment.SingleOrDefaultAsync(x => x.Id == id);

        if (apartment != null)
        {
            await _mainContext.Entry(apartment).Reference(x => x.Address).LoadAsync();
            return apartment;
        }

        throw new EntityNotFoundException();
    }

    public async Task AddAsync(Apartment entity)
    {
        var apartmentToAdd = await _mainContext.Apartment.SingleOrDefaultAsync(x => x.Address == entity.Address);
        if (apartmentToAdd != null) throw new EntityAlreadyExistsException();
        entity.DateOfCreation = DateTime.UtcNow;
        await _mainContext.AddAsync(entity);
        await _mainContext.SaveChangesAsync();
    }

    public async Task Update(Apartment entity)
    {
        var apartmentsToUpdate = await _mainContext.Apartment.SingleOrDefaultAsync(x => x.Id == entity.Id);
        if (apartmentsToUpdate == null) throw new EntityNotFoundException();
        
        apartmentsToUpdate.Floor = entity.Floor;
        apartmentsToUpdate.HasElevator = entity.HasElevator;
        apartmentsToUpdate.Rent = entity.Rent;
        apartmentsToUpdate.Size = entity.Size;
        apartmentsToUpdate.Rooms = entity.Rooms;
        apartmentsToUpdate.DateOfUpdate = DateTime.UtcNow;
        
        await _mainContext.SaveChangesAsync();
    }

    public async Task DeleteById(int id)
    {
        var apartmentsToDelete = await _mainContext.Apartment.SingleOrDefaultAsync(x => x.Id == id);
        if (apartmentsToDelete != null)
        {
            _mainContext.Apartment.Remove(apartmentsToDelete);
            await _mainContext.SaveChangesAsync();
        }

        throw new EntityNotFoundException();
    }
}