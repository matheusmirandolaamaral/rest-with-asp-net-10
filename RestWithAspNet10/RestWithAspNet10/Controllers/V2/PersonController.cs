using Microsoft.AspNetCore.Mvc;
using RestWithAspNet10.Data.DTO.V2;
using RestWithAspNet10.Service.Impl;


namespace RestWithAspNet10.Controllers.V2
{
    [ApiController]
    [Route("api/[controller]/v2")]
    public class PersonController : ControllerBase
    {
        private readonly PersonServiceImplV2 _personService;
        private readonly ILogger<PersonController> _logger;

        public PersonController(PersonServiceImplV2 personService, ILogger<PersonController> logger)
        {
            _personService = personService;
            _logger = logger;
        }

        

        [HttpPost]
        public IActionResult Post([FromBody] PersonDTO person)
        {
            _logger.LogInformation("Creating new Person: {firstName}",person.FirstName );
            var createPerson = _personService.Create(person);
            if(createPerson == null)
            {
                _logger.LogError("Failed to create person with name {firstName}", person.FirstName);
                return NotFound();
            }
            return Ok(createPerson);
        }


       

    }
}
