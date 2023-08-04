using Mercury.Server.Controllers;
using Mercury.Server.Data;
using Mercury.Server.Data.Context;
using Mercury.Shared.API;
using Mercury.Shared.Models.Mercury;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGeneration.CommandLine;

[Route("[controller]")]
[ApiController]
public class PersonController : MercuryBaseController<Person, MercuryContext>
{


    public PersonController(RepositoryEF<Person, MercuryContext> personManager)
        : base(personManager)
    {
    }


}
