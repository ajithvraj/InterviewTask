using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Domain.Entities;

namespace TaskManagement.Infrastructure.Database
{
    public class AppDbContext : DbContext
    {

        public DbSet<TaskItem> Tasks => Set<TaskItem>(); 

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }



    }
}
