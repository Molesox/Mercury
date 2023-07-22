using Mercury.Shared.Models;
using Mercury.Shared.Models.AspNetUser;


namespace Mercury.Client.Services
{
    public class AspNetUserManager : APIRepository<AspNetUser>
    {
        public AspNetUserManager(HttpClient _http)
            : base(_http, "AppUsers", nameof(AspNetUser.Id))
        { }
    }
}
