using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using bolly;
using MudBlazor.Services;
using MudBlazor;
using bolly.Services;
using Microsoft.AspNetCore.Components.Authorization;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Add MudBlazor services
builder.Services.AddMudServices(config =>
{
    config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.BottomLeft;
});

// Add HttpClient for data loading
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

// Add application services
builder.Services.AddScoped<IPropertyService, PropertyService>();
builder.Services.AddScoped<ITestimonialService, TestimonialService>();
builder.Services.AddScoped<ThemeService>();
builder.Services.AddScoped<LanguageService>();

// Add Authentication services
builder.Services.AddScoped<AuthenticationStateProvider, AzureSwaAuthenticationStateProvider>();
builder.Services.AddScoped<AuthenticationService>();
builder.Services.AddAuthorizationCore();

await builder.Build().RunAsync();