using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mercury.Shared.Services
{
    public interface ICustomAuthenticationStateProvider
    {
        void MarkUserAsAuthenticated(string email);
        void MarkUserAsLoggedOut();
    }

}
