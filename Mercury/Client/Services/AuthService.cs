using Mercury.Shared.Identity;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Blazored.LocalStorage;
using System.Text.Json;
using System.Text;
using Microsoft.AspNetCore.Components.Authorization;

namespace Mercury.Client.Services
{
    /// <summary>
    /// Represents the authentication service for the application, implementing the <see cref="IAuthService"/> interface.
    /// </summary>
    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;
        private readonly AuthenticationStateProvider _authenticationStateProvider;
        private readonly ILocalStorageService _localStorage;

        #region Ctor

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthService"/> class.
        /// </summary>
        /// <param name="httpClient">An instance of HttpClient for making HTTP requests.</param>
        /// <param name="authenticationStateProvider">An instance of AuthenticationStateProvider for managing user authentication state.</param>
        /// <param name="localStorage">An instance of ILocalStorageService for handling local storage operations.</param>
        public AuthService(HttpClient httpClient,
                           AuthenticationStateProvider authenticationStateProvider,
                           ILocalStorageService localStorage)
        {
            _httpClient = httpClient;
            _authenticationStateProvider = authenticationStateProvider;
            _localStorage = localStorage;
        }

        #endregion

        #region Interface Implementation

        /// <summary>
        /// Registers a new user based on the provided registration model.
        /// </summary>
        /// <param name="registerModel">The registration model containing user data.</param>
        /// <returns>A task result that represents the asynchronous operation, containing the registration result.</returns>
        public async Task<RegisterResult> Register(RegisterModel registerModel)
        {
            var response = await _httpClient.PostAsJsonAsync("api/accounts", registerModel);
            return await response.Content.ReadFromJsonAsync<RegisterResult>();
        }

        /// <summary>
        /// Logs in a user based on the provided login model.
        /// </summary>
        /// <param name="loginModel">The login model containing user credentials.</param>
        /// <returns>A task result that represents the asynchronous operation, containing the login result. A successful login stores the JWT token in local storage and sets the user as authenticated.</returns>
        public async Task<LoginResult> Login(LoginModel loginModel)
        {
            var loginAsJson = JsonSerializer.Serialize(loginModel);
            var response = await _httpClient.PostAsync("api/Login",
                new StringContent(loginAsJson, Encoding.UTF8, "application/json"));
            var loginResult = JsonSerializer.Deserialize<LoginResult>(
                await response.Content.ReadAsStringAsync(),
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            if (!response.IsSuccessStatusCode)
            {
                return loginResult;
            }

            await _localStorage.SetItemAsync("authToken", loginResult.Token);
            ((ApiAuthenticationStateProvider)_authenticationStateProvider)
                .MarkUserAsAuthenticated(loginModel.Email);
            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("bearer", loginResult.Token);

            return loginResult;
        }

        /// <summary>
        /// Logs out the currently authenticated user.
        /// </summary>
        /// <returns>A task result that represents the asynchronous operation. The operation removes the JWT token from local storage and sets the user as logged out.</returns>
        public async Task Logout()
        {
            await _localStorage.RemoveItemAsync("authToken");
            ((ApiAuthenticationStateProvider)_authenticationStateProvider)
                .MarkUserAsLoggedOut();
            _httpClient.DefaultRequestHeaders.Authorization = null;
        }

        #endregion
    }
}
