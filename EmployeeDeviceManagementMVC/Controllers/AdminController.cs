using Microsoft.AspNetCore.Mvc;
using Models;
using Services;
namespace Controllers{
    public class AdminController : Controller{
        IAuthApiServices authApiServices;
        IEmployeeApiServices employeeApiServices;
        IDeviceApiServices deviceApiServices;
        public AdminController(IAuthApiServices authApiServices,IEmployeeApiServices employeeApiServices,IDeviceApiServices deviceApiServices){
            this.authApiServices=authApiServices;
            this.employeeApiServices=employeeApiServices;
            this.deviceApiServices=deviceApiServices;
        }       
        public IActionResult Index(){
            return View();
        }
        public async Task<IActionResult> ManageEmployees(){
            var token=HttpContext.Session.GetString("JWToken");
            if (string.IsNullOrEmpty(token)) return RedirectToAction("Login","Account");
            var employees=await employeeApiServices.GetAllEmployeesAsync(token);
            return View(employees);
        }
        public async Task<IActionResult> PastEmployees(){
            var token=HttpContext.Session.GetString("JWToken");
            if (string.IsNullOrEmpty(token)) return RedirectToAction("Login","Account");
            var employees=await employeeApiServices.GetPastEmployeesAsync(token);
            return View(employees);
        }
        public IActionResult CreateEmployee(){
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateEmployee(EmployeeRequest employee){
            var token = HttpContext.Session.GetString("JWToken");
            if (string.IsNullOrEmpty(token)) return RedirectToAction("Login", "Account");
            try{await employeeApiServices.CreateEmployeeAsync(employee, token);}
            catch(Exception){ViewBag.Error="EmpID Already in use"; return View();}
            return RedirectToAction("ManageEmployees");
        }
        public async Task<IActionResult> EditEmployee(string empId){
            var token = HttpContext.Session.GetString("JWToken");
            if (string.IsNullOrEmpty(token)) return RedirectToAction("Login","Account");
            var employee=await employeeApiServices.GetEmployeeByIdAsync(empId,token);
            var employeeRequest = new EmployeeRequest{
                empId=employee.empId,
                empName=employee.empName,
                department=employee.department,
                isAvilable=employee.isAvilable
            };
            return View(employeeRequest);
        }
        [HttpPost]
        public async Task<IActionResult> EditEmployee(string empId,[FromForm] EmployeeRequest employee){
            var token=HttpContext.Session.GetString("JWToken");
            if (string.IsNullOrEmpty(token)) return RedirectToAction("Login","Account");
            await employeeApiServices.UpdateEmployeeAsync(empId,employee,token);
            return RedirectToAction("ManageEmployees");
        }
        public async Task<IActionResult> DeleteEmployee(string empId){
            var token=HttpContext.Session.GetString("JWToken");
            if (string.IsNullOrEmpty(token)) return RedirectToAction("Login","Account");
            await employeeApiServices.DeleteEmployeeAsync(empId,token);
            return RedirectToAction("ManageEmployees");
        }
        public async Task<IActionResult> ManageDevices(string empId){
            var token=HttpContext.Session.GetString("JWToken");
            if (string.IsNullOrEmpty(token)) return RedirectToAction("Login", "Account");
            var employee=await employeeApiServices.GetEmployeeByIdAsync(empId, token);
            return View(employee);
        }
        public IActionResult CreateDevice(string empId,string deviceType){
            ViewBag.EmpId=empId;
            ViewBag.DeviceType=deviceType;
            if (TempData["Error"] != null){ViewBag.Error = TempData["Error"];}
            return View();
        }
        [HttpPost]
        [ActionName("CreateDevice")]
        public async Task<IActionResult> CreateDevicePost(string empId,string deviceType){
            var token=HttpContext.Session.GetString("JWToken");
            if (string.IsNullOrEmpty(token)) return RedirectToAction("Login", "Account");
            if (deviceType=="Laptop"){
                var laptop=new Laptop{
                    empId=empId,
                    lapHostName=Request.Form["lapHostName"],
                    lapModel=Request.Form["lapModel"],
                    processor=Request.Form["processor"],
                    storage=Request.Form["storage"],
                    ram=Request.Form["ram"],
                    assignedOn=DateOnly.Parse(Request.Form["assignedOn"]),
                    status=Request.Form["status"]
                };
                try{await deviceApiServices.CreateLaptopAsync(laptop, token);}
                catch(Exception){TempData["Error"]="Laptop Host name Already in use";return RedirectToAction("CreateDevice",new{ deviceType,empId, });}
            }
            else if (deviceType=="Keyboard"){
                var keyboard=new Keyboard{
                    empId=empId,
                    keyId=Request.Form["keyId"],
                    keyS_No=int.Parse(Request.Form["keyS_No"]),
                    keyBrand=Request.Form["keyBrand"],
                    status=Request.Form["status"]
                };
                try{await deviceApiServices.CreateKeyboardAsync(keyboard, token);}
                catch(Exception){TempData["Error"]="Keyboard ID Already in use";return RedirectToAction("CreateDevice",new{ deviceType,empId, });}
                }
            else if (deviceType=="Mouse"){
                var mouse=new Mouse{
                    empId=empId,
                    mouseId=Request.Form["mouseId"],
                    mouseS_No=int.Parse(Request.Form["mouseS_No"]),
                    mouseBrand=Request.Form["mouseBrand"],
                    status=Request.Form["status"]
                };
                try{await deviceApiServices.CreateMouseAsync(mouse, token);}
                catch(Exception){TempData["Error"]="Mouse ID Already in use";return RedirectToAction("CreateDevice",new{ deviceType,empId, });}
            }
            return RedirectToAction("ManageDevices",new { empId });
        }
        public async Task<IActionResult> EditDevice(string empId,string deviceType,string id){
            var token=HttpContext.Session.GetString("JWToken");
            if (string.IsNullOrEmpty(token)) return RedirectToAction("Login", "Account");
            ViewBag.EmpId=empId;
            if (deviceType=="Laptop"){
                var laptops=await deviceApiServices.GetLaptopsAsync(empId, token);
                var laptop=laptops.FirstOrDefault(l => l.lapHostName == id);
                return View("EditLaptop", laptop);
            }
            else if (deviceType == "Keyboard"){
                var keyboards = await deviceApiServices.GetKeyboardsAsync(empId, token);
                var keyboard = keyboards.FirstOrDefault(k => k.keyId == id);
                return View("EditKeyboard", keyboard);
            }
            else if (deviceType == "Mouse"){
                var mouses = await deviceApiServices.GetMousesAsync(empId, token);
                var mouse = mouses.FirstOrDefault(m => m.mouseId == id);
                return View("EditMouse", mouse);
            }
            return RedirectToAction("ManageDevices", new { empId });
        }
        [HttpPost]
        [ActionName("EditDevice")]
        public async Task<IActionResult> EditDevicePost(string empId, string deviceType, string id){
            var token = HttpContext.Session.GetString("JWToken");
            if (string.IsNullOrEmpty(token)) return RedirectToAction("Login", "Account");
            if (deviceType == "Laptop"){
                var laptop = new Laptop{
                    empId = empId,
                    lapHostName = id,
                    lapModel = Request.Form["lapModel"],
                    processor = Request.Form["processor"],
                    storage = Request.Form["storage"],
                    ram = Request.Form["ram"],
                    assignedOn = DateOnly.Parse(Request.Form["assignedOn"]),
                    status = Request.Form["status"]
                };
                await deviceApiServices.UpdateLaptopAsync(empId, id, laptop, token);
            }
            else if (deviceType == "Keyboard"){
                var keyboard = new Keyboard{
                    empId = empId,
                    keyId = id,
                    keyS_No = int.Parse(Request.Form["keyS_No"]),
                    keyBrand = Request.Form["keyBrand"],
                    status = Request.Form["status"]
                };
                await deviceApiServices.UpdateKeyboardAsync(id, keyboard, token);
            }
            else if (deviceType == "Mouse"){
                var mouse = new Mouse{
                    empId = empId,
                    mouseId = id,
                    mouseS_No = int.Parse(Request.Form["mouseS_No"]),
                    mouseBrand = Request.Form["mouseBrand"],
                    status = Request.Form["status"]
                };
                await deviceApiServices.UpdateMouseAsync(id, mouse, token);
            }
            return RedirectToAction("ManageDevices", new { empId });
        }
        public async Task<IActionResult> DeleteDevice(string empId, string deviceType, string id){
            var token = HttpContext.Session.GetString("JWToken");
            if (string.IsNullOrEmpty(token)) return RedirectToAction("Login", "Account");
            if (deviceType == "Laptop")
                await deviceApiServices.DeleteLaptopAsync(empId, id, token);
            else if (deviceType == "Keyboard")
                await deviceApiServices.DeleteKeyboardAsync(empId, id, token);
            else if (deviceType == "Mouse")
                await deviceApiServices.DeleteMouseAsync(empId, id, token);
            return RedirectToAction("ManageDevices", new { empId });
        }
        public async Task<IActionResult> ManageUsers(){
            var token = HttpContext.Session.GetString("JWToken");
            if (string.IsNullOrEmpty(token)) return RedirectToAction("Login", "Account");
            var users = await authApiServices.GetAllUsersAsync(token);
            return View(users);
        }
        public IActionResult CreateUser(){
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateUser(RegisterRequest user){
            var token = HttpContext.Session.GetString("JWToken");
            if (string.IsNullOrEmpty(token)) return RedirectToAction("Login", "Account");
            try{await authApiServices.RegisterUserAsync(user, token);}
            catch{ViewBag.Error="Username already in use or Invalid E-ID.";return View();}
            return RedirectToAction("ManageUsers");
        }
        public async Task<IActionResult> EditUser(string empId){
            var token = HttpContext.Session.GetString("JWToken");
            if (string.IsNullOrEmpty(token)) return RedirectToAction("Login", "Account");
            var users = await authApiServices.GetAllUsersAsync(token);
            var user = users.FirstOrDefault(u => u.empId == empId);
            var registerRequest = new RegisterRequest{
                username = user.userName,
                Password = user.password,
                role = user.role,
                empId = user.empId
            };
            return View(registerRequest);
        }
        [HttpPost]
        public async Task<IActionResult> EditUser(string empId, RegisterRequest user){
            var token = HttpContext.Session.GetString("JWToken");
            if (string.IsNullOrEmpty(token)) return RedirectToAction("Login", "Account");
            await authApiServices.UpdateUserAsync(empId, user, token);
            return RedirectToAction("ManageUsers");
        }
        public async Task<IActionResult> DeleteUser(string empId){
            var token = HttpContext.Session.GetString("JWToken");
            if (string.IsNullOrEmpty(token)) return RedirectToAction("Login", "Account");
            try{await authApiServices.DeleteUserAsync(empId, token);}
            catch{ViewBag.Error="Admin Can't be removed.";}
            return RedirectToAction("ManageUsers");
        }
    }
}