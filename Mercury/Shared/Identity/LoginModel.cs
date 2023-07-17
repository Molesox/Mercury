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
        [EmailAddress]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets a required Password.
        /// </summary>
        /// <value>The password of the user.</value>
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the user should be remembered.
        /// </summary>
        /// <value>True if the user wants to be remembered, false otherwise.</value>
        public bool RememberMe { get; set; }
    }

}
