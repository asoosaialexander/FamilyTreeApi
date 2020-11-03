using System.Collections.Generic;

namespace FamilyTreeApi.Models
{
    public class PersonDetail
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string IsAlive { get; set; }
        public int? FatherId { get; set; }
        public string FatherName { get; set; }
        public int? MotherId { get; set; }
        public string MotherName { get; set; }
        public string Gender { get; set; }
        public int BirthYear { get; set; }
        public string Occupation { get; set; }
        public string Residence { get; set; }
        public string Photo { get; set; }
        public string MaritalStatus {get;set;}
        public List<SpouseDetail> Spouses { get; set; }
        public List<Sibling> Siblings { get; set; }
    }
}