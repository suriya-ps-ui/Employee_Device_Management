using Models;
namespace Services{
    public interface IEmployeeApiServices{
        Task<List<Employee>> GetAllEmployeesAsync(string token);
        Task<List<Employee>> GetPastEmployeesAsync(string token);
        Task<Employee?> GetEmployeeByIdAsync(string empId,string token);
        Task<EmployeeRequest?> CreateEmployeeAsync(EmployeeRequest employee, string token);
        Task UpdateEmployeeAsync(string empId,EmployeeRequest employee,string token);
        Task DeleteEmployeeAsync(string empId, string token);
    }
}