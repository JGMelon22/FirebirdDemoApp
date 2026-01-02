using FirebirdDemoApp.Vehicles.Domains.Entities;

namespace FirebirdDemoApp.Interfaces.Repositories;

public interface IVehicleRepository
{
    Task<ICollection<Vehicle>> GetAllAsync();
    Task<Vehicle?> GetByIdAsync(int id);
    Task<Vehicle> Create(Vehicle vehicle);
    Task<Vehicle?> Update(Vehicle vehicle);
    Task<bool> Delete(int id);
}