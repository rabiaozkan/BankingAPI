using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using BankingAPI.DTOs;
using BankingAPI.Services;

namespace BankingAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegistrationDto registration)
        {
            try
            {
                // DTO'dan gelen verileri kullanarak kullanıcı kaydı işlemleri
                var user = await _userService.RegisterAsync(registration);
                if (user != null)
                {
                    return Ok(new
                    {
                        message = "Registration successful"
                    });
                }
                return BadRequest(new
                {
                    message = "Registration failed"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    message = ex.Message
                });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto login)
        {
            try
            {
                // Kullanıcı adı ve parola doğrulaması
                var user = await _userService.AuthenticateAsync(login.Username, login.Password);

                if (user == null)
                {
                    return BadRequest(new
                    {
                        message = "Username or password is incorrect"
                    });
                }

                // JWT token oluşturma işlemi (bu kısım servis katmanında gerçekleşir)
                var token = _userService.GenerateJwtTokenAsync(user);

                // Başarılı yanıt döndür
                return Ok(new
                {
                    userId = user.UserId,
                    username = user.Username,
                    token = token
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    message = ex.Message
                });
            }
        }

        [HttpPut("update/{userId}")]
        public async Task<IActionResult> UpdateUser(int userId, [FromBody] UserUpdateDto update)
        {
            try
            {
                // Kullanıcı bilgi güncelleme işlemleri
                var result = await _userService.UpdateUserAsync(userId, update);
                if (result)
                {
                    return Ok(new
                    {
                        message = "User updated successfully"
                    });
                }
                return BadRequest(new
                {
                    message = "User update failed"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    message = ex.Message
                });
            }
        }

        [Authorize(Roles = "admin")] // Yalnızca admin rollerine erişim izni
        [HttpPut("update-role/{userId}")]
        public async Task<IActionResult> UpdateUserRole(int userId, [FromBody] UserRoleUpdateDto roleUpdate)
        {
            try
            {
                var result = await _userService.UpdateUserRoleAsync(userId, roleUpdate);
                if (result)
                {
                    return Ok(new { message = "User role updated successfully" });
                }
                return BadRequest(new { message = "User role update failed" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
