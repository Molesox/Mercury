
using Blazored.LocalStorage;
using Mercury.Client;
using Mercury.Client.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

/// <summary>
/// Creates a default WebAssemblyHostBuilder which is used to configure a Blazor application's services and pipeline.
/// </summary>
var builder = WebAssemblyHostBuilder.CreateDefault(args);

/// <summary>
/// Adds the root component of the application and specifies its CSS selector.
/// </summary>
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

/// <summary>
/// Adds and configures services to the builder.
/// </summary>
builder.Services.AddScoped(sp => new HttpClient
{ BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

/// <summary>
/// Adds Blazored LocalStorage services to the DI container.
/// </summary>
builder.Services.AddBlazoredLocalStorage();

/// <summary>
/// Adds AuthorizationCore services to the DI container.
/// </summary>
builder.Services.AddAuthorizationCore();

/// <summary>
/// Adds the custom AuthenticationStateProvider to the DI container.
/// </summary>
builder.Services.AddScoped<AuthenticationStateProvider,
    ApiAuthenticationStateProvider>();

/// <summary>
/// Adds the AuthService to the DI container.
/// </summary>
builder.Services.AddScoped<IAuthService, AuthService>();

/// <summary>
/// Builds the configured WebAssemblyHost and runs the application.
/// </summary>
await builder.Build().RunAsync();
