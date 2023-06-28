using Mercury.Shared.Identity;

namespace Mercury.Client.Services
{
    /// <summary>
    /// Defines the contract for an authentication service.
    /// </summary>
    public interface IAuthService
    {
        /// <summary>
        /// Authenticates a user based on a provided login model.
        /// </summary>
        /// <param name="loginModel">The login model containing user credentials.</param>
        /// <returns>A task result that represents the asynchronous operation, containing the login result.</returns>
        Task<LoginResult> Login(LoginModel loginModel);

        /// <summary>
        /// Logs out the currently authenticated user.
        /// </summary>
        /// <returns>A task result that represents the asynchronous operation.</returns>
        Task Logout();

        /// <summary>
        /// Registers a new user based on a provided registration model.
        /// </summary>
        /// <param name="registerModel">The registration model containing user data.</param>
        /// <returns>A task result that represents the asynchronous operation, containing the registration result.</returns>
        Task<RegisterResult> Register(RegisterModel registerModel);
    }
}