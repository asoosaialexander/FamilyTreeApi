namespace FamilyTreeApi.Models
{
    public class Relation
    {
        public int Id { get; set; }
        public Person Person { get; set; }
        public Person RelatedTo { get; set; }
        public string Relationship { get; set; }
    }
}