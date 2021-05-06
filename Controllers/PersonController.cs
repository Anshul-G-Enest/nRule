using Microsoft.AspNetCore.Mvc;
using Rule.WebAPI.Model.DTO;
using Rule.WebAPI.Services.Interface;
using System.Linq;
using System.Threading.Tasks;

namespace Rule.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private IPersonData _personData;
        public PersonController(IPersonData personData)
        {
            _personData = personData;
        }

        [HttpPost]
        public async Task<IActionResult> Post(PersonRequestModel personRequestModel)
        {
            var isSaved = await _personData.SavePerson(personRequestModel);
            if (isSaved)
                return Ok(isSaved);

            return BadRequest("Person is not mathed with Rule.");
        }

        [HttpPut]
        public async Task<IActionResult> Put(PersonRequestModel personRequestModel)
        {
            var isSaved = await _personData.UpdatePerson(personRequestModel);
            if (isSaved)
                return Ok(isSaved);

            return BadRequest("bad request");
        }

        [HttpGet]
        public async Task<IActionResult> Get(int personId)
        {
            var person = await _personData.GetPerson(personId);
            if (person != null)
                return Ok(person);

            return NotFound("Person Not found with Id.");
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            var persons = await _personData.GetPersons();

            if(persons.Any())
               return Ok(persons);
            return NotFound();
        }
    }
}
