using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TaskManagement.Api.Common;
using TaskManagement.Application.DTOs;
using TaskManagement.Application.Interfaces;

namespace TaskManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskManagementController : ControllerBase
    {

        private readonly ITaskServices _taskService;

        public TaskManagementController(ITaskServices taskService)
        {

            _taskService = taskService;
        }

        [HttpPost("CreateTask")]

        public async Task<IActionResult> CreateTask([FromBody] CreateTaskDto dto)
        {
            var userId = Request.Headers["x-user-id"].ToString();
            var role = Request.Headers["x-user-role"].ToString();

            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(role))
            {

                return Unauthorized(new ApiResponseModel<object>
                {
                    Success = false,
                    Message = "Login required"

                });

             }
            var result = await _taskService.CreateTaskAsync(dto, userId);

            return Ok(new ApiResponseModel<TaskResponseDto>
            {
                Success = true,
                Message = "Task Created",
                Data = result


            });
        }

        [HttpGet]
        public async Task<IActionResult> ViewTask()
        {
            var Role = Request.Headers["x-user-role"].ToString();
            var userId = Request.Headers["x-user-id"].ToString();

            if (Role == "Admin")
            {

                var task = await _taskService.GetAllTaskAsync();

                return Ok(new ApiResponseModel<List<TaskResponseDto>>
                {
                    Success = true,
                    Message = "All tasks retrieved",
                    Data = task
                });

            }
            var userTasks = await _taskService.GetUserTaskAsync(userId);

            return Ok(new ApiResponseModel<List<TaskResponseDto>>
            {
                Success = true,
                Message = "User tasks retrieved",
                Data = userTasks
            });





        }
        [HttpPut("{id}/UpdateTask")]
        public async Task<IActionResult> UpdateTask(int id, [FromBody] UpdateTaskDto dto)
        {
            var userId = Request.Headers["x-user-id"].ToString();

            await _taskService.UpdateTask(id, dto, userId);

            return Ok(new ApiResponseModel<object>
            {
                Success = true,
                Message = "Task updated successfully"
            });
        }

        [HttpPut("{id}/MarkCompleted")]
        public async Task<IActionResult> MarkTaskCompleted(int id)
        {
            var role = Request.Headers["x-user-role"].ToString();

            if (role != "Admin")
            {
                return StatusCode(403, new ApiResponseModel<object>
                {
                    Success = false,
                    Message = "Only Admin can mark tasks as completed"
                });
            }

            await _taskService.MarkTaskCompletedAsync(id);

            return Ok(new ApiResponseModel<object>
            {
                Success = true,
                Message = "Task marked as completed"
            });


        }
    }
}
