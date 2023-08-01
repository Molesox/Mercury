using Mercury.Shared.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace Mercury.Server.Controllers.Identity
{
    /// <summary>
    /// API controller for handling user logins.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly SignInManager<IdentityUser> _signInManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="LoginController"/> class.
        /// </summary>
        /// <param name="configuration">An instance of IConfiguration for accessing application settings.</param>
        /// <param name="signInManager">An instance of the SignInManager for managing IdentityUser sign-ins.</param>
        public LoginController(IConfiguration configuration,
                               SignInManager<IdentityUser> signInManager)
        {
            _configuration = configuration;
            _signInManager = signInManager;
        }

        /// <summary>
        /// Logs in a user with the provided login model.
        /// </summary>
        /// <param name="login">The login model containing user data.</param>
        /// <returns>A task result that represents the asynchronous operation, containing the ActionResult of the login attempt. A successful login returns a JWT token.</returns>
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginModel login)
        {
            var result = await _signInManager.PasswordSignInAsync(login.Email, login.Password, false, false);

            if (result.IsLockedOut) return CreateLoginFailureResponse("User is currently locked out.");
            if (result.IsNotAllowed) return CreateLoginFailureResponse("User is not allowed to login.");
            if (result.RequiresTwoFactor) return CreateLoginFailureResponse("This account requires two-factor authentication.");
            if (!result.Succeeded) return CreateLoginFailureResponse("Wrong credentials");
            var claims = new[] { new Claim(ClaimTypes.Name, login.Email) };
            string? jwtSecurityKey = _configuration.GetValue<string>("JwtSecurityKey");

            if (jwtSecurityKey is null) return CreateLoginFailureResponse();
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecurityKey));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiry = DateTime.Now.AddDays(Convert.ToInt32(_configuration["JwtExpiryInDays"]));

            var token = new JwtSecurityToken(
                _configuration["JwtIssuer"],
                _configuration["JwtAudience"],
                claims,
                expires: expiry,
                signingCredentials: creds
            );

            return Ok(new LoginResult { IsSuccesful = true, Token = new JwtSecurityTokenHandler().WriteToken(token) });
        }

        private BadRequestObjectResult CreateLoginFailureResponse(string? message = null)
        {
            return BadRequest(new LoginResult(message ?? "Something bad happend, please try again later.") { IsSuccesful = false });
        }
    }
}
