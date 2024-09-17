using EmployeeAdminPortal.Data;
using EmployeeAdminPortal.Models;
using EmployeeAdminPortal.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeAdminPortal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        public EmployeeController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        //Read All the employees
        [HttpGet]
        public IActionResult GetAllEmployees()
        {
            var allEmployees = dbContext.Employees.ToList();
            return Ok(allEmployees);
        }

        //Read a single employee
        [HttpGet]
        [Route("{id:guid}")]
        public IActionResult GetEmployee(Guid id)
        {
            var employee = dbContext.Employees.Find(id);
            if (employee == null) 
            {
                return BadRequest();
            }
            else
            {
                return Ok(employee);
            }

        }

        //Add an employee
        [HttpPost]

        public IActionResult AddEmployees(AddEmployeeDto addEmployeeDto)
        {
            var employeeEntity = new Employee()
            {

                Name = addEmployeeDto.Name,
                Email = addEmployeeDto.Email,
                Phone = addEmployeeDto.Phone,
                Salary = addEmployeeDto.Salary,
            };

            dbContext.Employees.Add(employeeEntity);
            dbContext.SaveChanges();

            return Ok(employeeEntity);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public IActionResult UpdateEmployee(Guid id, UpdateEmployeeDto updateEmployeeDto)
        {
            var employee = dbContext.Employees.Find(id);
            if (employee == null) 
            {
                return NotFound();
            }

            employee.Name = updateEmployeeDto.Name;
            employee.Email = updateEmployeeDto.Email;
            employee.Salary = updateEmployeeDto.Salary;
            employee.Phone = updateEmployeeDto.Phone;

            dbContext.Employees.Update(employee);
            dbContext.SaveChanges();

            return Ok(employee);
        }

        [HttpDelete]
        [Route("{id:guid}")]

        public IActionResult DeleteEmployees(Guid id)
        {
            var employee = dbContext.Employees.Find(id); 
            if (employee == null)
            {
                return BadRequest();
            }

            dbContext.Employees.Remove(employee);
            dbContext.SaveChanges();
            return Ok(employee);
        }
    }
}
