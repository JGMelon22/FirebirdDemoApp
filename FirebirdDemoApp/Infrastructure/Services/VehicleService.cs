using FirebirdDemoApp.Interfaces.Repositories;
using FirebirdDemoApp.Interfaces.Services;
using FirebirdDemoApp.Shared;
using FirebirdDemoApp.Vehicles.Domains.DTOs;
using FirebirdDemoApp.Vehicles.Domains.Mappings;

namespace FirebirdDemoApp.Infrastructure.Services;

public class VehicleService(
    IVehicleRepository repository,
    ILogger<VehicleService> logger,
    MappingExtensions mappingExtensions) : IVehicleService
{
    public async Task<Result<IEnumerable<VehicleResponse>>> GetAllAsync()
    {
        try
        {
            var vehicles = await repository.GetAllAsync();
            var response = mappingExtensions.ToResponse(vehicles);

            logger.LogInformation("A total of {AmountOfVehicles} was found", vehicles.Count);

            return Result<IEnumerable<VehicleResponse>>.Success(response);
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message, "Something went wrong while fetching vehicles");
            return Result<IEnumerable<VehicleResponse>>.Failure(Error.VehicleBadRequest);
        }
    }

    public async Task<Result<VehicleResponse?>> GetByIdAsync(int id)
    {
        try
        {
            var vehicle = await repository.GetByIdAsync(id);

            if (vehicle == null)
            {
                logger.LogWarning("Vehicle with id {Id} was not found", id);
                return Result<VehicleResponse?>.Failure(Error.VehicleNotFound);
            }

            var response = mappingExtensions.ToResponse(vehicle);

            return Result<VehicleResponse?>.Success(response);
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message, "Something went wrong while finding vehicle data");
            return Result<VehicleResponse?>.Failure(Error.VehicleBadRequest);
        }
    }

    public async Task<Result<VehicleResponse>> Create(VehicleRequest vehicle)
    {
        try
        {
            var vehicleToCreate = mappingExtensions.ToDomain(vehicle);

            var createdVehicle = await repository.Create(vehicleToCreate);

            logger.LogInformation("Vehicle successfully created: {Vehicle}", vehicleToCreate);

            var response = mappingExtensions.ToResponse(createdVehicle);

            return Result<VehicleResponse>.Success(response);
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message, "Something went wrong while creating vehicle data");
            return Result<VehicleResponse>.Failure(Error.VehicleBadRequest);
        }
    }

    public async Task<Result<VehicleResponse?>> Update(int id, VehicleRequest vehicle)
    {
        try
        {
            var vehicleToUpdate = mappingExtensions.ToDomain(vehicle);
            vehicleToUpdate.Id = id;

            var updatedVehicle = await repository.Update(vehicleToUpdate);

            if (updatedVehicle == null)
            {
                logger.LogWarning("Vehicle with id {Id} was not found", id);
                return Result<VehicleResponse?>.Failure(Error.VehicleNotFound);
            }

            var response = mappingExtensions.ToResponse(updatedVehicle);

            return Result<VehicleResponse?>.Success(response);
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message, "Something went wrong while updating vehicle data");
            return Result<VehicleResponse?>.Failure(Error.VehicleBadRequest);
        }
    }

    public async Task<Result<bool>> DeleteAsync(int id)
    {
        try
        {
            var vehicle = await repository.GetByIdAsync(id);

            if (vehicle is null)
            {
                logger.LogWarning("Vehicle with id {Id} was not found", id);
                return Result<bool>.Failure(Error.VehicleNotFound);
            }

            var response = await repository.DeleteAsync(id);
            return Result<bool>.Success(response);
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message, "Something went wrong while deleting vehicle data");
            return Result<bool>.Failure(Error.VehicleBadRequest);
        }
    }
}