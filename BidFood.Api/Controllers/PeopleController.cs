using BidFood.Domain.Interfaces;
using BidFood.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace BidFood.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PeopleController : ControllerBase
    {
        private readonly IDataProvider<Person> _provider;

        public PeopleController(IDataProvider<Person> provider)
        {
            _provider = provider;
        }

        [HttpGet]
        public IEnumerable<Person> Get()
        {
            return _provider.GetAll();
        }

        [HttpGet("{id}", Name = "GetPerson")]
        public ActionResult Get(int id)
        {
            return Ok(_provider.GetById(id));
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Person person)
        {
            var personInDb = _provider.GetByName(person.FirstName, person.LastName);

            if (personInDb == null)
            {
                var id = await _provider.CreateAsync(person);
                return CreatedAtRoute("GetPerson", new { id }, person);
            }

            return BadRequest("Person already exists.");
        }
    }
}