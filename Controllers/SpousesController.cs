using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FamilyTreeApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FamilyTreeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpousesController : ControllerBase
    {
        private readonly FamilyTreeContext _context;

        public SpousesController(FamilyTreeContext context)
        {
            _context = context;
        }

        // POST: api/spouses
        [HttpPost]
        public async Task<ActionResult<Person>> PostSpouse(int personId, int spouseId)
        {
            var person = await _context.Person.FindAsync(personId);
            if (person == null)
            {
                return NotFound();
            }

            var spouse = new Spouse();

            if(person.Gender.ToLower()=="male"){
                spouse.HusbandId = personId;
                spouse.WifeId = spouseId;
            }
            else{
                spouse.WifeId = personId;
                spouse.HusbandId = spouseId;
            }
            _context.Spouse.Add(spouse);
            await _context.SaveChangesAsync();

            return Ok();
        }

        // DELETE: api/spouse
        [HttpDelete]
        public async Task<ActionResult<Person>> DeleteSpouse(int personId, int spouseId)
        {
            var person = await _context.Person.FindAsync(personId);
            if (person == null)
            {
                return NotFound();
            }

            int key;

            if(person.Gender.ToLower()=="male"){
                key = _context.Spouse.Where(s=>s.HusbandId==personId && s.WifeId==spouseId).FirstOrDefault().Id;
            }
            else{
                key = _context.Spouse.Where(s=>s.WifeId==personId && s.HusbandId==spouseId).FirstOrDefault().Id;
            }

            var spouse = _context.Spouse.Find(key);
            _context.Spouse.Remove(spouse);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }    
}