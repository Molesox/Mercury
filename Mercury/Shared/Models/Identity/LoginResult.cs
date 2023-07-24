using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mercury.Shared.Base;

namespace Mercury.Shared.Models.Identity
{
    /// <summary>
    /// Represents the result of a login attempt.
    /// </summary>
    public class LoginResult : ResponseBase
    {
        public LoginResult(string errorMessage = "") : base(errorMessage)
        {

        }
        /// <summary>
        /// Gets or sets the token generated after a successful login attempt.
        /// </summary>
        /// <value>A string representing the token, or null if the login attempt was unsuccessful.</value>
        public string? Token { get; set; }
    }

}
