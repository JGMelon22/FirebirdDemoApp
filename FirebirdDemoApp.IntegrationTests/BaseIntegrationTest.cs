using FirebirdDemoApp.Infrastructure.Data;
using FirebirdDemoApp.Interfaces.Services;
using Microsoft.Extensions.DependencyInjection;

namespace FirebirdDemoApp.IntegrationTests;

[TestFixture]
public abstract class BaseIntegrationTest
{
    private IntegrationTestWebAppFactory _factory;
    protected IServiceScope Scope;
    private AppDbContext _dbContext;

    [OneTimeSetUp]
    public async Task OneTimeSetup()
    {
        _factory = new IntegrationTestWebAppFactory();
        await _factory.StartContainerAsync();
    }

    [SetUp]
    public void Setup()
    {
        Scope = _factory.Services.CreateScope();

        _dbContext = Scope.ServiceProvider
            .GetRequiredService<AppDbContext>();
    }

    [TearDown]
    public void TearDown()
    {
        Scope.Dispose();
        _dbContext.Dispose();
    }

    [OneTimeTearDown]
    public async Task OneTimeTearDown()
    {
        await _factory.StopContainerAsync();
        await _factory.DisposeAsync();
    }
}