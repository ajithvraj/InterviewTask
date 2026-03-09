using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.Application.DTOs.AuthDTOs;
using TaskManagement.Application.Interfaces;
using TaskManagement.Api.Common;

namespace TaskManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly IAuthService _authservice; 

        public AuthController (IAuthService authService)
        {
            _authservice = authService;
        }

        [HttpPost("login")] 

        public IActionResult LoginAsync([FromBody]  LoginRequestDto dto)
        {

        
 

            var user = _authservice.Login(dto.Username, dto.Password);

            if(user == null)
            {
                return Unauthorized(new ApiResponseModel<object>
                {
                    Success = false,
                    Message = "Inavalid username or password"
                    

                });

            }

            var respose = new LoginResponseDto
            {
                UserName = user.UserName,
                Role = user.Role

            };

            return Ok(new ApiResponseModel<LoginResponseDto>
            {
                Success = true,
                Message = "Login success",
                Data = respose

            });

        }


    }
}
