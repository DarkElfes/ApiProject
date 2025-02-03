using ApiProject.Client.Web;
using ApiProject.Client.Web.Users;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Add MudBlazor services
builder.Services.AddMudServices();

// Add authentication api service
builder.Services.AddHttpClient<AuthService>(config =>
    config.BaseAddress = new(builder.Configuration["ApiUrl"]!));


builder.Services.AddAuthorizationCore();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<AuthStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider, AuthStateProvider>();

// Add memory cache
builder.Services.AddMemoryCache();



await builder.Build().RunAsync();
