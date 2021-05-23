using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using WebApi.DTO;
using WebApi.Models;
using WebApi.Services;
using WebApi.Services.Interface;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [EnableCors("MyPolicy")]
    public class TasksController : ControllerBase
    {
        private readonly ITaskService _taskService;
        private readonly INotificationService _notificationService;

        public TasksController(ITaskService taskService, INotificationService notificationService)
        {
            _taskService = taskService;
            _notificationService = notificationService;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] int pageIndex, int pageSize, long userId)
        {
            return Ok(await _taskService.GetTasks(pageIndex, pageSize, userId));
        }

        [HttpGet("Notifications")]
        public async Task<IActionResult> Get()
        {
            return Ok(await _notificationService.GetNotifications());
        }

        [HttpPost]
        public async Task<IActionResult> Post(TaskRequestDTO dto)
        {
            await _taskService.PostTask(dto);
            return StatusCode(200);
        }

        [HttpPut]
        public async Task<IActionResult> Put(TaskRequestDTO dto)
        {
            await _taskService.PutTask(dto);
            return NoContent();
        }

        [HttpPut("notification/{id}")]
        public async Task<IActionResult> Put([FromRoute] long id)
        {
            await _notificationService.UpdateRead(id);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] long id)
        {
            await _taskService.DeleteTask(id);
            return NoContent();
        }
    }
}
