using Microsoft.AspNetCore.Mvc;
using PayrollTask.IService;
using PayrollTask.Models.Dto;

namespace PayrollTask.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EmployeeController : ControllerBase
{
    private readonly IEmployeeService _employeeService;

    public EmployeeController(IEmployeeService employeeService)
    {
        _employeeService = employeeService;
    }


    [HttpGet("/api/Employees")]
    public async Task<ActionResult> GetAllActive()
    {
        var users = await _employeeService.GetAllActive();
        return Ok(users);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult> GetById(int id)
    {
        var user = await _employeeService.GetById(id);
        if (user == null) return NotFound();

        return Ok(user);
    }

    [HttpPost]
    public async Task<ActionResult> Add(EmployeeModel employeeModel)
    {
        bool emailExists = await _employeeService.CheckIfEmailExists(employeeModel.Email);
        if (emailExists)
        {
            return BadRequest("Email already exists");
        }

        await _employeeService.Add(employeeModel);
        return Ok(employeeModel);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> Update(int id, EmployeeModel employeeModel)
    {
        var employee = await _employeeService.GetById(id);
        if (employee == null) return NotFound();

        bool isValidEmail = await _employeeService.CheckEmailValidity(employee.Email, employeeModel.Email);
        if (!isValidEmail)
        {
            return BadRequest("Email already exists");
        }

        await _employeeService.Update(employee, employeeModel);
        return Ok(employeeModel);
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Remove(int id)
    {
        var employee = await _employeeService.GetById(id);
        if (employee == null) return NotFound();

        await _employeeService.Remove(employee);
        return NoContent();
    }
}
