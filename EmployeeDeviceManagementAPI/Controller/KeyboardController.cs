using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services;
using Model;

namespace Controllers{
    [ApiController]
    [Route("/[controller]")]
    public class KeyboardController:ControllerBase{
        IDeviceService deviceService;
        public KeyboardController(IDeviceService deviceService){
            this.deviceService=deviceService;
        }

        [HttpGet("Employees/{empId}/Keyboards")]
        [Authorize]
        public async Task<IActionResult> GetKeyboards(string empId){
            var keyboards=await deviceService.GetKeyboardsByEmployeeAsync(empId);
            return Ok(keyboards);
        }
        [HttpPost("Employees/{empId}/Keyboards")]
        [Authorize(Roles="Admin")]
        public async Task<IActionResult> CreateKeyboard(string empId,[FromBody] Keyboard keyboard){
            keyboard.empId=empId;
            var newKeyboard=await deviceService.CreateKeyboardAsync(keyboard);
            return CreatedAtAction(nameof(GetKeyboards),new{empId=newKeyboard.empId,keyId=newKeyboard.keyId},newKeyboard);
        }
        [HttpPut("Employees/{empId}/Keyboards/{keyId}")]
        [Authorize(Roles="Admin")]
        public async Task<IActionResult> UpdateKeyboard(string empId,string keyId,[FromBody] Keyboard keyboard){
            await deviceService.UpdateKeyboardAsync(keyId,keyboard);
            return NoContent();
        }
        [HttpDelete("Employees/{empId}/Keyboards/{keyId}")]
        [Authorize(Roles="Admin")]
        public async Task<IActionResult> DeleteKeyboard(string empId, string keyId){
            await deviceService.DeleteKeyboardAsync(keyId);
            return NoContent();
        }
    }
}