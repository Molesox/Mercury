using System;
using System.Linq;

namespace Mercury.Shared.Base
{
    /// <summary>
    /// Represents the base for a Response.
    /// </summary>
    public class ResponseBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ResponseBase"/> class.
        /// </summary>
        public ResponseBase(string errorMessage = "")
        {
            Errors = new List<string>() { errorMessage };

        }

        /// <summary>
        /// Gets or sets a collection of error messages.
        /// </summary>
        public IEnumerable<string> Errors { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the contract is successful.
        /// </summary>
        public bool IsSuccesful { get; set; }


    }
}

