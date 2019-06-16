using Microsoft.EntityFrameworkCore;

namespace FamilyTreeApi.Models
{
    public class FamilyTreeContext : DbContext
    {
        public FamilyTreeContext(DbContextOptions<FamilyTreeContext> options)
            : base(options)
        {
        }
        DbSet<Person> Person { get; set; }
    }
}