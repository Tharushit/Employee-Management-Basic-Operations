﻿using crud_backend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace crud_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeContext _employeeContext; // Fix the variable name here.

        public EmployeeController(EmployeeContext employeeContext)
        {
            _employeeContext = employeeContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployees()
        {
            var employees = await _employeeContext.Employees.ToListAsync();
            if (employees == null || employees.Count == 0) 
            {
                return NotFound();
            }
            return employees;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployee(int id)
        {
            var employee = await _employeeContext.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound(); 
            }
            return employee;
        }

        [HttpPost]
        public async Task<ActionResult<Employee>> PostEmployee(Employee employee) 
        { 
            _employeeContext.Employees.Add(employee);
            await _employeeContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetEmployee), new {id = employee.Id}, employee);
        }

        [HttpPut("{id}")]

        public async Task<ActionResult> PutEmployee(int id, Employee employee) 
        { 
            if (id != employee.Id) 
            {
                return BadRequest();
            }
            _employeeContext.Entry(employee).State = EntityState.Modified;

            try 
            { 
                await _employeeContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) 
            {
                throw;
            }

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Deleteemployee(int id)
        {
            var employee = await _employeeContext.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            _employeeContext.Employees.Remove(employee);
            await _employeeContext.SaveChangesAsync();

            return Ok();
        }
    }
}
