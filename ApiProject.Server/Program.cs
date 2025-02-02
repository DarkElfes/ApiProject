using ApiProject.Server.Data;
using Carter;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Add database in memory
builder.Services.AddDbContext<AppDbContext>(o => o.UseInMemoryDatabase("AppDb"));

// Add MediatR
builder.Services.AddMediatR(config => config.RegisterServicesFromAssembly(typeof(Program).Assembly));


// Add Carter
builder.Services.AddCarter();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}


// Map Carter
app.MapCarter();

app.UseHttpsRedirection();


app.Run();
