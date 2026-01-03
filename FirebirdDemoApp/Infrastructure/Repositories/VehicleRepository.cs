using FirebirdDemoApp.Infrastructure.Data;
using FirebirdDemoApp.Interfaces.Repositories;
using FirebirdDemoApp.Vehicles.Domains.Entities;
using Microsoft.EntityFrameworkCore;

namespace FirebirdDemoApp.Infrastructure.Repositories;

public class VehicleRepository(AppDbContext dbContext) : IVehicleRepository
{
    public async Task<ICollection<VehicleService>> GetAllAsync()
    {
        return await dbContext.Vehicles
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<VehicleService?> GetByIdAsync(int id)
    {
        return await dbContext.Vehicles
            .FindAsync(id);
    }

    public async Task<VehicleService> Create(VehicleService vehicleService)
    {
        await dbContext.Vehicles.AddAsync(vehicleService);
        await dbContext.SaveChangesAsync();
        return vehicleService;
    }

    public async Task<VehicleService?> Update(VehicleService vehicleService)
    {
        var vehicleToUpdate = await dbContext.Vehicles.FindAsync(vehicleService.Id);

        if (vehicleToUpdate == null)
            return null;

        vehicleToUpdate.Name = vehicleService.Name;
        vehicleToUpdate.Brand = vehicleService.Brand;
        vehicleToUpdate.Price = vehicleService.Price;
        vehicleToUpdate.ReleaseYear = vehicleService.ReleaseYear;

        await dbContext.SaveChangesAsync();
        return vehicleService;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var vehicleToDelete = await dbContext.Vehicles.FindAsync(id);

        if (vehicleToDelete == null)
            return false;

        dbContext.Vehicles.Remove(vehicleToDelete);
        await dbContext.SaveChangesAsync();

        return true;
    }
}