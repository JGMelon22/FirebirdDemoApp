using FirebirdDemoApp.Vehicles.Domains.Entities;

namespace FirebirdDemoApp.Interfaces.Repositories;

public interface IVehicleRepository
{
    Task<ICollection<VehicleService>> GetAllAsync();
    Task<VehicleService?> GetByIdAsync(int id);
    Task<VehicleService> Create(VehicleService vehicleService);
    Task<VehicleService?> Update(VehicleService vehicleService);
    Task<bool> DeleteAsync(int id);
}