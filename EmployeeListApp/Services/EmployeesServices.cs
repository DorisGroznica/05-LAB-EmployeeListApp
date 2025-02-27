﻿using EmployeeListApp.Data;

namespace EmployeeListApp.Services
{
    public class EmployeesServices
    {
        AppDbContext _context;
        public EmployeesServices(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<Employee>> GetEmployeesAsync()
        {
            var result = _context.Employees;
            return await Task.FromResult(result.ToList());
        }
        public async Task<Employee> GetEmployeeByIdAsync(string id) => await _context.Employees.FindAsync(id);
        public async Task<Employee> InsertEmployeeAsync(Employee employee)
        {
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();
            return employee;
        }
   
        public async Task<Employee> UpdateEmployeeAsync(string id, Employee e)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
                return null;
            employee.FullName = e.FullName;
            employee.Id = e.Id;
            employee.Salary = e.Salary;
            employee.Department = e.Department;
            _context.Employees.Update(employee);
            await _context.SaveChangesAsync();
            return employee;
        }
        public async Task<Employee> DeleteEmployeeAsync(string id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
                return null;
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
            return employee;
        }
        private bool EmployeeExists(string id)
        {
            return _context.Employees.Any(e => e.Id == id);
        }
    }
}
