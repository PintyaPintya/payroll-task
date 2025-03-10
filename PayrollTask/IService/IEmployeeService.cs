using PayrollTask.Models.Domain;
using PayrollTask.Models.Dto;

namespace PayrollTask.IService;

public interface IEmployeeService
{
    Task<List<Employee>> GetAllActive();
    Task<Employee?> GetById(int employeeId);
    Task<bool> CheckIfEmailExists(string email);
    Task Add(EmployeeModel employeeModel);
    Task<bool> CheckEmailValidity(string existingEmail, string modelEmail);
    Task Update(Employee employee, EmployeeModel model);
    Task Remove(Employee employee);
}
