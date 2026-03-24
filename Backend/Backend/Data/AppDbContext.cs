using Microsoft.EntityFrameworkCore;

using EmployeeManagement.Shared.Models;

namespace Backend.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().HasKey(e => e.Employee_ID);
            modelBuilder.Entity<Department>().HasKey(d => d.Department_ID);
        }
    }
}