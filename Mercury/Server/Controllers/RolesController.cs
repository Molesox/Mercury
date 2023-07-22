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
    public class RolesController : ControllerBase
    {
        RepositoryEF<AspNetRole, MercuryDevContext> _aspNetRolesManager;

        public RolesController(RepositoryEF<AspNetRole, MercuryDevContext> aspnetManager)
        {
            _aspNetRolesManager = aspnetManager;
        }

        [HttpGet]
        public async Task<ActionResult<APIListOfEntityResponse<AspNetRole>>> GetAllAspNetRoles()
        {
            try
            {
                var result = await _aspNetRolesManager.GetAll();
                if (result is not null) return Ok(new APIListOfEntityResponse<AspNetRole>()
                {
                    Success = true,
                    Data = result
                });
                else return Ok(new APIListOfEntityResponse<AspNetRole>()
                {
                    Success = false,
                    ErrorMessages = new List<string>() { "No Roles" }
                });
            }
            catch (Exception ex)
            {
                // todo: log exception here
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("{AspNetRoleID}")]
        public async Task<ActionResult<APIEntityResponse<AspNetRole>>> GetAspNetRole([FromRoute] string AspNetRoleID)
        {
            try
            {
                var user = await _aspNetRolesManager.GetByID(AspNetRoleID);
                if (user is not null) return Ok(new APIEntityResponse<AspNetRole>()
                {
                    Success = true,
                    Data = user
                });
                else return Ok(new APIEntityResponse<AspNetRole>()
                {
                    Success = false,
                    ErrorMessages = new List<string>() { "Role Not found" }
                });
            }
            catch (Exception e)
            {
                // log exception here
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        public async Task<ActionResult<APIEntityResponse<AspNetRole>>> Post([FromBody] AspNetRole role)
        {
            try
            {
                await _aspNetRolesManager.Insert(role);
                var result = (await _aspNetRolesManager.Get(x => x.Id == role.Id)).FirstOrDefault();
                if (result != null)
                {
                    return Ok(new APIEntityResponse<AspNetRole>()
                    {
                        Success = true,
                        Data = result
                    });
                }
                else
                {
                    return Ok(new APIEntityResponse<AspNetRole>()
                    {
                        Success = false,
                        ErrorMessages = new List<string>() { "Could not find role after adding it." },
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
        public async Task<ActionResult<APIEntityResponse<AspNetRole>>> Put([FromBody] AspNetRole role)
        {
            try
            {
                await _aspNetRolesManager.Update(role);
                var result = (await _aspNetRolesManager.Get(x => x.Id == role.Id)).FirstOrDefault();

                if (result != null)
                {
                    return Ok(new APIEntityResponse<AspNetRole>()
                    {
                        Success = true,
                        Data = result
                    });
                }
                else
                {
                    return Ok(new APIEntityResponse<AspNetRole>()
                    {
                        Success = false,
                        ErrorMessages = new List<string>() { "Could not find role after updating it." },
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

        [HttpDelete("{AspNetRoleID}")]
        public async Task<ActionResult> Delete(string AspNetRoleID)
        {
            try
            {
                var user = (await _aspNetRolesManager.Get(x => x.Id == AspNetRoleID)).FirstOrDefault();
                if (user != null)
                {
                    var success = await _aspNetRolesManager.Delete(user);
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
