using ApartmentRental.Infrastructure.Context;
using ApartmentRental.Infrastructure.Entities;
using ApartmentRental.Infrastructure.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace ApartmentRental.Infrastructure.Repository;

public class TenantRepository : ITenantRepository
{
    private readonly MainContext _mainContext;

    public TenantRepository(MainContext mainContext)
    {
        _mainContext = mainContext;
    }

    public async Task<IEnumerable<Tenant>> GetAllAsync()
    {
        var tenants = await _mainContext.Tenant.ToListAsync();
        foreach (var tenant in tenants)
        {
            await _mainContext.Entry(tenant).Reference(x => x.Account).LoadAsync();
            await _mainContext.Entry(tenant).Reference(x => x.Apartment).LoadAsync();
        }

        return tenants;
    }

    public async Task<Tenant> GetById(int id)
    {
        var tenant = await _mainContext.Tenant.SingleOrDefaultAsync(x => x.Id == id);
        if (tenant != null)
        {
            await _mainContext.Entry(tenant).Reference(x => x.Account).LoadAsync();
            await _mainContext.Entry(tenant).Reference(x => x.Apartment).LoadAsync();
            return tenant;
        }
        throw new EntityNotFoundException();

    }

    public async Task AddAsync(Tenant entity)
    {
        var tenantsToAdd = await _mainContext.Tenant.SingleOrDefaultAsync(x => x.Id == entity.Id);
        if (tenantsToAdd != null) throw new EntityAlreadyExistsException();
        entity.DateOfCreation = DateTime.UtcNow;
        await _mainContext.AddAsync(entity);
        await _mainContext.SaveChangesAsync();
    }

    public async Task Update(Tenant entity)
    {
        var tenantsToUpdate = await _mainContext.Tenant.SingleOrDefaultAsync(x => x.Id == entity.Id);
        if (tenantsToUpdate == null) throw new EntityNotFoundException();

        tenantsToUpdate.Account = entity.Account;
        tenantsToUpdate.AccountId = entity.AccountId;
        tenantsToUpdate.Apartment = entity.Apartment;
        tenantsToUpdate.DateOfUpdate = DateTime.UtcNow;
        
        await _mainContext.SaveChangesAsync();
    }

    public async Task DeleteById(int id)
    {
        var tenantsToDelete = await _mainContext.Tenant.SingleOrDefaultAsync(x => x.Id == id);
        if (tenantsToDelete != null)
        {
            _mainContext.Tenant.Remove(tenantsToDelete);
            await _mainContext.SaveChangesAsync();
        }

        throw new EntityNotFoundException();
        
    }
}