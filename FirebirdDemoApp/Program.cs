using FirebirdDemoApp.Infrastructure.Data;
using FirebirdDemoApp.Infrastructure.Repositories;
using FirebirdDemoApp.Infrastructure.Services;
using FirebirdDemoApp.Interfaces.Repositories;
using FirebirdDemoApp.Interfaces.Services;
using FirebirdDemoApp.Middlewares;
using FirebirdDemoApp.Vehicles.Domains.DTOs;
using FirebirdDemoApp.Vehicles.Domains.Mappings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddValidation();

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseFirebird(builder.Configuration.GetConnectionString("Default")));

builder.Services.AddScoped<IVehicleRepository, VehicleRepository>();
builder.Services.AddScoped<IVehicleService, VehicleService>();

builder.Services.AddSingleton<MappingExtensions>();

builder.Services.AddExceptionHandler<BadRequestExceptionHandler>();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/vehicles", async (IVehicleService service) =>
    {
        var response = await service.GetAllAsync();

        return response.Match(
            Results.Ok,
            Results.BadRequest
        );
    })
    .WithName("GetVehicles");

app.MapGet("/vehicles/{id}", async (IVehicleService service, [FromRoute] int id) =>
    {
        var response = await service.GetByIdAsync(id);

        return response.Match(
            Results.Ok,
            Results.NotFound
        );
    })
    .WithName("GetVehicle");

app.MapPost("/vehicles", async (IVehicleService service, VehicleRequest vehicle) =>
    {
        var response = await service.CreateAsync(vehicle);

        return response.Match(
            Results.Ok,
            Results.BadRequest
        );
    })
    .WithName("CreateVehicle");

app.MapPatch("/{id}", async (IVehicleService service, int id, VehicleRequest vehicle) =>
    {
        var response = await service.UpdateAsync(id, vehicle);

        return response.Match(
            Results.Ok,
            Results.NotFound
        );
    })
    .WithName("PatchVehicle");

app.MapDelete("/{id}", async (IVehicleService service, int id) =>
    {
        var response = await service.DeleteAsync(id);

        return response.Match(
            Results.Ok,
            Results.NotFound
        );
    })
    .WithName("DeleteVehicle");

app.UseExceptionHandler();

app.Run();