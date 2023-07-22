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
    public class AppUsersController : ControllerBase
    {
        RepositoryEF<AspNetUser, MercuryDevContext> _aspNetUserManager;

        public AppUsersController(RepositoryEF<AspNetUser, MercuryDevContext> aspnetManager)
        {
            _aspNetUserManager = aspnetManager;
        }

        [HttpGet]
        public async Task<ActionResult<APIListOfEntityResponse<AspNetUser>>> GetAllAspNetUsers()
        {
            try
            {
                var result = await _aspNetUserManager.GetAll();
                if (result is not null) return Ok(new APIListOfEntityResponse<AspNetUser>()
                {
                    Success = true,
                    Data = result
                });
                else return Ok(new APIListOfEntityResponse<AspNetUser>()
                {
                    Success = false,
                    ErrorMessages = new List<string>() { "No Users" }
                });
            }
            catch (Exception ex)
            {
                // todo: log exception here
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("{aspNetUserID}")]
        public async Task<ActionResult<APIEntityResponse<AspNetUser>>> GetAspNetUser([FromRoute] string aspNetUserID)
        {
            try
            {
                var user = await _aspNetUserManager.GetByID(aspNetUserID);
                if (user is not null) return Ok(new APIEntityResponse<AspNetUser>()
                {
                    Success = true,
                    Data = user
                });
                else return Ok(new APIEntityResponse<AspNetUser>()
                {
                    Success = false,
                    ErrorMessages = new List<string>() { "User Not found" }
                });
            }
            catch (Exception e)
            {
                // log exception here
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        public async Task<ActionResult<APIEntityResponse<AspNetUser>>> Post([FromBody] AspNetUser user)
        {
            try
            {
                await _aspNetUserManager.Insert(user);
                var result = (await _aspNetUserManager.Get(x => x.Id == user.Id)).FirstOrDefault();
                if (result != null)
                {
                    return Ok(new APIEntityResponse<AspNetUser>()
                    {
                        Success = true,
                        Data = result
                    });
                }
                else
                {
                    return Ok(new APIEntityResponse<AspNetUser>()
                    {
                        Success = false,
                        ErrorMessages = new List<string>() { "Could not find user after adding it." },
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
        public async Task<ActionResult<APIEntityResponse<AspNetUser>>> Put([FromBody] AspNetUser user)
        {
            try
            {
                await _aspNetUserManager.Update(user);
                var result = (await _aspNetUserManager.Get(x => x.Id == user.Id)).FirstOrDefault();

                if (result != null)
                {
                    return Ok(new APIEntityResponse<AspNetUser>()
                    {
                        Success = true,
                        Data = result
                    });
                }
                else
                {
                    return Ok(new APIEntityResponse<AspNetUser>()
                    {
                        Success = false,
                        ErrorMessages = new List<string>() { "Could not find user after updating it." },
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

        [HttpDelete("{AspNetUserID}")]
        public async Task<ActionResult> Delete(string AspNetUserID)
        {
            try
            {
                var user = (await _aspNetUserManager.Get(x => x.Id == AspNetUserID)).FirstOrDefault();
                if (user != null)
                {
                    var success = await _aspNetUserManager.Delete(user);
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
}
