using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Domain.Interfaces;
using TaskManagement.Infrastructure.Database;
using TaskManagement.Domain.Entities;

namespace TaskManagement.Infrastructure.Repositories
{
    public class TaskRepository : ITaskRepository 
    {

        private readonly AppDbContext _context;

        public TaskRepository(AppDbContext context)
        {
            _context = context;
        }



        public async Task<TaskItem> CreateTaskAsync(TaskItem task)
        {

            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();
            return task;

        }

        public async Task<TaskItem?> GetTaskByIdAsync(int Id)
        {

            return  _context.Tasks.FirstOrDefault(t => t.Id == Id);

        }

        public async Task<List<TaskItem>> GetTasksByUserAsync(string userId)
        {

            return   _context.Tasks.Where(t => t.OwnerUserId == userId).ToList();

        }

        public async Task<List<TaskItem>> GetAllTaskAsync()
        {

            return  _context.Tasks.ToList();

        }
         

        public async Task UpdateTaskAsync(TaskItem task)
        {

            _context.Tasks.Update(task);
            await _context.SaveChangesAsync();



        }







    }

}
