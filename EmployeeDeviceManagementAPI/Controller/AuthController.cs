using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model;
using Services;

namespace Controllers{
    [ApiController]
    [Route("/[controller]")]
    public class AuthController:ControllerBase{
        IAuthService authService;
        public AuthController(IAuthService authService){
            this.authService=authService;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest){
            try{
                var token=await authService.LoginAsync(loginRequest.username,loginRequest.password);
                return Ok(new{Token=token});
            }
            catch (Exception ex){
                return Unauthorized(ex.Message);
            }
        }
        [HttpPost("Register")]
        [Authorize(Roles="Admin")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest registerRequest){
            try{
                await authService.RegisterUserAsync(registerRequest.username,registerRequest.Password,registerRequest.role,registerRequest.empId);
                return Ok("User registered successfully");
            }
            catch (Exception ex){
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("Users")]
        [Authorize(Roles="Admin")]
        public async Task<IActionResult> GetAllUsers(){
            var users=await authService.GetAllUserAsync();
            return Ok(users);
        }
        [HttpPut("Users/{empId}")]
        [Authorize(Roles="Admin")]
        public async Task<IActionResult> UpdateUser(string empId,[FromBody] RegisterRequest registerRequest){
            var user=new User{
                userName=registerRequest.username,
                password=registerRequest.Password,
                role=registerRequest.role
            };
            await authService.UpdateUserAsync(empId,user);
            return NoContent();
        }
        [HttpDelete("Users/{empId}")]
        [Authorize(Roles="Admin")]
        public async Task<IActionResult> DeleteUser(string empId){
            await authService.DeleteUserAsync(empId);
            return NoContent();
        }
    }
}