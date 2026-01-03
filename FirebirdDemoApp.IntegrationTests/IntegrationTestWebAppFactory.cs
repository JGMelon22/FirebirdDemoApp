using FirebirdDemoApp.Infrastructure.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Testcontainers.FirebirdSql;

namespace FirebirdDemoApp.IntegrationTests;

public class IntegrationTestWebAppFactory : WebApplicationFactory<Program>
{
    private readonly FirebirdSqlContainer _dbContainer = new FirebirdSqlBuilder("jacobalberty/firebird:v4.0")
        .WithUsername("cool_user_test")
        .WithPassword("Strong_password_123!")
        .WithDatabase("app.fdb")
        .Build();

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            var descriptorType =
                typeof(DbContextOptions<AppDbContext>);

            var descriptor = services
                .SingleOrDefault(s => s.ServiceType == descriptorType);

            if (descriptor is not null)
                services.Remove(descriptor);

            services.AddDbContext<AppDbContext>(options =>
                options.UseFirebird(_dbContainer.GetConnectionString()));
        });
    }

    public async Task StartContainerAsync()
    {
        await _dbContainer.StartAsync();
        await using var scope = Services.CreateAsyncScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        await dbContext.Database.MigrateAsync();
    }

    public Task StopContainerAsync()
        => _dbContainer.StopAsync();
}