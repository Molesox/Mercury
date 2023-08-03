using Blazored.LocalStorage;
using Mercury.Shared.Services;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;

namespace Mercury.Client
{
    /// <summary>
    /// Represents the API authentication state provider for the application, derived from <see cref="AuthenticationStateProvider"/>.
    /// </summary>
    public class ApiAuthenticationStateProvider : AuthenticationStateProvider, ICustomAuthenticationStateProvider
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorage;

        #region Ctor

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiAuthenticationStateProvider"/> class.
        /// </summary>
        /// <param name="httpClient">An instance of HttpClient for making HTTP requests.</param>
        /// <param name="localStorage">An instance of ILocalStorageService for handling local storage operations.</param>
        public ApiAuthenticationStateProvider(HttpClient httpClient,
            ILocalStorageService localStorage)
        {
            _httpClient = httpClient;
            _localStorage = localStorage;
        }

        #endregion

        #region Methods

        public async Task<string?> GetUserId()
        {
            var savedToken = await _localStorage.GetItemAsync<string>("authToken");

            if (string.IsNullOrWhiteSpace(savedToken))
            {
                return null;
            }

            var claims = ParseClaimsFromJwt(savedToken);

            return claims?.FirstOrDefault(x => x.Type == "id")?.Value;
        }

        /// <summary>
        /// Gets the authentication state of the current user.
        /// </summary>
        /// <returns>A task result that represents the asynchronous operation, containing the AuthenticationState of the user.</returns>
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {

            var savedToken = await _localStorage.GetItemAsync<string>("authToken");

            if (string.IsNullOrWhiteSpace(savedToken))
            {
                return new AuthenticationState(new ClaimsPrincipal(
                    new ClaimsIdentity()));
            }

            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("bearer", savedToken);

            return new AuthenticationState(new ClaimsPrincipal(
                new ClaimsIdentity(ParseClaimsFromJwt(savedToken), "jwt")));
        }

        /// <summary>
        /// Marks a user as authenticated and notifies the authentication state provider.
        /// </summary>
        /// <param name="email">The email of the authenticated user.</param>
        public void MarkUserAsAuthenticated(string email)
        {
            var authenticatedUser = new ClaimsPrincipal(new ClaimsIdentity(new[]
                { new Claim(ClaimTypes.Name, email) }, "apiauth"));
            var authState = Task.FromResult(new AuthenticationState(authenticatedUser));
            NotifyAuthenticationStateChanged(authState);
        }

        /// <summary>
        /// Marks the current user as logged out and notifies the authentication state provider.
        /// </summary>
        public void MarkUserAsLoggedOut()
        {
            var anonymousUser = new ClaimsPrincipal(new ClaimsIdentity());
            var authState = Task.FromResult(new AuthenticationState(anonymousUser));
            NotifyAuthenticationStateChanged(authState);
        }

        #endregion

        #region Helpers

        /// <summary>
        /// Parses JWT claims from the provided token.
        /// </summary>
        /// <param name="jwt">The JWT to parse.</param>
        /// <returns>An IEnumerable collection of Claim representing the claims in the token.</returns>
        private IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
        {
            var claims = new List<Claim>();
            var payload = jwt.Split('.').ElementAtOrDefault(1);
            if(payload is null) return claims;
            var jsonBytes = ParseBase64WithoutPadding(payload);
            var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);

            if (keyValuePairs is null) return claims;

            keyValuePairs.TryGetValue(ClaimTypes.Role, out object? roles);

            if (roles is not null)
            {
                string rolesStr = roles.ToString()!;
                if (rolesStr.Trim().StartsWith("["))
                {
                    var parsedRoles = JsonSerializer.Deserialize<string[]>(rolesStr);
                    if (parsedRoles is not null)
                        foreach (var parsedRole in parsedRoles)
                        {
                            claims.Add(new Claim(ClaimTypes.Role, parsedRole));
                        }
                }
                else
                {
                    claims.Add(new Claim(ClaimTypes.Role, rolesStr));
                }

                keyValuePairs.Remove(ClaimTypes.Role);
            }

            claims.AddRange(keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString() ?? "")));

            return claims;
        }

        /// <summary>
        /// Parses a Base64 string without padding.
        /// </summary>
        /// <param name="base64">The Base64 string to parse.</param>
        /// <returns>A byte array representing the decoded Base64 string.</returns>
        private byte[] ParseBase64WithoutPadding(string base64)
        {
            switch (base64.Length % 4)
            {
                case 2: base64 += "=="; break;
                case 3: base64 += "="; break;
            }
            return Convert.FromBase64String(base64);
        }

        #endregion
    }

}
