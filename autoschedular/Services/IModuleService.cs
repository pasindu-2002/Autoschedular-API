using autoschedular.Model.DTOs;

namespace autoschedular.Services
{
    public interface IModuleService
    {
        Task<ApiResponse<ModuleResponseDTO>> GetModuleAsync(string moduleCode);
        Task<ApiResponse<object>> CreateModuleAsync(CreateModuleDTO createModuleDto);
        Task<ApiResponse<object>> UpdateModuleAsync(string moduleCode, UpdateModuleDTO updateModuleDto);
        Task<ApiResponse<object>> DeleteModuleAsync(string moduleCode);
        Task<ApiResponse<List<ModuleResponseDTO>>> GetAllModulesAsync();
    }
}
