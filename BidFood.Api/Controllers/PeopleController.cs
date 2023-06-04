using BidFood.Domain.Interfaces;
using BidFood.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace BidFood.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
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
            var id = await _provider.CreateAsync(person);
            return CreatedAtRoute("GetPerson", new { id }, person);
        }
    }
}