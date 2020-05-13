using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FamilyTreeApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace FamilyTreeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonDetailsController : ControllerBase
    {
        private readonly FamilyTreeContext _context;

        public PersonDetailsController(FamilyTreeContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public ActionResult<PersonDetail> Get(int id)
        {
            var personDetail = new PersonDetail();
            var person = _context.Person.SingleOrDefault(p => p.Id == id);
            if (person is null)
            {
                return NotFound();
            }

            personDetail.Id = person.Id;
            personDetail.Name = person.Name;
            personDetail.Gender = person.Gender;
            var father = _context.Person.SingleOrDefault(p => p.Id == person.FatherId);
            if (father != null)
                personDetail.FatherName = father.Name;
            var mother = _context.Person.SingleOrDefault(p => p.Id == person.MotherId);
            if (mother != null)
                personDetail.MotherName = mother.Name;
            personDetail.Occupation = person.Occupation;
            personDetail.Residence = person.Residence;
            personDetail.BirthYear = person.BirthYear;

            if (person.MotherId != null && person.FatherId != null)
            {
                var data = _context.Person
                    .Where(p => p.Id != person.Id && p.MotherId == person.MotherId && p.FatherId == person.FatherId)
                    .ToList();

                if (data.Count > 0)
                {
                    personDetail.Siblings = new List<Sibling>();

                    foreach (var sibling in data)
                    {
                        string relation = string.Empty;
                        if (sibling.BirthYear < person.BirthYear)
                            relation += "Elder ";
                        if (sibling.BirthYear > person.BirthYear)
                            relation += "Younger ";

                        if (sibling.Gender.Equals("Male", StringComparison.OrdinalIgnoreCase))
                            relation += "Brother";
                        if (sibling.Gender.Equals("Female", StringComparison.OrdinalIgnoreCase))
                            relation += "Sister";

                        personDetail.Siblings.Add(new Sibling() { Relation = relation, Info = sibling });
                    }
                }
            }

            var spouseData = _context.Spouse
                .Where(s => person.Gender.ToLower() == "male" ? s.HusbandId == person.Id : s.WifeId == person.Id)
                .ToList();

            if (spouseData.Count > 0)
            {
                personDetail.Spouses = new List<SpouseDetail>();

                foreach (var spouse in spouseData)
                {
                    var spouseDetail = new SpouseDetail();
                    if (person.Gender.ToLower() == "male")
                    {
                        spouseDetail.Info = _context.Person.FirstOrDefault(p => p.Id == spouse.WifeId);
                        spouseDetail.Children = _context.Person
                            .Where(p => p.FatherId != null && p.MotherId != null
                                && p.FatherId == person.Id && p.MotherId == spouse.WifeId).ToList();
                    }
                    else
                    {
                        spouseDetail.Info = _context.Person.FirstOrDefault(p => p.Id == spouse.HusbandId);
                        spouseDetail.Children = _context.Person
                            .Where(p => p.FatherId != null && p.MotherId != null
                                && p.MotherId == person.Id && p.FatherId == spouse.HusbandId).ToList();
                    }

                    personDetail.Spouses.Add(spouseDetail);
                }
            }

            return personDetail;
        }
    }
}
