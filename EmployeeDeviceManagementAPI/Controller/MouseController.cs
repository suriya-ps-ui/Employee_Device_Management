using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model;
using Services;

namespace Controllers{
    [ApiController]
    [Route("/[controller]")]
    public class MouseController:ControllerBase{
        IDeviceService deviceService;
        public MouseController(IDeviceService deviceService){
            this.deviceService=deviceService;
        }

                [HttpGet("Employees/{empId}/Mouses")]
        [Authorize]
        public async Task<IActionResult> GetMouses(string empId){
            var mouses=await deviceService.GetMousesByEmployeeAsync(empId);
            return Ok(mouses);
        }
        [HttpPost("Employees/{empId}/Mouses")]
        [Authorize(Roles="Admin")]
        public async Task<IActionResult> CreateMouse(string empId,[FromBody] Mouse mouse){
            mouse.empId=empId;
            var newMouse=await deviceService.CreateMouseAsync(mouse);
            return CreatedAtAction(nameof(GetMouses),new{empId=newMouse.empId,mouseId=newMouse.mouseId},newMouse);
        }
        [HttpPut("Employees/{empId}/Mouses/{mouseId}")]
        [Authorize(Roles="Admin")]
        public async Task<IActionResult> UpdateMouse(string empId,string mouseId,[FromBody] Mouse mouse){
            await deviceService.UpdateMouseAsync(mouseId, mouse);
            return NoContent();
        }
        [HttpDelete("Employees/{empId}/Mouses/{mouseId}")]
        [Authorize(Roles="Admin")]
        public async Task<IActionResult> DeleteMouse(string empId, string mouseId){
            await deviceService.DeleteMouseAsync(mouseId);
            return NoContent();
        }
    }
}