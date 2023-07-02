using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mercury.Shared.Models
{
    #region UserSearch class

    /// <summary>
    /// This class represents a model for searching users based on different criteria.
    /// </summary>
    [Table("UserSearch", Schema ="dbo")]
    public class UserSearch
    {
        #region Properties

        /// <summary>
        /// Gets or sets the user's ID.
        /// </summary>
        [Key]
        [Required]
        public string AspNetUserID { get; set; }

        /// <summary>
        /// Gets or sets the user's username.
        /// </summary>
        public string? UserName { get; set; }

        /// <summary>
        /// Gets or sets the user's email.
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// Gets or sets the user's phone number.
        /// </summary>
        public string? PhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether lockout is enabled for the user.
        /// </summary>
        public bool LockoutEnabled { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the user's lockout ends, if any.
        /// </summary>
        public DateTime? LockoutEnd { get; set; }

        #endregion
    }

    #endregion

}
