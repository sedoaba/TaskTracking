using Microsoft.AspNetCore.Mvc;

namespace TaskTracking.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController : ControllerBase
    {


        [HttpGet]
        public Task<IActionResult> GetTasks()
        {
            return null;
        }
    }
}
