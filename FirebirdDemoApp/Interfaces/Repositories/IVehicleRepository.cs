using FirebirdDemoApp.Vehicles.Domains.Entities;

namespace FirebirdDemoApp.Interfaces.Repositories;

public interface IVehicleRepository
{
    Task<ICollection<Vehicle>> GetAllAsync();
    Task<Vehicle?> GetByIdAsync(int id);
    Task<Vehicle> CreateAsync(Vehicle vehicle);
    Task<Vehicle?> UpdateAsync(Vehicle vehicle);
    Task<bool> DeleteAsync(int id);
}