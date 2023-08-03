using Mercury.Shared.Services;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace Mercury.Client
{
    public class ApiFakeAuthenticationStateProvider : AuthenticationStateProvider, ICustomAuthenticationStateProvider
    {
        private ClaimsPrincipal _user;

        public ApiFakeAuthenticationStateProvider()
        {
            // Start with a default authenticated user
            _user = CreateAuthenticatedUser("Test User");
        }

        public async Task<string?> GetUserId()
        {
            return "1";
        }
        public override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            return Task.FromResult(new AuthenticationState(_user));
        }

        public void MarkUserAsAuthenticated(string name)
        {
            _user = CreateAuthenticatedUser(name);
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        public void MarkUserAsLoggedOut()
        {
            _user = new ClaimsPrincipal(new ClaimsIdentity());
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        private ClaimsPrincipal CreateAuthenticatedUser(string name)
        {
            var identity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, name) }, "Fake authentication type");
            return new ClaimsPrincipal(identity);
        }
    }

}
