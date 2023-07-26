using System.Net.Http.Headers;
using System.Net.Http.Json;
using Blazored.LocalStorage;
using Newtonsoft.Json;

using System.Text;
using Microsoft.AspNetCore.Components.Authorization;
using Mercury.Shared.Services;
using Mercury.Shared.Models.Identity;

namespace Mercury.Client.Services
{
    /// <summary>
    /// Represents the authentication service for the application, implementing the <see cref="IAuthService"/> interface.
    /// </summary>
    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;
        private readonly ICustomAuthenticationStateProvider _authenticationStateProvider;
        private readonly ILocalStorageService _localStorage;

        #region Ctor

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthService"/> class.
        /// </summary>
        /// <param name="httpClient">An instance of HttpClient for making HTTP requests.</param>
        /// <param name="authenticationStateProvider">An instance of AuthenticationStateProvider for managing user authentication state.</param>
        /// <param name="localStorage">An instance of ILocalStorageService for handling local storage operations.</param>
        public AuthService(HttpClient httpClient,
                           ICustomAuthenticationStateProvider authenticationStateProvider,
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
            var failedResult = new RegisterResult("Something bad happened, try again later") { IsSuccesful = false };

#if DEBUG
            // In a debug environment, simulate successful registration
            await Task.Delay(1000); // simulate some delay
            return new RegisterResult() { IsSuccesful = true };
#else

            try
            {
                var registerAsJson = JsonConvert.SerializeObject(registerModel); ;

                var response = await _httpClient.PostAsync("api/Accounts", new StringContent(registerAsJson, Encoding.UTF8, "application/json"));
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();
                var registerResult = JsonConvert.DeserializeObject<RegisterResult>(content);

                return registerResult ?? failedResult;
            }
            catch (Exception e)
            {
                // log exception here
                return failedResult;
            }
#endif
        }

        /// <summary>
        /// Logs in a user based on the provided login model.
        /// </summary>
        /// <param name="loginModel">The login model containing user credentials.</param>
        /// <returns>A task result that represents the asynchronous operation, containing the login result. A successful login stores the JWT token in local storage and sets the user as authenticated.</returns>
        public async Task<LoginResult> Login(LoginModel loginModel)
        {
#if DEBUG
            // In a debug environment, simulate successful login
            await Task.Delay(1000); // simulate some delay
            var fakeToken = "FakeJwtToken"; // replace with a fake JWT if needed
            await _localStorage.SetItemAsync("authToken", fakeToken);
            ((ICustomAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsAuthenticated(loginModel.Email);
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", fakeToken);
            return new LoginResult( ) { IsSuccesful = true, Token = fakeToken };
#else
            try
            {
                var loginAsJson = JsonConvert.SerializeObject(loginModel);

                var response = await _httpClient.PostAsync("api/Login",
                    new StringContent(loginAsJson, Encoding.UTF8, "application/json"));
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();
                var loginResult = JsonConvert.DeserializeObject<LoginResult>(content);

                if (!response.IsSuccessStatusCode || loginResult is null)
                {
                    return loginResult ?? new LoginResult("Something bad happended, try again later...") { IsSuccesful = false };
                }

                await _localStorage.SetItemAsync("authToken", loginResult.Token);

                ((ICustomAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsAuthenticated(loginModel.Email);
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", loginResult.Token);

                return loginResult;
            }
            catch (Exception e)
            {
                //log exception here
                return new LoginResult("Something bad happended, try again later...") { IsSuccesful = false };
            }
#endif
        }

        /// <summary>
        /// Logs out the currently authenticated user.
        /// </summary>
        /// <returns>A task result that represents the asynchronous operation. The operation removes the JWT token from local storage and sets the user as logged out.</returns>
        public async Task Logout()
        {
            await _localStorage.RemoveItemAsync("authToken");
            ((ICustomAuthenticationStateProvider)_authenticationStateProvider)
                .MarkUserAsLoggedOut();
            _httpClient.DefaultRequestHeaders.Authorization = null;
        }

#endregion
    }
}
