﻿using Mercury.Server.Data;
using Mercury.Server.Data.Context;
using Mercury.Shared.Models.AspNetUser;
using Mercury.Shared.Models.Identity;
using Mercury.Shared.Models.Mercury;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Mercury.Server.Controllers.Identity
{
    /// <summary>
    /// API controller for managing user accounts.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RepositoryEF<Person, MercuryContext> _personManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountsController"/> class.
        /// </summary>
        /// <param name="userManager">An instance of the UserManager for managing IdentityUser data.</param>
        public AccountsController(UserManager<IdentityUser> userManager, RepositoryEF<Person, MercuryContext> personManaer )
        {
            _userManager = userManager;
            _personManager = personManaer;
        }

        /// <summary>
        /// Registers a new user with the provided registration model.
        /// </summary>
        /// <param name="model">The registration model containing user data.</param>
        /// <returns>A task result that represents the asynchronous operation, containing the ActionResult of the registration attempt.</returns>
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            var newUser = new IdentityUser { UserName = model.Email, Email = model.Email };
            var result = await _userManager.CreateAsync(newUser, model.Password);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(x => x.Description) ?? new[] { "No description available" };

                return Ok(new RegisterResult
                {
                    IsSuccesful = false,
                    Errors = errors,
                });

            }
            var alrearyExists = (await _personManager.Get(p => p.Emails.Any(e => e.EmailAddress == newUser.Email)));
            if (alrearyExists.Any())
            {
                alrearyExists.First().AppUserID = newUser.Id;
                var updated = await _personManager.Update(alrearyExists.First());
            }

            var _ = await _personManager.Insert(new Person(newUser));

            return Ok(new RegisterResult { IsSuccesful = true });
        }
    }
}
