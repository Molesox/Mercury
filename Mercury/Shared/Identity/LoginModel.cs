using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mercury.Shared.Identity
{
    /// <summary>
    /// Represents a model for the login functionality.
    /// </summary>
    public class LoginModel
    {
        /// <summary>
        /// Gets or sets a required Email.
        /// </summary>
        /// <value>The Email address of the user.</value>
        [Required]
        public required string Email { get; set; }

        /// <summary>
        /// Gets or sets a required Password.
        /// </summary>
        /// <value>The password of the user.</value>
        [Required]
        public required string Password { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the user should be remembered.
        /// </summary>
        /// <value>True if the user wants to be remembered, false otherwise.</value>
        public bool RememberMe { get; set; }
    }

}
