using Microsoft.EntityFrameworkCore;
using PayrollTask.Data;
using PayrollTask.IService;
using PayrollTask.Models.Domain;
using PayrollTask.Models.Dto;

namespace PayrollTask.Service;

public class PTaskService : IPTaskService
{
    private readonly ApplicationDbContext _context;
    private readonly IEmployeeService _employeeService;

    public PTaskService(ApplicationDbContext context, IEmployeeService employeeService)
    {
        _context = context;
        _employeeService = employeeService;
    }

    public async Task<List<PTask>> GetAll(string status)
    {
        if(status == "")
        {
            return await _context.PTasks
                .Include(p => p.PTaskStatus)
                .ToListAsync();
        }

        return await _context.PTasks
                .Include(p => p.PTaskStatus)
            .Where(p => p.PTaskStatus != null &&
                p.PTaskStatus.Name.ToLower() == status.ToLower())
            .ToListAsync();
    }

    public async Task<bool> CheckIfStatusExists(string status)
    {
        return await _context.PTaskStatuses.AnyAsync(s => s.Name.ToLower() == status.ToLower());
    }

    public async Task<(bool, string)> Add(PTaskModel pTaskModel)
    {
        var employee = await _employeeService.GetById(pTaskModel.EmployeeId);
        if (employee == null)
            return (false, "No such employee exists");

        bool isTaskAssigned = await CheckIfTaskAlreadyAssigned(employee);
        if (isTaskAssigned)
            return (false, "A task is already assigned to the employee");

        var pTask = new PTask()
        {
            Description = pTaskModel.Description,
            PTaskStatusId = 1,
            AssignedDate = DateTime.UtcNow,
            DueDate = pTaskModel.DueDate,
            EmployeeId = pTaskModel.EmployeeId,
            AssignedBy = pTaskModel.AssignedBy
        };

        if (pTaskModel.ResourceFile != null && pTaskModel.ResourceFile.Length > 0)
        {
            pTask.ResourceFilePath = await UploadFile(pTaskModel.ResourceFile, "Input");
        }

        await _context.PTasks.AddAsync(pTask);
        await _context.SaveChangesAsync();

        return (true, "");
    }

    private async Task<bool> CheckIfTaskAlreadyAssigned(Employee employee)
    {
        return await _context.PTasks
            .AnyAsync(t => t.EmployeeId == employee.EmployeeId && t.PTaskStatusId == 1);
    }

    private async Task<string> UploadFile(IFormFile file, string direction)
    {
        var directory = Path.Combine(Directory.GetCurrentDirectory(), direction + "Files");
        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        var filePath = Guid.NewGuid() + "_" + Path.GetFileName(file.FileName);
        var fullPath = Path.Combine(directory, filePath);

        using(var stream = new FileStream(fullPath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        return filePath;
    }
    public async Task SubmitTask(PTask pTask, IFormFile file)
    {
        pTask.SubmissionFilePath = await UploadFile(file, "Output");
        pTask.PTaskStatusId = 2;
        _context.PTasks.Update(pTask);
        await _context.SaveChangesAsync();
    }

    public async Task<PTask?> GetById(int taskId)
    {
        return await _context.PTasks
            .Include(p => p.PTaskStatus)
            .FirstOrDefaultAsync(p => p.Id == taskId);
    }

    public async Task UpdateStatus(PTask pTask, int statusId)
    {
        pTask.PTaskStatusId = statusId;
        _context.PTasks.Update(pTask);
        await _context.SaveChangesAsync();
    }
}
