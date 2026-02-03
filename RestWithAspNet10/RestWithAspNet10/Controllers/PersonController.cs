using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestWithAspNet10.Model;
using RestWithAspNet10.Service;
using RestWithAspNet10.Service.Impl;

namespace RestWithAspNet10.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonController : ControllerBase
    {
        private readonly IPersonService _personService;
        public PersonController(IPersonService personService)
        {
            _personService = personService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_personService.FindAll());
        }
        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            var person = _personService.FindById(id);
            if(person == null)
            {
                return NotFound();
            }
            return Ok(person);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Person person)
        {
            var createPerson = _personService.Create(person);
            if(createPerson == null)
            {
                return NotFound();
            }
            return Ok(createPerson);
        }


        [HttpPut]
        public IActionResult Put([FromBody] Person person)
        {
            var updatePerson = _personService.Update(person);
            if (updatePerson == null)
            {
                return NotFound();
            }
            return Ok(updatePerson);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            _personService.Delete(id);
            return NoContent();
        }

    }
}
