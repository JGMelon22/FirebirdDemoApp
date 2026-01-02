namespace FirebirdDemoApp.Shared;

public sealed record Error(int Code, string Description)
{
    public static Error VehicleNotFound => new(100, "Vehicle not found");
    public static Error VehicleBadRequest => new(101, "Vehicle bad request");
}