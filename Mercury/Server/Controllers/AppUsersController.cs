using Mercury.Server.Data;
using Mercury.Server.Data.Context;
using Mercury.Shared.API;
using Mercury.Shared.Models;
using Mercury.Shared.Models.AspNetUser;
using Microsoft.AspNetCore.Mvc;

namespace Mercury.Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AppUsersController : ControllerBase
    {
        RepositoryEF<UserSearch, MercuryDevContext> _userSearchManager;
        RepositoryEF<AspNetUser, MercuryDevContext> _aspNetUserManager;

        public AppUsersController(RepositoryEF<UserSearch, MercuryDevContext> customersManager, RepositoryEF<AspNetUser, MercuryDevContext> aspnetManager)
        {
            _userSearchManager = customersManager;
            _aspNetUserManager = aspnetManager;
        }

        [HttpGet]
        public async Task<ActionResult<APIListOfEntityResponse<UserSearch>>> GetAllCustomers()
        {
            try
            {
                var result = await _userSearchManager.GetAll();
                return Ok(new APIListOfEntityResponse<UserSearch>()
                {
                    Success = true,
                    Data = result
                });
            }
            catch (Exception ex)
            {
                // log exception here
                return StatusCode(500);
            }
        }

        [HttpGet("GetAllAspNetUsers")]
        public async Task<ActionResult<APIListOfEntityResponse<AspNetUser>>> GetAllAspNetUsers()
        {
            try
            {
                var result = await _aspNetUserManager.GetAll();
                return Ok(new APIListOfEntityResponse<AspNetUser>()
                {
                    Success = true,
                    Data = result
                });
            }
            catch (Exception ex)
            {
                // log exception here
                return StatusCode(500);
            }
        }

        [HttpGet("{email}")]
        public async Task<ActionResult<APIEntityResponse<UserSearch>>> GetByEmail(string email)
        {
            try
            {
                var result = (await _userSearchManager.Get(x => x.Email == email)).FirstOrDefault();
                if (result != null)
                {
                    return Ok(new APIEntityResponse<UserSearch>()
                    {
                        Success = true,
                        Data = result
                    });
                }
                else
                {
                    return Ok(new APIEntityResponse<UserSearch>()
                    {
                        Success = false,
                        ErrorMessages = new List<string>() { "Email Not Found" },
                        Data = null
                    });
                }
            }
            catch (Exception ex)
            {
                // log exception here
                return StatusCode(500);
            }
        }

        [HttpGet("{email}/searchbyemail")]
        public async Task<ActionResult<APIListOfEntityResponse<UserSearch>>> SearchByEmail(string email)
        {
            try
            {
                var result = await _userSearchManager.Get(x => x.Email.ToLower().Contains(email.ToLower()));
                if (result != null && result.Count() > 0)
                {
                    return Ok(new APIListOfEntityResponse<UserSearch>()
                    {
                        Success = true,
                        Data = result
                    });
                }
                else
                {
                    return Ok(new APIEntityResponse<UserSearch>()
                    {
                        Success = false,
                        ErrorMessages = new List<string>() { "Email Not Found" },
                        Data = null
                    });
                }
            }
            catch (Exception ex)
            {
                // log exception here
                return StatusCode(500);
            }
        }

        [HttpPost]
        public async Task<ActionResult<APIEntityResponse<UserSearch>>> Post([FromBody] UserSearch user)
        {
            try
            {
                await _userSearchManager.Insert(user);
                var result = (await _userSearchManager.Get(x => x.AspNetUserID == user.AspNetUserID)).FirstOrDefault();
                if (result != null)
                {
                    return Ok(new APIEntityResponse<UserSearch>()
                    {
                        Success = true,
                        Data = result
                    });
                }
                else
                {
                    return Ok(new APIEntityResponse<UserSearch>()
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
                return StatusCode(500);
            }
        }

        [HttpPut]
        public async Task<ActionResult<APIEntityResponse<UserSearch>>> Put([FromBody] UserSearch user)
        {
            try
            {
                await _userSearchManager.Update(user);
                var result = (await _userSearchManager.Get(x => x.AspNetUserID == user.AspNetUserID)).FirstOrDefault();
                if (result != null)
                {
                    return Ok(new APIEntityResponse<UserSearch>()
                    {
                        Success = true,
                        Data = result
                    });
                }
                else
                {
                    return Ok(new APIEntityResponse<UserSearch>()
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
                return StatusCode(500);
            }
        }

        [HttpDelete("{AspNetUserID}")]
        public async Task<ActionResult> Delete(string AspNetUserID)
        {
            try
            {
                var CustomerList = await _userSearchManager.Get(x => x.AspNetUserID == AspNetUserID);
                if (CustomerList != null)
                {
                    var Customer = CustomerList.First();
                    var success = await _userSearchManager.Delete(Customer);
                    if (success)
                        return NoContent();
                    else
                        return StatusCode(500);
                }
                else
                    return StatusCode(500);
            }
            catch (Exception ex)
            {
                // log exception here
                var msg = ex.Message;
                return StatusCode(500);
            }
        }
    }
}
