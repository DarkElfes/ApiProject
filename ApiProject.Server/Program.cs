using ApiProject.Server.Data;
using ApiProject.Server.Users;
using Carter;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Add database in memory
builder.Services.AddDbContext<AppDbContext>(o => o.UseInMemoryDatabase("AppDb"));

// Add token provider 
builder.Services.AddScoped<TokenProvider>();

// Add MediatR 
builder.Services.AddMediatR(config => config.RegisterServicesFromAssembly(typeof(Program).Assembly));

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(options =>
    {
        options.AllowAnyOrigin()
            .AllowAnyHeader();
    });
});


// Add Carter
builder.Services.AddCarter();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseCors();

// Map Carter
app.MapCarter();

app.UseHttpsRedirection();

app.Run();
