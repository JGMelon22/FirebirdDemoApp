using FirebirdDemoApp.Shared;
using FirebirdDemoApp.Vehicles.Domains.DTOs;

namespace FirebirdDemoApp.Interfaces.Services;

public interface IVehicleService
{
    Task<Result<IEnumerable<VehicleResponse>>> GetAllAsync();
    Task<Result<VehicleResponse?>> GetByIdAsync(int id);
    Task<Result<VehicleResponse>> CreateAsync(VehicleRequest vehicle);
    Task<Result<VehicleResponse?>> UpdateAsync(int id, VehicleRequest vehicle);
    Task<Result<Unit>> DeleteAsync(int id);
}