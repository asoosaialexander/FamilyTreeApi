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
    public class FamilyTreeController : ControllerBase
    {
        private readonly FamilyTreeContext _context;

        public FamilyTreeController(FamilyTreeContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public ActionResult<FamilyTree> GetFamilyTree(int id)
        {
            var familyTree = new FamilyTree();
            familyTree.Person = _context.Person.SingleOrDefault(p => p.Id == id);
            if (familyTree.Person is null)
            {
                return NotFound();
            }
            if (familyTree.Person.FatherId != null)
            {
                familyTree.Father = _context.Person.SingleOrDefault(f => f.Id == familyTree.Person.FatherId);
            }
            if (familyTree.Person.MotherId != null)
            {
                familyTree.Mother = _context.Person.SingleOrDefault(m => m.Id == familyTree.Person.MotherId);
            }

            if (familyTree.Person.MotherId != null && familyTree.Person.FatherId != null)
            {
                var data = _context.Person
                    .Where(p => p.Id != familyTree.Person.Id && p.MotherId == familyTree.Person.MotherId && p.FatherId == familyTree.Person.FatherId)
                    .ToList();

                if (data.Count > 0)
                {
                    familyTree.Siblings = new List<Sibling>();

                    foreach (var sibling in data)
                    {
                        string relation = string.Empty;

                        if (sibling.BirthYear > 0 && familyTree.Person.BirthYear > 0)
                        {
                            if (sibling.BirthYear < familyTree.Person.BirthYear)
                                relation += "Elder ";
                            if (sibling.BirthYear > familyTree.Person.BirthYear)
                                relation += "Younger ";
                        }

                        if (sibling.Gender.Equals("Male", StringComparison.OrdinalIgnoreCase))
                            relation += "Brother";
                        if (sibling.Gender.Equals("Female", StringComparison.OrdinalIgnoreCase))
                            relation += "Sister";

                        familyTree.Siblings.Add(new Sibling() { Relation = relation, Info = sibling });
                    }
                }
            }

            var spouseData = _context.Spouse
                .Where(s => familyTree.Person.Gender.ToLower() == "male" ? s.HusbandId == familyTree.Person.Id : s.WifeId == familyTree.Person.Id)
                .ToList();

            if (spouseData.Count > 0)
            {
                familyTree.Spouses = new List<SpouseDetail>();

                foreach (var spouse in spouseData)
                {
                    var spouseDetail = new SpouseDetail();
                    if (familyTree.Person.Gender.ToLower() == "male")
                    {
                        spouseDetail.Info = _context.Person.FirstOrDefault(p => p.Id == spouse.WifeId);
                        spouseDetail.Children = _context.Person
                            .Where(p => p.FatherId != null && p.MotherId != null
                                && p.FatherId == familyTree.Person.Id && p.MotherId == spouse.WifeId).ToList();
                    }
                    else
                    {
                        spouseDetail.Info = _context.Person.FirstOrDefault(p => p.Id == spouse.HusbandId);
                        spouseDetail.Children = _context.Person
                            .Where(p => p.FatherId != null && p.MotherId != null
                                && p.MotherId == familyTree.Person.Id && p.FatherId == spouse.HusbandId).ToList();
                    }

                    familyTree.Spouses.Add(spouseDetail);
                }
            }

            return familyTree;
        }
    }
}