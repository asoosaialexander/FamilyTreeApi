using System.Collections.Generic;

namespace FamilyTreeApi.Models
{
    public class SpouseDetail
    {
        public Person Info { get; set; }
        public List<Person> Children { get; set; }
    }
}