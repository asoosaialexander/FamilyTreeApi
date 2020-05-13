using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FamilyTreeApi.Models;

namespace FamilyTreeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        private readonly FamilyTreeContext _context;

        public PeopleController(FamilyTreeContext context)
        {
            _context = context;
        }

        // GET: api/People
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Person>>> GetPerson()
        {
            return await _context.Person.ToListAsync();
        }

        [HttpGet("fathers")]
        public async Task<ActionResult<IEnumerable<Person>>> GetFathers()
        {
            return await _context.Person
                .Where(p => p.Gender.ToLower() == "male").ToListAsync();
        }

        [HttpGet("mothers")]
        public async Task<ActionResult<IEnumerable<Person>>> GetMothers()
        {
            return await _context.Person
                .Where(p => p.Gender.ToLower() == "female").ToListAsync();
        }

        // [HttpGet("spouses/{id}")]
        // public async Task<ActionResult<IEnumerable<Person>>> GetSpouses()
        // {
        //     var person = await _context.Person.FindAsync(id);

        //     if (person == null)
        //     {
        //         return NotFound();
        //     }

        //     if (person.Gender.ToLower() == "male")
        //     {
        //         return await _context.Person
        //             .Where(p => p.Gender.ToLower() == "female" && p.Id != person.MotherId).ToListAsync();
        //     }
        //     else
        //     {
        //         return await _context.Person
        //             .Where(p => p.Gender.ToLower() == "male" && p.Id != person.FatherId).ToListAsync();
        //     }
        // }

        [HttpGet("spouses/{id}")]
        public async Task<ActionResult<IEnumerable<Person>>> GetSpouses(int id)
        {
            var person = await _context.Person.FindAsync(id);
            List<Person> spouses = new List<Person>();

            if (person == null)
            {
                return NotFound();
            }

             var spouseData = _context.Spouse
                .Where(s => person.Gender.ToLower() == "male" ? s.HusbandId == person.Id : s.WifeId == person.Id)
                .ToList();

            if (spouseData.Count > 0)
            {
                foreach (var entry in spouseData)
                {
                    var spouse = new Person();
                    if (person.Gender.ToLower() == "male")
                    {
                        spouse = _context.Person.FirstOrDefault(p => p.Id == entry.WifeId);

                    }
                    else
                    {
                        spouse = _context.Person.FirstOrDefault(p => p.Id == entry.HusbandId);
     
                    }

                    spouses.Add(spouse);
                }
            }

            return spouses;
        }

       
        // GET: api/People/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Person>> GetPerson(int id)
        {
            var person = await _context.Person.FindAsync(id);

            if (person == null)
            {
                return NotFound();
            }

            return person;
        }

        // PUT: api/People/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPerson(int id, Person person)
        {
            if (id != person.Id)
            {
                return BadRequest();
            }

            _context.Entry(person).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/People
        [HttpPost]
        public async Task<ActionResult<Person>> PostPerson(Person person)
        {
            _context.Person.Add(person);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPerson", new { id = person.Id }, person);
        }

        // DELETE: api/People/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Person>> DeletePerson(int id)
        {
            var person = await _context.Person.FindAsync(id);
            if (person == null)
            {
                return NotFound();
            }

            _context.Person.Remove(person);
            await _context.SaveChangesAsync();

            return person;
        }

        private bool PersonExists(int id)
        {
            return _context.Person.Any(e => e.Id == id);
        }
    }
}
