using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Application.DTOs;
using TaskManagement.Application.Interfaces;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Interfaces;

namespace TaskManagement.Application.Services
{
    public class TaskService : ITaskServices
    {
        private readonly ITaskRepository _repo;

        public TaskService(ITaskRepository repo)
        {
            _repo = repo;
        }

        public async Task<TaskResponseDto> CreateTaskAsync(CreateTaskDto dto, string userId)
        {

            var task = new TaskItem
            {
                Title = dto.Title,
                Description= dto.Description,
                CreatedAt = DateTime.UtcNow,
                DueDate = dto.DueDate,
                IsCompleted = false,
                OwnerUserId= userId,


            };

            var completed = await _repo.CreateTaskAsync(task);

            return new TaskResponseDto
            {
                Id = completed.Id,
                Title = completed.Title,
                Description = completed.Description,
                CreatedAt = DateTime.UtcNow,
                DueDate = completed.DueDate,
                IsCompleted = completed.IsCompleted


            };


        }

        public async Task<List<TaskResponseDto>> GetUserTaskAsync(string userId)
        {

            var tasks = await _repo.GetTasksByUserAsync(userId);

            return tasks.Select(t => new TaskResponseDto
            {
                Id = t.Id,
                Title = t.Title,
                Description = t.Description,
                CreatedAt = DateTime.UtcNow,
                DueDate = t.DueDate,
                IsCompleted = t.IsCompleted


            }).ToList();

        }

        public async Task<List<TaskResponseDto>> GetAllTaskAsync()
        {

            var tasks = await _repo.GetAllTaskAsync();

            return tasks.Select(t => new TaskResponseDto
            {
                Id = t.Id,
                Title = t.Title,
                Description = t.Description,
                CreatedAt = DateTime.UtcNow,
                DueDate = t.DueDate,
                IsCompleted = t.IsCompleted


            }).ToList();

        }

        public async Task UpdateTask(int id, UpdateTaskDto dto, string userId)
        {

            var update = await _repo.GetTaskByIdAsync(id); 

            if(update == null)
            {
                throw new Exception("Task not found");
            }

            if (update.OwnerUserId != userId) { throw new Exception("you can only update your own task"); }

                update.Title = dto.Title;
                update.Description = dto.Description;
                update.DueDate = dto.DueDate; 

                await _repo.UpdateTaskAsync(update);

        }

        public async Task MarkTaskCompletedAsync(int id)
        {
            var comple = await _repo.GetTaskByIdAsync(id);

            if( comple == null ) { throw new Exception("Task not found"); } 


            comple.IsCompleted = true;
            await _repo.UpdateTaskAsync(comple);


        }
    }
}
