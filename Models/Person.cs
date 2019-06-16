using System.Collections.Generic;

namespace FamilyTreeApi.Models{
    public class Person{
        public int Id {get;set;}
        public string Name {get;set;}
        public Gender Gender{get;set;}
        public List<Person> Relationships {get;set;}
    }

    public enum Gender{
        Male, Female
    }
}