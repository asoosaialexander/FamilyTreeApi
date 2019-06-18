using Microsoft.EntityFrameworkCore;

namespace FamilyTreeApi.Models
{
    public class FamilyTreeContext : DbContext
    {
        public FamilyTreeContext(DbContextOptions<FamilyTreeContext> options)
            : base(options)
        {
        }
        public DbSet<Person> Persons { get; set; }

        // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        // {
        //     optionsBuilder.UseMySQL("server=localhost;database=FamilyTree;user=root;password=Al3x@nder");
        // }
    }
}