using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using System.Text.Json;
using Microsoft.AspNetCore.Components;

namespace bolly.Services;

public class AzureSwaAuthenticationStateProvider : AuthenticationStateProvider
{
    private readonly HttpClient _httpClient;
    private readonly LanguageService _languageService;

    public AzureSwaAuthenticationStateProvider(HttpClient httpClient, LanguageService languageService)
    {
        _httpClient = httpClient;
        _languageService = languageService;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        try
        {
            var response = await _httpClient.GetAsync("/.auth/me");
            
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var authData = JsonSerializer.Deserialize<JsonElement>(content);

                if (authData.TryGetProperty("clientPrincipal", out var clientPrincipal) && clientPrincipal.ValueKind != JsonValueKind.Null)
                {
                    var claims = new List<Claim>();
                    
                    // Extract user identity
                    if (clientPrincipal.TryGetProperty("userDetails", out var userDetails))
                    {
                        claims.Add(new Claim(ClaimTypes.NameIdentifier, userDetails.GetString() ?? ""));
                    }

                    // Extract name
                    if (clientPrincipal.TryGetProperty("userDetails", out var name))
                    {
                        claims.Add(new Claim(ClaimTypes.Name, name.GetString() ?? ""));
                    }

                    // Extract roles
                    if (clientPrincipal.TryGetProperty("userRoles", out var rolesElement))
                    {
                        foreach (var role in rolesElement.EnumerateArray())
                        {
                            claims.Add(new Claim(ClaimTypes.Role, role.GetString() ?? ""));
                        }
                    }

                    var identity = new ClaimsIdentity(claims, "aad");
                    var user = new ClaimsPrincipal(identity);
                    return new AuthenticationState(user);
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error getting authentication state: {ex.Message}");
        }

        return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
    }

    public void Login(string provider = "aad")
    {
        // Redirect to Azure SWA login endpoint
        var returnUrl = new Uri(new Uri(_httpClient.BaseAddress?.ToString() ?? "http://localhost:5111"), "/me").ToString();
        var loginUrl = $"/.auth/login/{provider}?post_login_redirect_uri={Uri.EscapeDataString(returnUrl)}";
        
        // Since we can't use Navigation from AuthenticationStateProvider, we'll use window.location
        throw new PlatformNotSupportedException("Use AuthenticationService.Login() from a component instead");
    }

    public void Logout()
    {
        throw new PlatformNotSupportedException("Use AuthenticationService.Logout() from a component instead");
    }

    private string GetBaseUrl()
    {
        return _httpClient.BaseAddress?.ToString() ?? "http://localhost:5111";
    }
}

public class AuthenticationService
{
    private readonly NavigationManager _navigationManager;

    public AuthenticationService(NavigationManager navigationManager)
    {
        _navigationManager = navigationManager;
    }

    public void Login(string provider = "aad")
    {
        var returnUrl = _navigationManager.ToAbsoluteUri("/me").ToString();
        var loginUrl = $"/.auth/login/{provider}?post_login_redirect_uri={Uri.EscapeDataString(returnUrl)}";
        _navigationManager.NavigateTo(loginUrl, forceLoad: true);
    }

    public void Logout()
    {
        _navigationManager.NavigateTo("/.auth/logout", forceLoad: true);
    }
}
