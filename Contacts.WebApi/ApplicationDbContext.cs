using Microsoft.EntityFrameworkCore;
using Contacts.WebApi.Models;

namespace Contacts.WebApi
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {
            
        }

        public DbSet<Contact> Contacts { get; set; }
    }
}
