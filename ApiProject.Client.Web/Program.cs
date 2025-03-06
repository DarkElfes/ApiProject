using ApiProject.Client.Web;
using ApiProject.Client.Web.Features.PhoneCases;
using ApiProject.Client.Web.Features.Users;
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

// Add authentication api service
builder.Services.AddHttpClient<AuthService>(config =>
    config.BaseAddress = new($"{builder.Configuration["ApiUrl"]!}/auth/"))
    .AddHttpMessageHandler<AuthHandler>();

// Add phone cases api service
builder.Services.AddHttpClient<PhoneCaseService>(config =>
    config.BaseAddress = new($"{builder.Configuration["ApiUrl"]!}/phone-case/"))
    .AddHttpMessageHandler<AuthHandler>(); ;


builder.Services.AddAuthorizationCore();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<AuthenticationStateProvider, AuthStateProvider>();
builder.Services.AddScoped<AuthHandler>();

// Add memory cache
builder.Services.AddMemoryCache();

// Add locale storage service
builder.Services.AddBlazoredLocalStorage();



await builder.Build().RunAsync();
