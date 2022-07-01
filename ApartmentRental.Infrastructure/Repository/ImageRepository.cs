using ApartmentRental.Infrastructure.Context;
using ApartmentRental.Infrastructure.Entities;
using ApartmentRental.Infrastructure.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace ApartmentRental.Infrastructure.Repository;

public class ImageRepository : IImageRepository
{
    private readonly MainContext _mainContext;

    public ImageRepository(MainContext mainContext)
    {
        _mainContext = mainContext;
    }

    public async Task<IEnumerable<Image>> GetAllAsync()
    {
        var images = await _mainContext.Image.ToListAsync();
        foreach (var image in images)
        {
            await _mainContext.Entry(image).Reference(x => x.Apartment).LoadAsync();
        }

        return images;
    }

    public async Task<Image> GetById(int id)
    {
        var image = await _mainContext.Image.SingleOrDefaultAsync(x => x.Id == id);
        if (image != null)
        {
            await _mainContext.Entry(image).Reference(x => x.Apartment).LoadAsync();
            return image;
        }

        throw new EntityNotFoundException();
    }

    public async Task AddAsync(Image entity)
    {
        var imagesToAdd = await _mainContext.Image.SingleOrDefaultAsync(x => x.Id == entity.Id);
        if (imagesToAdd != null) throw new EntityAlreadyExistsException();
        entity.DateOfCreation = DateTime.UtcNow;
        await _mainContext.AddAsync(entity);
        await _mainContext.SaveChangesAsync();
    }

    public async Task Update(Image entity)
    {
        var imagesToUpdate = await _mainContext.Image.SingleOrDefaultAsync(x => x.Id == entity.Id);
        if (imagesToUpdate == null)
        {
            throw new EntityNotFoundException();
        }

        imagesToUpdate.Data = entity.Data;
        imagesToUpdate.DateOfUpdate = DateTime.UtcNow;
        await _mainContext.SaveChangesAsync();
    }

    public async Task DeleteById(int id)
    {
        var imagesToDelete = await _mainContext.Image.SingleOrDefaultAsync(x => x.Id == id);
        if (imagesToDelete != null)
        {
            _mainContext.Image.Remove(imagesToDelete);
            await _mainContext.SaveChangesAsync();
        }

        throw new EntityNotFoundException();
    }
}