using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services;
using Model;

namespace Controllers{
    [ApiController]
    [Route("/[controller]")]
    public class LaptopController:ControllerBase{
        IDeviceService deviceService;
        public LaptopController(IDeviceService deviceService){
            this.deviceService=deviceService;
        }

        [HttpGet("Employees/{empId}/Laptops")]
        [Authorize]
        public async Task<IActionResult> GetLaptops(string empId){
            var laptops=await deviceService.GetLaptopsByEmployeeAsync(empId);
            return Ok(laptops);
        }
        [HttpPost("Employees/{empId}/Laptops")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateLaptop(string empId,[FromBody] Laptop laptop){
            var newLaptop=await deviceService.CreateLaptopAsync(laptop);
            return CreatedAtAction(nameof(GetLaptops),new{empId=newLaptop.empId,lapHostName=newLaptop.lapHostName},newLaptop);
        }
        [HttpPut("Employees/{empId}/Laptops/{lapHostName}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateLaptop(string empId,string lapHostName,[FromBody] Laptop laptop){
            await deviceService.UpdateLaptopAsync(empId,lapHostName,laptop);
            return NoContent();
        }
        [HttpDelete("Employees/{empId}/Laptops/{lapHostName}")]
        [Authorize(Roles="Admin")]
        public async Task<IActionResult> DeleteLaptop(string empId, string lapHostName){
            await deviceService.DeleteLaptopAsync(empId, lapHostName);
            return NoContent();
        }
    }
}