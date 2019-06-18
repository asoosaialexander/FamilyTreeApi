using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FamilyTreeApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FamilyTreeApi.Controllers
{
    [Route("api/persons")]
    [ApiController]
    public class PersonsController : ControllerBase
    {
        private readonly FamilyTreeContext _context;
        public PersonsController(FamilyTreeContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Person>>> GetAll()
        {
            return await _context.Persons.ToListAsync();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Person>> GetById(long id)
        {
            var person = await _context.Persons.FindAsync(id);
            if (person == null)
            {
                return NotFound();
            }
            return person;
        }
        [HttpPost]
        public async Task<ActionResult<Person>> Post(Person person)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            _context.Persons.Add(person);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = person.Id }, person);
        }
    }
}