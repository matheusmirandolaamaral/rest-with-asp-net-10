//using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using RestWithAspNet10.Data.DTO.V1;
using RestWithAspNet10.Hypermedia.Utils;
using RestWithAspNet10.Service;


namespace RestWithAspNet10.Controllers.V1
{
    [ApiController]
    [Route("api/[controller]/v1")]
    //[EnableCors("LocalPolicy")]
    public class PersonController : ControllerBase
    {
        private readonly IPersonService _personService;
        private readonly ILogger<PersonController> _logger;

        public PersonController(IPersonService personService, ILogger<PersonController> logger)
        {
            _personService = personService;
            _logger = logger;
        }

        [HttpGet("{sortDirection}/{pageSize}/{page}")]
        [ProducesResponseType(200, Type = typeof(PagedSearchDTO<PersonDTO>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult Get([FromQuery] string? name, string sortDirection, int pageSize, int page)
        {
            _logger.LogInformation( "Fetching persons with paged search: {name}, {sortDirection}, {pageSize}, {page}", name, sortDirection, pageSize, page);
            return Ok(_personService.FindWithPagedSearch(name, sortDirection, pageSize, page));
        }



        [HttpGet("find-by-name")]
        [ProducesResponseType(200, Type = typeof(List<PersonDTO>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult GetByName([FromQuery] string? firstName, [FromQuery] string? lastName)
        {
            _logger.LogInformation("Fetching persons with name {firstName} {lastName}", firstName, lastName);
            return Ok(_personService.FindByName(firstName, lastName));
        }



        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(PersonDTO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        //[EnableCors("LocalPolicy")]
        public IActionResult Get(long id)
        {
            _logger.LogInformation("Fetching persons with ID {id}", id);
            var person = _personService.FindById(id);
            if (person == null)
            {
                _logger.LogWarning("Person with ID {id} not found", id);
                return NotFound();
            }
            return Ok(person);
        }



        [HttpPost]
        [ProducesResponseType(200, Type = typeof(PersonDTO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        //[EnableCors("MultipleOriginPolicy")]
        public IActionResult Post([FromBody] PersonDTO person)
        {
            _logger.LogInformation("Creating new Person: {firstName}", person.FirstName);
            var createPerson = _personService.Create(person);
            if (createPerson == null)
            {
                _logger.LogError("Failed to create person with name {firstName}", person.FirstName);
                return NotFound();
            }
            return Ok(createPerson);
        }




        [HttpPut]
        [ProducesResponseType(200, Type = typeof(PersonDTO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult Put([FromBody] PersonDTO person)
        {
            _logger.LogInformation("Updating persons with ID {id}", person.Id);
            var updatePerson = _personService.Update(person);
            if (updatePerson == null)
            {
                _logger.LogError("Failed to update person with ID {id}", person.Id);
                return NotFound();
            }
            _logger.LogDebug("Person update sucessfully: {firstName}", updatePerson.FirstName);
            return Ok(updatePerson);
        }



        [HttpDelete("{id}")]
        [ProducesResponseType(204, Type = typeof(PersonDTO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult Delete(long id)
        {
            _logger.LogInformation("Deleting persons with ID {id}", id);
            _personService.Delete(id);
            _logger.LogDebug("Person with ID {id} deleted sucessfully", id);
            return NoContent();
        }




        [HttpPatch("{id}")]
        [ProducesResponseType(200, Type = typeof(PersonDTO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult Disable(long id)
        {
            _logger.LogInformation("Disabling persons with ID {id}", id);
            var disablePerson = _personService.Disable(id);
            if (disablePerson == null)
            {
                _logger.LogError("Failed to disable person with ID {id}", id);
                return NotFound();
            }
            _logger.LogDebug("Person with ID {id} disabled sucessfully", id);
            return Ok(disablePerson);

        }
    }
}
