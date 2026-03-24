using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EmployeeManagement.Shared.Models;
using Backend.Data;

namespace EmployeeManagement.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DepartmentsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Department>>> GetDepartments()
        {
            return await _context.Departments.OrderBy(d => d.Department_Name).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Department>> GetDepartment(int id)
        {
            var department = await _context.Departments.FindAsync(id);
            if (department == null) return NotFound();
            return department;
        }

        [HttpPost]
        public async Task<ActionResult<Department>> PostDepartment(Department department)
        {
            bool isDuplicate = await _context.Departments
                .AnyAsync(d => d.Department_Name.ToLower() == department.Department_Name.ToLower());

            if (isDuplicate)
            {
                return BadRequest("ชื่อแผนกนี้มีอยู่ในระบบแล้ว!");
            }

            _context.Departments.Add(department);
            await _context.SaveChangesAsync();

            return Ok(department);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutDepartment(int id, Department department)
        {
            if (id != department.Department_ID) return BadRequest();

            bool isDuplicate = await _context.Departments
                .AnyAsync(d => d.Department_Name.ToLower() == department.Department_Name.ToLower()
                            && d.Department_ID != id);

            if (isDuplicate)
            {
                return BadRequest("ชื่อแผนกมีซ้ำกับแผนกอื่นในระบบแล้ว!");
            }

            _context.Entry(department).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DepartmentExists(id)) return NotFound();
                else throw;
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDepartment(int id)
        {
            var department = await _context.Departments.FindAsync(id);
            if (department == null) return NotFound();

            var hasEmployees = await _context.Employees.AnyAsync(e => e.Department_ID == id);
            if (hasEmployees)
            {
                return BadRequest("ไม่สามารถลบแผนกนี้ได้ เนื่องจากยังมีพนักงานอยู่");
            }

            _context.Departments.Remove(department);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DepartmentExists(int id) => _context.Departments.Any(e => e.Department_ID == id);
    }
}