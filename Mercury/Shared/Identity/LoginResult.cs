using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mercury.Shared.Identity
{
    /// <summary>
    /// Represents the result of a login attempt.
    /// </summary>
    public class LoginResult
    {
        /// <summary>
        /// Gets or sets a value indicating whether the login attempt was successful.
        /// </summary>
        /// <value>True if the login attempt was successful, false otherwise.</value>
        public bool Successful { get; set; }

        /// <summary>
        /// Gets or sets an error message in case the login attempt was unsuccessful.
        /// </summary>
        /// <value>A string representing an error message, or null if the login attempt was successful.</value>
        public string? Error { get; set; }

        /// <summary>
        /// Gets or sets the token generated after a successful login attempt.
        /// </summary>
        /// <value>A string representing the token, or null if the login attempt was unsuccessful.</value>
        public string? Token { get; set; }
    }

}
