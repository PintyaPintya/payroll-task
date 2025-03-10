namespace PayrollTask.Models.Domain;

public class Employee
{
    public int EmployeeId { get; set; }
    public required string Name { get; set; }
    public required string Email { get; set; }
    public byte[] PasswordHash { get; set; } = [];
    public byte[] PasswordSalt { get; set; } = [];
    public int Salary { get; set; }
    public bool IsActive { get; set; } = true;
}