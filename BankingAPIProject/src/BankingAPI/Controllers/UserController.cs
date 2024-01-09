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

        /// <summary>
        /// Registers a new user.
        /// </summary>
        /// <param name="registration">User registration details.</param>
        /// <returns>Response for the registration process.</returns>
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegistrationDto registration)
        {
            var user = await _userService.RegisterAsync(registration);
            if (user != null)
            {
                return Ok(new { message = "Registration successful" });
            }
            return BadRequest(new { message = "Registration failed" });
        }

        /// <summary>
        /// Authenticates a user and provides a JWT token.
        /// </summary>
        /// <param name="login">User login details.</param>
        /// <returns>Authenticated user info and a JWT token.</returns>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto login)
        {
            var user = await _userService.AuthenticateAsync(login.Username, login.Password);

            if (user == null)
            {
                return BadRequest(new { message = "Username or password is incorrect" });
            }

            var token = await _userService.GenerateJwtTokenAsync(user);

            return Ok(new { userId = user.UserId, username = user.Username, token = token });
        }

        /// <summary>
        /// Updates user information.
        /// </summary>
        /// <param name="userId">User ID to update.</param>
        /// <param name="update">User update details.</param>
        /// <returns>Response for the update operation.</returns>
        [HttpPut("update/{userId}")]
        public async Task<IActionResult> UpdateUser(int userId, [FromBody] UserUpdateDto update)
        {
            var result = await _userService.UpdateUserAsync(userId, update);
            if (result)
            {
                return Ok(new { message = "User updated successfully" });
            }
            return BadRequest(new { message = "User update failed" });
        }

        /// <summary>
        /// Updates the role of a user. Accessible only by admin users.
        /// </summary>
        /// <param name="userId">User ID whose role is to be updated.</param>
        /// <param name="roleUpdate">User role update details.</param>
        /// <returns>Response for the role update operation.</returns>
        [Authorize(Roles = "admin")]
        [HttpPut("update-role/{userId}")]
        public async Task<IActionResult> UpdateUserRole(int userId, [FromBody] UserRoleUpdateDto roleUpdate)
        {
            var result = await _userService.UpdateUserRoleAsync(userId, roleUpdate);
            if (result)
            {
                return Ok(new { message = "User role updated successfully" });
            }
            return BadRequest(new { message = "User role update failed" });
        }
    }
}
