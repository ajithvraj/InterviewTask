using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Domain.Entities;

namespace TaskManagement.Domain.Interfaces
{
    public interface ITaskRepository
    {
        Task<TaskItem> CreateTaskAsync(TaskItem task); 
        Task<TaskItem?> GetTaskByIdAsync(int Id);
        Task<List<TaskItem>> GetTasksByUserAsync(string userId);
        Task<List<TaskItem>> GetAllTaskAsync();
        Task UpdateTaskAsync(TaskItem task);


    }
}
