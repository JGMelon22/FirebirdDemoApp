using FirebirdDemoApp.Interfaces.Services;
using FirebirdDemoApp.Vehicles.Domains.DTOs;
using Microsoft.Extensions.DependencyInjection;

namespace FirebirdDemoApp.IntegrationTests.Infrastructure.Services;

[TestFixture]
public class VehicleTests : BaseIntegrationTest
{
    [Test]
    public async Task Should_CreateVehicle_WhenDataIsCorrect()
    {
        // Arrange
        var service = Scope.ServiceProvider.GetRequiredService<IVehicleService>();

        var vehicle = new VehicleRequest(
            Name: "Mustang",
            Brand: "Ford",
            Price: 35_000.00M,
            ReleaseYear: 2025);

        // Act
        var result = await service.Create(vehicle);

        // Assert
        Assert.That(result.IsSuccess, Is.True);
        Assert.That(result.Value, Is.Not.Null);
        Assert.That(result.Value.Name, Is.EqualTo("Mustang"));
        Assert.That(result.Value.Brand, Is.EqualTo("Ford"));
        Assert.That(result.Value.Price, Is.EqualTo(35_000.00M));
        Assert.That(result.Value.ReleaseYear, Is.EqualTo(2025));
    }

    [Test]
    public async Task Should_ReturnVehiclesList_WhenResultIsSuccess()
    {
        // Arrange
        var service = Scope.ServiceProvider.GetRequiredService<IVehicleService>();

        List<VehicleRequest> vehicles =
        [
            new("Civic", "Honda", 27_500.00M, 2024),
            new("Corolla", "Toyota", 26_000.00M, 2024),
            new("Model 3", "Tesla", 39_990.00M, 2025)
        ];

        foreach (var vehicle in vehicles)
        {
            await service.Create(vehicle);
        }

        // Act
        var result = await service.GetAllAsync();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result.IsSuccess, Is.True);
            Assert.That(result.Value, Is.Not.Null);

            Assert.That(result.Value!.Count(), Is.GreaterThanOrEqualTo(3));
            Assert.That(result.Value!.Any(x => x.Name == "Civic"), Is.True);
            Assert.That(result.Value!.Any(x => x.Name == "Model 3"), Is.True);
        });
    }

    [Test]
    public async Task Should_ReturnSingleVehicle_WhenPassedIdExists()
    {
        // Arrange
        var service = Scope.ServiceProvider.GetRequiredService<IVehicleService>();

        var vehicle = new VehicleRequest(
            Name: "RAV4",
            Brand: "Toyota",
            Price: 32_000.00M,
            ReleaseYear: 2024
        );

        var createdVehicle = await service.Create(vehicle);
        int id = createdVehicle.Value!.Id;

        // Act
        var result = await service.GetByIdAsync(id);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result.IsSuccess, Is.True);
            Assert.That(result.Value, Is.Not.Null);
            Assert.That(result.Value!.Name, Is.EqualTo("RAV4"));
        });
    }

    [Test]
    public async Task Should_UpdatedVehicle_WhenPassedIdExists()
    {
        // Arrange
        var service = Scope.ServiceProvider.GetRequiredService<IVehicleService>();

        var vehicle = new VehicleRequest(
            Name: "Accord",
            Brand: "Honda",
            Price: 28_500.00M,
            ReleaseYear: 2023
        );

        var vehicleUpdated = new VehicleRequest(
            Name: "Camaro",
            Brand: "Chevrolet",
            Price: 25_000.00M,
            ReleaseYear: 2022);

        var createdVehicle = await service.Create(vehicle);

        // Act
        var result = await service.Update(createdVehicle.Value!.Id, vehicleUpdated);

        // Assert
        Assert.That(result.IsSuccess, Is.True);
        Assert.That(result.Value, Is.Not.Null);
        Assert.That(result.Value.Name, Is.EqualTo("Camaro"));
    }

    [Test]
    public async Task Should_RemoveVehicle_WhenIdIsPresent()
    {
        // Arrange
        var service = Scope.ServiceProvider.GetRequiredService<IVehicleService>();

        var vehicle = new VehicleRequest(
            Name: "Mustang",
            Brand: "Ford",
            Price: 35_000.00M,
            ReleaseYear: 2025);

        var createdVehicle = await service.Create(vehicle);
        int id = createdVehicle.Value!.Id;

        // Act
        var result = await service.DeleteAsync(id);

        // Assert
        Assert.That(result.IsSuccess, Is.True);
    }
}