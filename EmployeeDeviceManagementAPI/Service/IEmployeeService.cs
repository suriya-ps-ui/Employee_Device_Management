using Model;

namespace Services{
    public interface IEmployeeService{
        Task<List<Employee>> GetAllEmployeesAsync();
        Task<List<Employee>> PastEmployeesAsync();
        Task<Employee> GetEmployeeByIDAsync(string id);
        Task<Employee> CreateEmployeeAsync(Employee employee);
        Task UpdateEmployeeAsync(string id,Employee employee);
        Task DeleteEmployeeAsync(string id);

    }
}