using FirebirdDemoApp.Shared;
using FirebirdDemoApp.Vehicles.Domains.DTOs;

namespace FirebirdDemoApp.Interfaces.Services;

public interface IVehicleService
{
    Task<Result<IEnumerable<VehicleResponse>>> GetAllAsync();
    Task<Result<VehicleResponse?>> GetByIdAsync(int id);
    Task<Result<VehicleResponse>> Create(VehicleRequest vehicle);
    Task<Result<VehicleResponse?>> Update(int id, VehicleRequest vehicle);
    Task<Result<bool>> DeleteAsync(int id);
}