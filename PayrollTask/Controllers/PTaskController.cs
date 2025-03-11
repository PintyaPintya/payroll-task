using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PayrollTask.IService;
using PayrollTask.Models.Dto;

namespace PayrollTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PTaskController : ControllerBase
    {
        private readonly IPTaskService _pTaskService;

        public PTaskController(IPTaskService pTaskService)
        {
            _pTaskService = pTaskService;
        }

        [HttpGet("/api/PTasks")]
        public async Task<ActionResult> GetAll(string status = "")
        {
            if (!status.IsNullOrEmpty())
            {
                bool statusExists = await _pTaskService.CheckIfStatusExists(status);
                if (!statusExists)
                    return NotFound("Status not found");
            }

            var tasks = await _pTaskService.GetAll(status);
            return Ok(tasks);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult> GetById(int id)
        {
            var task = await _pTaskService.GetById(id);
            if (task == null)
                return NotFound("No such task exists");

            return Ok(task);
        }

        [HttpPost]
        public async Task<ActionResult> Add(PTaskModel pTaskModel)
        {
            var result = await _pTaskService.Add(pTaskModel);
            if (!result.Item1)
            {
                return BadRequest(result.Item2);
            }

            return Ok();
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> UpdateStatus(int id, int statusId)
        {
            var task = await _pTaskService.GetById(id);
            if (task == null)
                return NotFound("No such task exists");

            if ((task.PTaskStatusId != 3 && task.PTaskStatusId != 2) && (statusId != 4 && statusId != 5))
                return BadRequest("Invalid task update request");

            await _pTaskService.UpdateStatus(task, statusId);
            return Ok();
        }

        [HttpPut("ScheduleReview/{taskId:int}")]
        public async Task<ActionResult> AssignReview(int taskId)
        {
            var task = await _pTaskService.GetById(taskId);
            if (task == null)
                return NotFound("No such task exists");

            if (task.PTaskStatusId != 2)
                return BadRequest("Invalid task request");

            await _pTaskService.UpdateStatus(task, 3);
            return Ok();
        }

        [HttpPut("Submit/{taskId:int}")]
        public async Task<ActionResult> Submit(int taskId, IFormFile submission)
        {
            var task = await _pTaskService.GetById(taskId);
            if (task == null)
                return NotFound("No such task exists");

            if (task.PTaskStatusId != 1)
                return BadRequest("Invalid task request");

            if (submission == null || submission.Length < 1)
                return BadRequest("Invalid file");

            await _pTaskService.SubmitTask(task, submission);
            return Ok();
        }
    }
}