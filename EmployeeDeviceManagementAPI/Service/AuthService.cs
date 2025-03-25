using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Model;

namespace Services{
    public class AuthService:IAuthService{
        AssetManagementContext assetManagementContext;
        IConfiguration configuration;
        public AuthService(AssetManagementContext assetManagementContext,IConfiguration configuration){
            this.assetManagementContext=assetManagementContext;
            this.configuration=configuration;
        }
        public async Task<string> LoginAsync(string username,string password){
            var user=await assetManagementContext.Users.FirstOrDefaultAsync(u=>u.userName==username && u.password==password);
            if (user==null){
                throw new Exception("Invalid credentials");
            }
            var claims=new[]{
                new Claim(ClaimTypes.Name,user.userName),
                new Claim(ClaimTypes.Role,user.role),
                new Claim("EmpId",user.empId??"")
            };
            var key=new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]??throw new Exception("Key is null")));
            var creds=new SigningCredentials(key,SecurityAlgorithms.HmacSha256);
            var token=new JwtSecurityToken(
                claims:claims,
                expires:DateTime.Now.AddHours(1),
                signingCredentials:creds);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public async Task RegisterUserAsync(string username,string pass,string Role,string? Id=null){
            if(await assetManagementContext.Users.AnyAsync(u=>u.userName==username)){
                throw new Exception("Username already exists");
            }
            var user=new User{
                userName=username,
                password=pass,
                role=Role,
                empId=Id
            };
            assetManagementContext.Users.Add(user);
            await assetManagementContext.SaveChangesAsync();
        }
        public async Task<List<User>> GetAllUserAsync(){
            return await assetManagementContext.Users.ToListAsync();
        }
        public async Task UpdateUserAsync(string empId,User user){
            var use=await assetManagementContext.Users.FirstOrDefaultAsync(u=>u.empId==empId);
            if(use==null){
                throw new Exception("There is no such user");
            }
            use.userName=user.userName;
            use.password=user.password;
            await assetManagementContext.SaveChangesAsync();
        }
        public async Task DeleteUserAsync(string empId){
            var user=await assetManagementContext.Users.FirstOrDefaultAsync(u=>u.empId==empId);
            if(user==null){
                throw new Exception("There is no such user");
            }
            assetManagementContext.Users.Remove(user);
            await assetManagementContext.SaveChangesAsync();
        }
    }
}