using autoschedular.Model.DTOs;

namespace autoschedular.Services
{
    public interface IAssignModuleService
    {
        Task<IEnumerable<AssignModuleResponseDto>> GetAllAssignModulesAsync();
        Task<AssignModuleResponseDto?> GetAssignModuleByIdAsync(Guid id);
        Task<IEnumerable<AssignModuleResponseDto>> GetAssignModulesByBatchIdAsync(string batchId);
        Task<IEnumerable<AssignModuleResponseDto>> GetAssignModulesByLecturerIdAsync(string lecturerId);
        Task<IEnumerable<AssignModuleResponseDto>> GetAssignModulesByModuleIdAsync(string moduleId);
        Task<IEnumerable<AssignModuleResponseDto>> GetAssignModulesByDateAsync(DateTime date);
        Task<IEnumerable<AssignModuleResponseDto>> GetAssignModulesByDateRangeAsync(DateTime startDate, DateTime endDate);
        Task<IEnumerable<AssignModuleResponseDto>> GetAssignModulesByStatusAsync(string status);
        Task<IEnumerable<AssignModuleResponseDto>> GetAssignModulesBySessionTypeAsync(string sessionType);
        Task<AssignModuleWithDetailsDto?> GetAssignModuleWithDetailsAsync(Guid id);
        Task<Guid?> CreateAssignModuleAsync(CreateAssignModuleDto createDto);
        Task<bool> UpdateAssignModuleAsync(Guid id, UpdateAssignModuleDto updateDto);
        Task<bool> DeleteAssignModuleAsync(Guid id);
        Task<bool> AssignModuleExistsAsync(Guid id);
    }
}
