using ApartmentRental.Core.Entities;

namespace ApartmentRental.Infrastructure.Repository;

public class ApartmentRepository : IApartmentRepository
{
    public Task<IEnumerable<Apartment>> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task<Apartment> GetById(int id)
    {
        throw new NotImplementedException();
    }

    public Task Add(Apartment entity)
    {
        throw new NotImplementedException();
    }

    public Task Update(Apartment entity)
    {
        throw new NotImplementedException();
    }

    public Task DeleteById(int id)
    {
        throw new NotImplementedException();
    }
}