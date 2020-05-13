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
        public DbSet<Spouse> Spouse {get;set;}

        // protected override void OnModelCreating(ModelBuilder modelBuilder){
        //     modelBuilder.Entity<Person>()
        //         .HasOne(p=>p.Father)
        //         .WithOne(p=>p.Mother);
        // }
    }
}
