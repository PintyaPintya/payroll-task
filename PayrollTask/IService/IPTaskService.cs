using PayrollTask.Models.Domain;
using PayrollTask.Models.Dto;

namespace PayrollTask.IService;

public interface IPTaskService
{
    Task<List<PTask>> GetAll(string status);
    Task<bool> CheckIfStatusExists(string status);
    Task<(bool, string)> Add(PTaskModel pTaskModel);
    Task<PTask?> GetById(int taskId);
    Task UpdateStatus(PTask pTask, int statusId);
    Task SubmitTask(PTask pTask, IFormFile file);
}
