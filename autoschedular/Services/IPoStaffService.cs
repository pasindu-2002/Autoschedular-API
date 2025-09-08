using autoschedular.Model.DTOs;

namespace autoschedular.Services
{
    public interface IPoStaffService
    {
        Task<PoStaffResponseDto?> GetEmployeeAsync(string empNo, string password);
        Task<List<PoStaffResponseDto>> GetAllEmployeesAsync();
        Task<bool> CreateEmployeeAsync(CreatePoStaffDto createPoStaffDto);
        Task<bool> UpdateEmployeeAsync(string empNo, UpdatePoStaffDto updatePoStaffDto);
        Task<bool> DeleteEmployeeAsync(string empNo);
        Task<bool> EmployeeExistsAsync(string empNo);
    }
}
