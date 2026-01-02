using FirebirdDemoApp.Vehicles.Domains.DTOs;
using FirebirdDemoApp.Vehicles.Domains.Entities;
using Riok.Mapperly.Abstractions;

namespace FirebirdDemoApp.Vehicles.Domains.Mappings;

[Mapper]
public partial class MappingExtensions
{
    [MapperIgnoreTarget(nameof(Vehicle.Id))]
    public partial Vehicle ToDomain(VehicleRequest vehicle);

    public partial VehicleResponse ToResponse(Vehicle vehicle);

    public partial IEnumerable<VehicleResponse> ToResponse(IEnumerable<Vehicle> vehicle);
}