using autoschedular.Model;
using autoschedular.Model.DTOs;
using Microsoft.EntityFrameworkCore;

namespace autoschedular.Services.implementation
{
    public class ModuleService : IModuleService
    {
        private readonly AutoSchedularDbContext _context;

        public ModuleService(AutoSchedularDbContext context)
        {
            _context = context;
        }

        public async Task<ApiResponse<ModuleResponseDTO>> GetModuleAsync(string moduleCode)
        {
            try
            {
                var module = await _context.Modules
                    .Include(m => m.Course)
                    .FirstOrDefaultAsync(m => m.ModuleCode == moduleCode);

                if (module == null)
                {
                    return new ApiResponse<ModuleResponseDTO>
                    {
                        StatusCode = 404,
                        Message = "Module not found",
                        Data = null
                    };
                }

                var moduleResponse = new ModuleResponseDTO
                {
                    ModuleCode = module.ModuleCode,
                    ModuleName = module.ModuleName,
                    ModuleHours = module.ModuleHours,
                    CourseCode = module.CourseCode,
                    CourseName = module.Course?.CourseName
                };

                return new ApiResponse<ModuleResponseDTO>
                {
                    StatusCode = 200,
                    Message = "Module fetched successfully",
                    Data = moduleResponse
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<ModuleResponseDTO>
                {
                    StatusCode = 500,
                    Message = "Query failed",
                    Data = null,
                    Details = new { details = ex.Message }
                };
            }
        }

        public async Task<ApiResponse<object>> CreateModuleAsync(CreateModuleDTO createModuleDto)
        {
            try
            {
                // Check if module code already exists
                var existingModule = await _context.Modules
                    .FirstOrDefaultAsync(m => m.ModuleCode == createModuleDto.ModuleCode);

                if (existingModule != null)
                {
                    return new ApiResponse<object>
                    {
                        StatusCode = 409,
                        Message = "Module with this code already exists",
                        Data = null
                    };
                }

                // Check if course exists
                var course = await _context.Courses
                    .FirstOrDefaultAsync(c => c.CourseCode == createModuleDto.CourseCode);

                if (course == null)
                {
                    return new ApiResponse<object>
                    {
                        StatusCode = 400,
                        Message = "Course not found",
                        Data = null
                    };
                }

                var module = new Module
                {
                    ModuleCode = createModuleDto.ModuleCode,
                    ModuleName = createModuleDto.ModuleName,
                    ModuleHours = createModuleDto.ModuleHours,
                    CourseCode = createModuleDto.CourseCode
                };

                _context.Modules.Add(module);
                await _context.SaveChangesAsync();

                return new ApiResponse<object>
                {
                    StatusCode = 201,
                    Message = "Module added successfully",
                    Data = null
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<object>
                {
                    StatusCode = 500,
                    Message = "Insert failed",
                    Data = null,
                    Details = new { details = ex.Message }
                };
            }
        }

        public async Task<ApiResponse<object>> UpdateModuleAsync(string moduleCode, UpdateModuleDTO updateModuleDto)
        {
            try
            {
                var module = await _context.Modules.FirstOrDefaultAsync(m => m.ModuleCode == moduleCode);

                if (module == null)
                {
                    return new ApiResponse<object>
                    {
                        StatusCode = 404,
                        Message = "Module not found",
                        Data = null
                    };
                }

                bool hasChanges = false;

                if (updateModuleDto.ModuleName != null)
                {
                    module.ModuleName = updateModuleDto.ModuleName;
                    hasChanges = true;
                }

                if (updateModuleDto.ModuleHours.HasValue)
                {
                    module.ModuleHours = updateModuleDto.ModuleHours.Value;
                    hasChanges = true;
                }

                if (updateModuleDto.CourseCode != null)
                {
                    // Check if course exists
                    var course = await _context.Courses
                        .FirstOrDefaultAsync(c => c.CourseCode == updateModuleDto.CourseCode);

                    if (course == null)
                    {
                        return new ApiResponse<object>
                        {
                            StatusCode = 400,
                            Message = "Course not found",
                            Data = null
                        };
                    }

                    module.CourseCode = updateModuleDto.CourseCode;
                    hasChanges = true;
                }

                if (!hasChanges)
                {
                    return new ApiResponse<object>
                    {
                        StatusCode = 400,
                        Message = "No fields to update",
                        Data = null
                    };
                }

                await _context.SaveChangesAsync();

                return new ApiResponse<object>
                {
                    StatusCode = 200,
                    Message = "Module updated successfully",
                    Data = null
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<object>
                {
                    StatusCode = 500,
                    Message = "Update failed",
                    Data = null,
                    Details = new { details = ex.Message }
                };
            }
        }

        public async Task<ApiResponse<object>> DeleteModuleAsync(string moduleCode)
        {
            try
            {
                var module = await _context.Modules.FirstOrDefaultAsync(m => m.ModuleCode == moduleCode);

                if (module == null)
                {
                    return new ApiResponse<object>
                    {
                        StatusCode = 404,
                        Message = "Module not found",
                        Data = null
                    };
                }

                _context.Modules.Remove(module);
                await _context.SaveChangesAsync();

                return new ApiResponse<object>
                {
                    StatusCode = 200,
                    Message = "Module deleted successfully",
                    Data = null
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<object>
                {
                    StatusCode = 500,
                    Message = "Delete failed",
                    Data = null,
                    Details = new { details = ex.Message }
                };
            }
        }

        public async Task<ApiResponse<List<ModuleResponseDTO>>> GetAllModulesAsync()
        {
            try
            {
                var modules = await _context.Modules
                    .Include(m => m.Course)
                    .Select(m => new ModuleResponseDTO
                    {
                        ModuleCode = m.ModuleCode,
                        ModuleName = m.ModuleName,
                        ModuleHours = m.ModuleHours,
                        CourseCode = m.CourseCode,
                        CourseName = m.Course != null ? m.Course.CourseName : null
                    })
                    .ToListAsync();

                return new ApiResponse<List<ModuleResponseDTO>>
                {
                    StatusCode = 200,
                    Message = "Modules fetched successfully",
                    Data = modules
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<List<ModuleResponseDTO>>
                {
                    StatusCode = 500,
                    Message = "Query failed",
                    Data = null,
                    Details = new { details = ex.Message }
                };
            }
        }
    }
}
