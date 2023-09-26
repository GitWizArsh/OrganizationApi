using Microsoft.EntityFrameworkCore;

namespace OrganizationApplication.Models
{
    public class CompanyContext:DbContext
    {
        public CompanyContext(DbContextOptions<CompanyContext> options)
           : base(options)
        {


        }
        public DbSet<CompanyContext> Companies { get; set; } = null!;

    }
}
