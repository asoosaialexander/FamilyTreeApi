using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace FamilyTreeApi.Models
{
    public class FamilyTreeContext : DbContext
    {
        public FamilyTreeContext (DbContextOptions<FamilyTreeContext> options)
            : base(options)
        {
        }

        public DbSet<Person> Person { get; set; }
        public DbSet<Relation> Relation { get; set; }
    }
}
