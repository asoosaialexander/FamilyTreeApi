using System;
using System.Collections.Generic;

namespace FamilyTreeApi.Models
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Gender Gender { get; set; }
        public int BirthYear
        {
            get
            {
                return BirthYear;
            }
            set
            {
                if (value > 1900 && value <= DateTime.Now.Year)
                {
                    BirthYear = value;
                }
                else{
                    throw new InvalidOperationException("Invalid Birth Year!");
                }
            }
        }
        public List<Person> Relationships { get; set; }
    }

    public enum Gender
    {
        Male, Female
    }
}