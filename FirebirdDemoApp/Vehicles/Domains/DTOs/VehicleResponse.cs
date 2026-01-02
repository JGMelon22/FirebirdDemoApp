namespace FirebirdDemoApp.Vehicles.Domains.DTOs;

public class VehicleResponse
{
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Brand { get; init; } = string.Empty;
    public decimal Price { get; init; }
    public int ReleaseYear { get; init; }
}