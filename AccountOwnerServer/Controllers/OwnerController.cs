using AutoMapper;
using Contracts;
using Entities.DataTransferObject;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Dynamic;
using System.Text.Json.Serialization;

namespace AccountOwnerServer.Controllers
{
    [Route("api/owner")]
    [ApiController]
    public class OwnerController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private readonly IRepositoryWrapper _repository;
        private readonly IMapper _mapper;

        public OwnerController(ILoggerManager logger, IRepositoryWrapper repository,IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetOwners([FromQuery] OwnerParameters ownerParameters)
        {

            try
            {
                if (!ownerParameters.VaildYearRange)
                {
                    return BadRequest("Max Year of Birth cannot be less than MinYear of birth");
                }
                var owners=_repository.Owner.GetOwners(ownerParameters);

                var metadata = new
                {
                    owners.TotalCount,
                    owners.PageSize,
                    owners.CurrentPage,
                    owners.HasNext,
                    owners.HasPrevious,
                };

                Response.Headers.Add("X-Pagination",JsonConvert.SerializeObject(metadata));

                _logger.LogInfo($"Returned all Owners form Database");

                //var ownerResult= _mapper.Map<IEnumerable<OwnerDto>>(owners);

                return Ok(owners);
                //return Ok(ownerResult);

            }
            catch(Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllOwners action: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpGet("{id}",Name ="OwnerById")]

        public IActionResult GetOwnerById(Guid id, [FromQuery] string fields)
        {
           
            try
            {
                var owner = _repository.Owner.GetOwnerById(id,fields);
                if (owner == default(ExpandoObject))
                {
                    _logger.LogError($"Owner with id: {id}, hasn't been found in db.");
                    return NotFound();
                }

                return Ok(owner);

                //_logger.LogInfo($"Returned owner with id: {id}");

                //var ownerResult = _mapper.Map<OwnerDto>(owner);
                //return Ok(ownerResult);
            }
            catch (Exception ex)
            {

                _logger.LogError($"Something went wrong inside the GetOwnerById action: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

            [HttpGet("{id}/account")]

            public IActionResult GetOwnerWithDetails(Guid id)
            {
                try
                {
                    var owner = _repository.Owner.GetOwnerWithDetails(id);
                    if (owner == null)
                    {
                        _logger.LogError($"Owner with id: {id}, hasn't been found in db.");
                        return NotFound();
                    }
                    _logger.LogInfo($"Returned owner with details for  id: {id}");

                    var ownerResult = _mapper.Map<OwnerDto>(owner);
                    return Ok(ownerResult);
                }
                catch (Exception ex)
                {

                    _logger.LogError($"Something went wrong inside the GetOwnerById action: {ex.Message}");
                    return StatusCode(500, "Internal Server Error");
                }

            }

        //Modify Owner Colltroller here for Post Method
        [HttpPost]
        public IActionResult CreateOwner([FromBody] OwnerForCreationDto owner)
        {
            try
            {
                if (owner is null)
                {
                    _logger.LogError("Owner object sent from the client is null");
                    return BadRequest("Owner object is null");

                }
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invaild owner object sent from the client");
                    return BadRequest("Invaild model object");
                }
                var ownerEntity = _mapper.Map<Owner>(owner);
                _repository.Owner.CreateOwner(ownerEntity);
                _repository.Save();

                var createdOwner = _mapper.Map<OwnerDto>(ownerEntity);

                return CreatedAtRoute("OwnerById", new { id = createdOwner.id }, createdOwner);

            }
            
            catch (Exception ex)
            {

                _logger.LogError($"Something went wrong indide the CreateOwner action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }


        [HttpPut("{id}")]
        public IActionResult UpdateOwner(Guid id,[FromBody]OwnerForUpdateDto owner)
        {
            try
            {
                if (owner is null)
                {
                    _logger.LogError("Owner object sent from client is null.");
                    return BadRequest("Owner object is null");

                }
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invaild owner object sent form client. ");
                    return BadRequest("Invaild model object ");


                }
                var ownerEntity = _repository.Owner.GetOwnerById(id);
                if(ownerEntity is null)
                {
                    _logger.LogError($"Owner with id: {id}, has't been found in db");
                    return NotFound();
                }
                _mapper.Map(owner, ownerEntity);
                _repository.Owner.UpdateOwner(ownerEntity);
                _repository.Save();

                return NoContent();

            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside UpdateOwner action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteOwner(Guid id)
        {
            try
            {
                var owner = _repository.Owner.GetOwnerById(id);
                if(owner == null)
                {
                    _logger.LogError($"Owner with id: {id} hasn't been not found in database");
                    return NotFound();

                }
                _repository.Owner.DeleteOwner(owner);
                _repository.Save();

                return NoContent();

            }
            catch (Exception ex)
                {
                _logger.LogError($"Something went wrong inside DeleteOwner action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        


    }
}
