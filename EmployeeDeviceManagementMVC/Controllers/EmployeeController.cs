using Microsoft.AspNetCore.Mvc;
using Services;
using System.IdentityModel.Tokens.Jwt;

namespace Controllers{
    public class EmployeeController : Controller{
        IEmployeeApiServices employeeApiServices;

        public EmployeeController(IEmployeeApiServices employeeServices){
            this.employeeApiServices = employeeServices;
        }

        public async Task<IActionResult> Devices(string empId){
            var token = HttpContext.Session.GetString("JWToken");
            if (string.IsNullOrEmpty(token)){
                Console.WriteLine("No token found in session.");
                return RedirectToAction("Login", "Account");
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadJwtToken(token);
            empId ??= jwtToken.Claims.FirstOrDefault(c => c.Type == "EmpId")?.Value;

            if (string.IsNullOrEmpty(empId)){
                Console.WriteLine("No empId found in token or parameters.");
                return RedirectToAction("Login", "Account");
            }
            var employee = await employeeApiServices.GetEmployeeByIdAsync(empId, token);
            if (employee == null){
                Console.WriteLine($"Employee not found for empId: {empId}");
                return RedirectToAction("Login", "Account");
            }
            return View(employee);
        }
    }
}