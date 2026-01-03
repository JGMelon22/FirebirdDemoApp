using FirebirdDemoApp.Vehicles.Domains.DTOs;
using FirebirdDemoApp.Vehicles.Domains.Entities;
using Riok.Mapperly.Abstractions;

namespace FirebirdDemoApp.Vehicles.Domains.Mappings;

[Mapper]
public partial class MappingExtensions
{
    [MapperIgnoreTarget(nameof(VehicleService.Id))]
    public partial VehicleService ToDomain(VehicleRequest vehicle);

    public partial VehicleResponse ToResponse(VehicleService vehicleService);

    public partial IEnumerable<VehicleResponse> ToResponse(IEnumerable<VehicleService> vehicle);
}