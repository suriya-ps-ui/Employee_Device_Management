using Model;
namespace Services{
    public interface IDeviceService{
        Task<List<Laptop>> GetLaptopsByEmployeeAsync(string empId);
        Task<Laptop> CreateLaptopAsync(Laptop laptop);
        Task UpdateLaptopAsync(string empId, string lapHostName, Laptop laptop);
        Task DeleteLaptopAsync(string empId, string lapHostName);
        Task<List<Keyboard>> GetKeyboardsByEmployeeAsync(string empId);
        Task<Keyboard> CreateKeyboardAsync(Keyboard keyboard);
        Task UpdateKeyboardAsync(string keyId, Keyboard keyboard);
        Task DeleteKeyboardAsync(string keyId);
        Task<List<Mouse>> GetMousesByEmployeeAsync(string empId);
        Task<Mouse> CreateMouseAsync(Mouse mouse);
        Task UpdateMouseAsync(string mouseId, Mouse mouse);
        Task DeleteMouseAsync(string mouseId);
    }
}