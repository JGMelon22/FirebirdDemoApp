using FirebirdDemoApp.Infrastructure.Data;
using FirebirdDemoApp.Interfaces.Services;
using Microsoft.Extensions.DependencyInjection;

namespace FirebirdDemoApp.IntegrationTests;

[TestFixture]
public abstract class BaseIntegrationTest
{
    private IntegrationTestWebAppFactory _factory = null!;
    private IServiceScope _scope = null!;
    protected IVehicleService VehicleService = null!;
    protected AppDbContext DbContext = null!;

    [OneTimeSetUp]
    public async Task OneTimeSetup()
    {
        _factory = new IntegrationTestWebAppFactory();
        await _factory.StartContainerAsync();
    }

    [SetUp]
    public void Setup()
    {
        _scope = _factory.Services.CreateScope();
        VehicleService = _scope.ServiceProvider.GetRequiredService<IVehicleService>();
        DbContext = _scope.ServiceProvider.GetRequiredService<AppDbContext>();
    }

    [TearDown]
    public async Task TearDown()
    {
        await DbContext.DisposeAsync();
        _scope.Dispose();
    }

    [OneTimeTearDown]
    public async Task OneTimeTearDown()
    {
        await _factory.StopContainerAsync();
        await _factory.DisposeAsync();
    }
}