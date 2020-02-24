using Kairos.Models;
using Microsoft.EntityFrameworkCore;

namespace Kairos.Models
{
    public class HomeContext : DbContext
    {
        public HomeContext(DbContextOptions options) : base(options){}

        public DbSet<User> Users {get; set;}
    }
}