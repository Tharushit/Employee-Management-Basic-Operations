using Microsoft.EntityFrameworkCore;

namespace crud_backend.Models
{
    public class EmployeeContext : DbContext
    {
        public EmployeeContext(DbContextOptions<EmployeeContext> options) : base(options)
        {
            
        }
        public DbSet<Employee> Employees { get; set;}
    }
}
