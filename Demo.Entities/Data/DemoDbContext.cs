using Demo.Entities.Entities;
using Microsoft.EntityFrameworkCore;



namespace Demo.Entities.Data
{
    public class DemoDbContext:DbContext

    {
        public DemoDbContext(DbContextOptions<DemoDbContext> options): base(options) { }

        public DbSet<Employee> Employee { get; set; }
        public DbSet<EmployeeHistory> EmployeeHistory { get; set; }

        public DbSet<Person> Person { get; set; }
        public DbSet<Position> Position { get; set; }

       

    }
}
