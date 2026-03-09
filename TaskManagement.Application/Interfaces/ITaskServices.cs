using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Application.DTOs;

namespace TaskManagement.Application.Interfaces
{
    public interface ITaskServices
    {

        Task<TaskResponseDto> CreateTaskAsync(CreateTaskDto dto, string userId);
        Task<List<TaskResponseDto>> GetUserTaskAsync(string userId);
        Task<List<TaskResponseDto>> GetAllTaskAsync();
        Task UpdateTask(int id,UpdateTaskDto dto, string userId);

        Task MarkTaskCompletedAsync(int id);



    }
} 
