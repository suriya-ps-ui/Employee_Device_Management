using Microsoft.AspNetCore.Mvc;
using Models;
using Services;
using System.IdentityModel.Tokens.Jwt;

namespace Controllers{
    public class AccountController:Controller{
        IAuthApiServices authApiServices;
        public AccountController(IAuthApiServices authApiServices){
            this.authApiServices = authApiServices;
        }
        [HttpGet]
        public IActionResult Login(){
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginRequest model){
            if(!ModelState.IsValid){
                return View(model);
            }
            try{
                var token=await authApiServices.LoginAsync(model);
                HttpContext.Session.SetString("JWToken",token);
                var tokenHandler=new JwtSecurityTokenHandler();
                var jwtToken=tokenHandler.ReadJwtToken(token);
                var role=jwtToken.Claims.SingleOrDefault(c=>c.Type=="http://schemas.microsoft.com/ws/2008/06/identity/claims/role")?.Value;
                if (role=="Admin"){
                    return RedirectToAction("Index","Admin");
                }
                return RedirectToAction("Devices","Employee");
            }
            catch (Exception ex){
                ModelState.AddModelError("",$"Login failed:{ex.Message}");
                return View(model);
            }
        }
        [HttpGet]
        public IActionResult Logout(){
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}