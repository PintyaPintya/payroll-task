namespace PayrollTask.Models.Dto;

public class EmployeeModel
{
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
    public int Salary { get; set; }
}
