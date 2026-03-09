using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Application.Interfaces;
using TaskManagement.Application.Models;

namespace TaskManagement.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly List<User> _users = new()
        {
            new User
            {
                UserName = "admin",
                Password = "admin123",
                Role = "Admin"

            } ,
            new User
            {
                UserName = "TestUser", 
                Password = "user123",
                Role = "User"
            }

        };

        public User? Login(string username, string password)
        {

            return _users.FirstOrDefault(u => u.UserName == username && u.Password == password);


        }



    }
}
