using PayrollTask.Data;
using PayrollTask.IService;

namespace PayrollTask.Service;

public class EmployeeService : IEmployeeService
{
    private readonly ApplicationDbContext _context;

    public EmployeeService(ApplicationDbContext context)
    {
        _context = context;
    }
}
