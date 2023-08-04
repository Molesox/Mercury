using Mercury.Server.Data;
using Mercury.Server.Data.Context;
using Mercury.Shared.API;
using Mercury.Shared.Models;
using Mercury.Shared.Models.AspNetUser;
using Microsoft.AspNetCore.Mvc;

namespace Mercury.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RolesController : MercuryBaseController<AspNetRole, MercuryContext>
    {

        public RolesController(RepositoryEF<AspNetRole, MercuryContext> roleManager)
            : base(roleManager) { }
    }
}
