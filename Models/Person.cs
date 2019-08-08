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
        public Person Mother {get;set;}
        public Person Father {get;set;}
        public string Gender { get; set; }
        public int BirthYear { get; set; }
        public string Occupation { get; set; }
        public string Residence { get; set; }
    }
}
