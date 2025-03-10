namespace PayrollTask.Models.Dto;

public class PTaskModel
{
    public required string Description { get; set; }
    public DateTime DueDate { get; set; }
    public int EmployeeId { get; set; }
    public int AssignedBy { get; set; }
    public string? ResourceFilePath { get; set; }
    public IFormFile? ResourceFile { get; set; }
}
