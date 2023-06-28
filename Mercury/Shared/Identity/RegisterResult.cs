using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mercury.Shared.Identity
{
    /// <summary>
    /// Represents the result of a registration attempt.
    /// </summary>
    public class RegisterResult
    {
        /// <summary>
        /// Ctor.
        /// </summary>
        public RegisterResult()
        {
            Errors = new List<string>();
        }

        /// <summary>
        /// Gets or sets a value indicating whether the registration attempt was successful.
        /// </summary>
        /// <value>True if the registration attempt was successful, false otherwise.</value>
        public bool Successful { get; set; }

        /// <summary>
        /// Gets or sets a collection of error messages in case the registration attempt was unsuccessful.
        /// </summary>
        /// <value>An enumerable collection of string representing error messages, or an empty collection if the registration attempt was successful.</value>
        public IEnumerable<string> Errors { get; set; }
    }
}
