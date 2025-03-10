using Microsoft.EntityFrameworkCore;
using PayrollTask.Data;
using PayrollTask.IService;
using PayrollTask.Models;
using PayrollTask.Models.Domain;
using PayrollTask.Models.Dto;

namespace PayrollTask.Service;

public class EmployeeService : IEmployeeService
{
    private readonly ApplicationDbContext _context;

    public EmployeeService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Employee>> GetAllActive()
    {
        return await _context.Employees
            .Where(e => e.IsActive)
            .ToListAsync();
    }

    public async Task<Employee?> GetById(int employeeId)
    {
        return await _context.Employees
            .FirstOrDefaultAsync(e => e.EmployeeId == employeeId);
    }

    public async Task<bool> CheckIfEmailExists(string email)
    {
        return await _context.Employees.AnyAsync(e => e.IsActive
            && e.Email.ToLower() == email.ToLower());
    }

    public async Task Add(EmployeeModel model)
    {
        var password = PasswordHasher.CreatePasswordHash(model.Password);

        var employee = new Employee()
        {
            Name = model.Name,
            Email = model.Email,
            Salary = model.Salary,
            PasswordHash = password.Item1,
            PasswordSalt = password.Item2
        };

        await _context.Employees.AddAsync(employee);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> CheckEmailValidity(string existingEmail, string modelEmail)
    {
        if (existingEmail == modelEmail)
        {
            return true;
        }
        else
        {
            bool emailExists = await CheckIfEmailExists(modelEmail);
            return !emailExists;
        }
    }

    public async Task Update(Employee employee, EmployeeModel model)
    {
        employee.Name = model.Name;
        employee.Email = model.Email;
        employee.Salary = model.Salary;

        var password = PasswordHasher.CreatePasswordHash(model.Password);
        employee.PasswordHash = password.Item1;
        employee.PasswordSalt = password.Item2;

        _context.Employees.Update(employee);
        await _context.SaveChangesAsync();
    }

    public async Task Remove(Employee employee)
    {
        employee.IsActive = false;

        _context.Employees.Update(employee);
        await _context.SaveChangesAsync();
    }
}
