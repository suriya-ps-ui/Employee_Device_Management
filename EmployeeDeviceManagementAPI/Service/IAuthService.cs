using Model;

namespace Services{
    public interface IAuthService{
        Task<string> LoginAsync(string username,string password);
        Task RegisterUserAsync(string username,string pass,string Role,string? Id=null);
        Task<List<User>> GetAllUserAsync();
        Task UpdateUserAsync(string empId,User user);
        Task DeleteUserAsync(string empId);
    }
}