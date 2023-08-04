using Mercury.Server.Data;
using Mercury.Shared.API;
using Mercury.Shared.Models.AspNetUser;
using Mercury.Shared.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;
using Serialize.Linq.Serializers;
using System.Linq.Expressions;

namespace Mercury.Server.Controllers
{
    public class MercuryBaseController<TEntity, TDataContext> : ControllerBase
        where TEntity : class
        where TDataContext : DbContext
    {
        private RepositoryEF<TEntity, TDataContext> _repository;

        public MercuryBaseController(RepositoryEF<TEntity, TDataContext> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<APIListOfEntityResponse<TEntity>>> GetAll()
        {
            try
            {
                var result = await _repository.GetAll();

                if (result is not null)
                {
                    return Ok(new APIListOfEntityResponse<TEntity>()
                    {
                        Success = true,
                        Data = result
                    });
                }
                else
                {
                    return Ok(new APIListOfEntityResponse<TEntity>()
                    {
                        Success = false,
                    });
                }

            }
            catch (Exception ex)
            {
                // log exception here
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<APIEntityResponse<TEntity>>> GetById([FromRoute] string Id)
        {
            try
            {
                var entity = await _repository.GetByID(Id);
                if (entity is not null) return Ok(new APIEntityResponse<TEntity>()
                {
                    Success = true,
                    Data = entity
                });
                else return Ok(new APIEntityResponse<TEntity>()
                {
                    Success = false,
                });
            }
            catch (Exception e)
            {
                // log exception here
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost("getwithfilter")]
        public async Task<ActionResult<APIListOfEntityResponse<TEntity>>> GetWithFilter([FromBody] QueryFilter<TEntity> Filter)
        {
            try
            {
                var result = await _repository.Get(Filter);
                return Ok(new APIListOfEntityResponse<TEntity>()
                {
                    Success = true,
                    Data = result.ToList()
                });
            }
            catch (Exception ex)
            {
                // log exception here
                var msg = ex.Message;
                return StatusCode(500);
            }
        }

        [HttpPost("getwithLinqfilter")]
        public async Task<ActionResult<APIListOfEntityResponse<TEntity>>> GetWithLinqFilter([FromBody] LinqQueryFilter<TEntity> linqQueryFilter)
        {
            try
            {
                var result = await _repository.Get(linqQueryFilter);
                
                return Ok(new APIListOfEntityResponse<TEntity>()
                {
                    Success = true,
                    Data = result.ToList()
                });
            }
            catch (Exception ex)
            {
                // log exception here
                var msg = ex.Message;
                return StatusCode(500);
            }
        }

        [HttpPost]
        public async Task<ActionResult<APIEntityResponse<TEntity>>> Post([FromBody] TEntity Entity)
        {
            try
            {
                var insertedEntity = await _repository.Insert(Entity);
                return Ok(new APIEntityResponse<TEntity>()
                {
                    Success = true,
                    Data = insertedEntity
                });
            }
            catch (Exception ex)
            {
                // log exception here
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPut]
        public async Task<ActionResult<APIEntityResponse<TEntity>>> Put([FromBody] TEntity Entity)
        {
            try
            {
                var updateEntity = await _repository.Update(Entity);
                return Ok(new APIEntityResponse<TEntity>()
                {
                    Success = true,
                    Data = updateEntity
                });
            }
            catch (Exception ex)
            {
                // log exception here
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete("{Id}")]
        public async Task<ActionResult> Delete([FromRoute] string Id)
        {
            try
            {
                var success = await _repository.Delete(Id);
                if (success)
                {
                    return Ok();
                }
                else
                {
                    return NotFound();
                }

            }
            catch (Exception ex)
            {
                // log exception here
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

    }
}
