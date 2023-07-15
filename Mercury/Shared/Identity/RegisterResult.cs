using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mercury.Shared.Base;

namespace Mercury.Shared.Identity
{
    /// <summary>
    /// Represents the result of a registration attempt.
    /// </summary>
    public class RegisterResult : ResponseBase
    {
        public RegisterResult(string errorMessage=""):base(errorMessage)
        {
            
        }
    }
}
