using Microsoft.EntityFrameworkCore;

namespace FmsTest.Models
{
    public class ApplicationContext : DbContext
    {
        public DbSet<PersonContact> PersonContacts { get; set; }

        public DbSet<CompanyContact> CompanyContacts { get; set; }

        public DbSet<UsersGroupContact> UsersGroups { get; set; }

        public DbSet<Address> Address { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { 
            Database.EnsureCreated();
        }
    }
}
