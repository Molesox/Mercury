using Blazored.LocalStorage;
using Mercury.Client;
using Mercury.Client.Services;
using Mercury.Shared.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

// Creates a default WebAssemblyHostBuilder which is used to configure a Blazor application's services and pipeline.
var builder = WebAssemblyHostBuilder.CreateDefault(args);

// Adds the root component of the application and specifies its CSS selector.
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Adds and configures services to the builder.
builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
});


// Adds Blazored LocalStorage services to the DI container.
builder.Services.AddBlazoredLocalStorage();

// Adds AuthorizationCore services to the DI container.
builder.Services.AddAuthorizationCore();

#if !DEBUG
builder.Services.AddScoped<AuthenticationStateProvider,
    ApiFakeAuthenticationStateProvider>();
builder.Services.AddScoped<ICustomAuthenticationStateProvider, ApiFakeAuthenticationStateProvider>();
#else

builder.Services.AddScoped<AuthenticationStateProvider,
    ApiAuthenticationStateProvider>();
    builder.Services.AddScoped<ICustomAuthenticationStateProvider, ApiAuthenticationStateProvider>();
#endif



// Adds the AuthService to the DI container.
builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddScoped<AspNetUserManager>();
builder.Services.AddScoped<AspNetRoleManager>();
builder.Services.AddScoped<PersonManager>();

builder.Services.AddScoped<ThemeService>();

await builder.Build().RunAsync();

