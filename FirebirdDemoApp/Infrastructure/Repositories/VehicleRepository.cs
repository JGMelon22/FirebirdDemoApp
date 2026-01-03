using FirebirdDemoApp.Infrastructure.Data;
using FirebirdDemoApp.Interfaces.Repositories;
using FirebirdDemoApp.Vehicles.Domains.Entities;
using Microsoft.EntityFrameworkCore;

namespace FirebirdDemoApp.Infrastructure.Repositories;

public class VehicleRepository(AppDbContext dbContext) : IVehicleRepository
{
    public async Task<ICollection<Vehicle>> GetAllAsync()
    {
        return await dbContext.Vehicles
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Vehicle?> GetByIdAsync(int id)
    {
        return await dbContext.Vehicles
            .FindAsync(id);
    }

    public async Task<Vehicle> Create(Vehicle vehicle)
    {
        await dbContext.Vehicles.AddAsync(vehicle);
        await dbContext.SaveChangesAsync();
        return vehicle;
    }

    public async Task<Vehicle?> Update(Vehicle vehicle)
    {
        var vehicleToUpdate = await dbContext.Vehicles.FindAsync(vehicle.Id);

        if (vehicleToUpdate == null)
            return null;

        vehicleToUpdate.Name = vehicle.Name;
        vehicleToUpdate.Brand = vehicle.Brand;
        vehicleToUpdate.Price = vehicle.Price;
        vehicleToUpdate.ReleaseYear = vehicle.ReleaseYear;

        await dbContext.SaveChangesAsync();
        return vehicle;
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