using ApiProject.Client.Web;
using ApiProject.Client.Web.Features.PhoneCases;
using ApiProject.Client.Web.Features.Users;
using ApiProject.Client.Web.Services;
using Blazored.LocalStorage;
using FluentValidation;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Add MudBlazor services
builder.Services.AddMudServices();

// Add Fluent validators for models
builder.Services.AddValidatorsFromAssembly(typeof(ApiProject.Shared.Users.Validators.LoginUserCommandValidator).Assembly);

// Add user api service
builder.Services.AddHttpClient<UserService>(config =>
    config.BaseAddress = new($"{builder.Configuration["ApiUrl"]!}/user/"))
    .AddHttpMessageHandler<AuthHandler>();

// Add auth api service
builder.Services.AddHttpClient<AuthService>(config =>
    config.BaseAddress = new($"{builder.Configuration["ApiUrl"]!}/user/auth/"));


// Add phone cases api service
builder.Services.AddHttpClient<PhoneCaseService>(config =>
    config.BaseAddress = new($"{builder.Configuration["ApiUrl"]!}/phone-case/"))
    .AddHttpMessageHandler<AuthHandler>();


// Add veil service
builder.Services.AddScoped<VeilService>();

builder.Services.AddAuthorizationCore();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<AuthenticationStateProvider, AuthStateProvider>();
builder.Services.AddScoped<AuthHandler>();

// Add memory cache
builder.Services.AddMemoryCache();

// Add locale storage service
builder.Services.AddBlazoredLocalStorage();



await builder.Build().RunAsync();
