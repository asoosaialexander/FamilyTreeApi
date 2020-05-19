using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FamilyTreeApi.Models
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int IsAlive { get; set; }
        public int? MotherId { get; set; }
        public int? FatherId { get; set; }
        public string Gender { get; set; }
        public int BirthYear { get; set; }
        public string Occupation { get; set; }
        public string Residence { get; set; }
        public string Photo { get; set; }
        public string MaritalStatus {get;set;}
    }
}
