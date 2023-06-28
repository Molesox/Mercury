using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Mercury.Shared.Identity
{
    /// <summary>
    /// Represents a model for registering a user, containing email, password and password confirmation.
    /// </summary>
    public class RegisterModel
    {
        /// <summary>
        /// Gets or sets the Email address. This field is required and should follow the email address format.
        /// </summary>
        /// <value>The Email address of the user.</value>
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public required string Email { get; set; }

        /// <summary>
        /// Gets or sets the Password. This field is required and should be between 6 and 100 characters long.
        /// </summary>
        /// <value>The password of the user.</value>
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public required string Password { get; set; }

        /// <summary>
        /// Gets or sets the Confirm Password. This field should match the Password field.
        /// </summary>
        /// <value>The confirmation of the user's password.</value>
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public required string ConfirmPassword { get; set; }
    }
}
