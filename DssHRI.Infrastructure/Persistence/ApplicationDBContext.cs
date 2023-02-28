using Microsoft.EntityFrameworkCore;

namespace DssHRI.Infrastructure.Persistence;
public class ApplicationDBContext : DbContext
{
    public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
    { }
    //public DbSet<Patient> patients { get; set; }
    //public DbSet<User> Users { get; set; }

}
