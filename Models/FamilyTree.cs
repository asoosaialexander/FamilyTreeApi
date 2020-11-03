using System.Collections.Generic;

namespace FamilyTreeApi.Models
{
    public class FamilyTree
    {
        public Person Person { get; set; }
        public Person Father { get; set; }
        public Person Mother { get; set; }
        public List<SpouseDetail> Spouses { get; set; }
        public List<Sibling> Siblings { get; set; }
    }
}