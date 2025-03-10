using System.ComponentModel.DataAnnotations.Schema;

namespace PayrollTask.Models.Domain;

public class PTask
{
    public int Id { get; set; }
    public required string Description { get; set; }
    public int PTaskStatusId { get; set; } = 1;
    public virtual PTaskStatus? PTaskStatus { get; set; }
    public DateTime AssignedDate { get; set; }
    public DateTime DueDate { get; set; }
    public DateTime? SubmittedDate { get; set; }
    public int EmployeeId { get; set; }
    public virtual Employee? Employee { get; set; }
    public int AssignedBy { get; set; }
    public string? ResourceFilePath { get; set; }

    [NotMapped]
    public IFormFile? ResourceFile { get; set; }

    public string? SubmissionFilePath { get; set; }

    [NotMapped]
    public IFormFile? SubmissionFile { get; set; }
}
