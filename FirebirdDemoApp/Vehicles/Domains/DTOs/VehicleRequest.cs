using System.ComponentModel.DataAnnotations;

namespace FirebirdDemoApp.Vehicles.Domains.DTOs;

public record VehicleRequest(
    [Required] string Name,
    [Required] string Brand,
    [Required] [Range(1.00, 99999999.99)] decimal Price,
    [Range(1900, int.MaxValue)] [Required] int ReleaseYear
);