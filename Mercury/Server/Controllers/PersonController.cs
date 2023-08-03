using Mercury.Server.Data;
using Mercury.Server.Data.Context;
using Mercury.Shared.API;
using Mercury.Shared.Models.Mercury;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
class PersonController : ControllerBase
{
    RepositoryEF<Person, MercuryContext> _personManager;

    public PersonController(RepositoryEF<Person, MercuryContext> aspnetManager)
    {
        _personManager = aspnetManager;
    }

    [HttpGet]
    public async Task<ActionResult<APIListOfEntityResponse<Person>>> GetAllPersons()
    {
        try
        {
            var result = await _personManager.GetAll();
            if (result is not null) return Ok(new APIListOfEntityResponse<Person>()
            {
                Success = true,
                Data = result
            });
            else return Ok(new APIListOfEntityResponse<Person>()
            {
                Success = false,
                ErrorMessages = new List<string>() { "No persons" }
            });
        }
        catch (Exception ex)
        {
            // todo: log exception here
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpGet("{PersonID}")]
    public async Task<ActionResult<APIEntityResponse<Person>>> GetPerson([FromRoute] string PersonID)
    {
        try
        {
            var person = default(Person);

            if (int.TryParse(PersonID, out int id))
                person = await _personManager.GetByID(PersonID);
            else
                person = (await _personManager.Get(x => x.AppUserID == PersonID)).FirstOrDefault();

            if (person is not null) return Ok(new APIEntityResponse<Person>()
            {
                Success = true,
                Data = person
            });
            else return Ok(new APIEntityResponse<Person>()
            {
                Success = false,
                ErrorMessages = new List<string>() { "person Not found" }
            });
        }
        catch (Exception e)
        {
            // log exception here
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpGet("GetByEmail/{email}")]
    public async Task<ActionResult<APIEntityResponse<Person>>> GetPersonByEmail([FromRoute] string email)
    {
        try
        {
            var person = (await _personManager.Get(x => x.Emails.First().EmailAddress == email)).FirstOrDefault();
            if (person is not null) return Ok(new APIEntityResponse<Person>()
            {
                Success = true,
                Data = person
            });
            else return Ok(new APIEntityResponse<Person>()
            {
                Success = false,
                ErrorMessages = new List<string>() { "person Not found" }
            });
        }
        catch (Exception e)
        {
            // log exception here
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpPost]
    public async Task<ActionResult<APIEntityResponse<Person>>> Post([FromBody] Person person)
    {
        try
        {
            await _personManager.Insert(person);
            var result = (await _personManager.Get(x => x.PersonID == person.PersonID)).FirstOrDefault();
            if (result != null)
            {
                return Ok(new APIEntityResponse<Person>()
                {
                    Success = true,
                    Data = result
                });
            }
            else
            {
                return Ok(new APIEntityResponse<Person>()
                {
                    Success = false,
                    ErrorMessages = new List<string>() { "Could not find person after adding it." },
                    Data = null
                });
            }
        }
        catch (Exception ex)
        {
            // log exception here
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpPut]
    public async Task<ActionResult<APIEntityResponse<Person>>> Put([FromBody] Person person)
    {
        try
        {
            await _personManager.Update(person);
            var result = (await _personManager.Get(x => x.PersonID == person.PersonID)).FirstOrDefault();

            if (result != null)
            {
                return Ok(new APIEntityResponse<Person>()
                {
                    Success = true,
                    Data = result
                });
            }
            else
            {
                return Ok(new APIEntityResponse<Person>()
                {
                    Success = false,
                    ErrorMessages = new List<string>() { "Could not find person after updating it." },
                    Data = null
                });
            }
        }
        catch (Exception ex)
        {
            // log exception here
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpDelete("{PersonID}")]
    public async Task<ActionResult> Delete(int PersonID)
    {
        try
        {
            var person = (await _personManager.Get(x => x.PersonID == PersonID)).FirstOrDefault();
            if (person != null)
            {
                var success = await _personManager.Delete(person);
                if (success) return Ok();
            }

            return StatusCode(StatusCodes.Status500InternalServerError);
        }
        catch (Exception ex)
        {
            // log exception here
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}
