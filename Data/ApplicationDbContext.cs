using HostelFinder.Models;
using Microsoft.EntityFrameworkCore;

namespace HostelFinder.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Hostel> Hostels { get; set; }
        public DbSet<Student> Students { get; set; }

    }
}
