using Microsoft.EntityFrameworkCore;
using autoschedular.Model;
using autoschedular.Model.DTOs;

namespace autoschedular.Services.implementation
{
    public class AssignModuleService : IAssignModuleService
    {
        private readonly AutoSchedularDbContext _context;

        public AssignModuleService(AutoSchedularDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<AssignModuleResponseDto>> GetAllAssignModulesAsync()
        {
            var assignModules = await _context.AssignModules
                .Include(am => am.Batch)
                    .ThenInclude(b => b!.Course)
                .Include(am => am.Lecturer)
                .Include(am => am.Module)
                .OrderBy(am => am.Date)
                .ToListAsync();

            return assignModules.Select(am => new AssignModuleResponseDto
            {
                Id = am.Id,
                BatchId = am.BatchId,
                LecturerId = am.LecturerId,
                ModuleId = am.ModuleId,
                Date = am.Date,
                Status = am.Status,
                SessionType = am.SessionType,
                BatchName = am.Batch?.Course?.CourseName,
                LecturerName = am.Lecturer?.FullName,
                ModuleName = am.Module?.ModuleName
            });
        }

        public async Task<AssignModuleResponseDto?> GetAssignModuleByIdAsync(Guid id)
        {
            var assignModule = await _context.AssignModules
                .Include(am => am.Batch)
                    .ThenInclude(b => b!.Course)
                .Include(am => am.Lecturer)
                .Include(am => am.Module)
                .FirstOrDefaultAsync(am => am.Id == id);

            if (assignModule == null)
                return null;

            return new AssignModuleResponseDto
            {
                Id = assignModule.Id,
                BatchId = assignModule.BatchId,
                LecturerId = assignModule.LecturerId,
                ModuleId = assignModule.ModuleId,
                Date = assignModule.Date,
                Status = assignModule.Status,
                SessionType = assignModule.SessionType,
                BatchName = assignModule.Batch?.Course?.CourseName,
                LecturerName = assignModule.Lecturer?.FullName,
                ModuleName = assignModule.Module?.ModuleName
            };
        }

        public async Task<IEnumerable<AssignModuleResponseDto>> GetAssignModulesByBatchIdAsync(string batchId)
        {
            var assignModules = await _context.AssignModules
                .Include(am => am.Batch)
                    .ThenInclude(b => b!.Course)
                .Include(am => am.Lecturer)
                .Include(am => am.Module)
                .Where(am => am.BatchId == batchId)
                .OrderBy(am => am.Date)
                .ToListAsync();

            return assignModules.Select(am => new AssignModuleResponseDto
            {
                Id = am.Id,
                BatchId = am.BatchId,
                LecturerId = am.LecturerId,
                ModuleId = am.ModuleId,
                Date = am.Date,
                Status = am.Status,
                SessionType = am.SessionType,
                BatchName = am.Batch?.Course?.CourseName,
                LecturerName = am.Lecturer?.FullName,
                ModuleName = am.Module?.ModuleName
            });
        }

        public async Task<IEnumerable<AssignModuleResponseDto>> GetAssignModulesByLecturerIdAsync(string lecturerId)
        {
            var assignModules = await _context.AssignModules
                .Include(am => am.Batch)
                    .ThenInclude(b => b!.Course)
                .Include(am => am.Lecturer)
                .Include(am => am.Module)
                .Where(am => am.LecturerId == lecturerId)
                .OrderBy(am => am.Date)
                .ToListAsync();

            return assignModules.Select(am => new AssignModuleResponseDto
            {
                Id = am.Id,
                BatchId = am.BatchId,
                LecturerId = am.LecturerId,
                ModuleId = am.ModuleId,
                Date = am.Date,
                Status = am.Status,
                SessionType = am.SessionType,
                BatchName = am.Batch?.Course?.CourseName,
                LecturerName = am.Lecturer?.FullName,
                ModuleName = am.Module?.ModuleName
            });
        }

        public async Task<IEnumerable<AssignModuleResponseDto>> GetAssignModulesByModuleIdAsync(string moduleId)
        {
            var assignModules = await _context.AssignModules
                .Include(am => am.Batch)
                    .ThenInclude(b => b!.Course)
                .Include(am => am.Lecturer)
                .Include(am => am.Module)
                .Where(am => am.ModuleId == moduleId)
                .OrderBy(am => am.Date)
                .ToListAsync();

            return assignModules.Select(am => new AssignModuleResponseDto
            {
                Id = am.Id,
                BatchId = am.BatchId,
                LecturerId = am.LecturerId,
                ModuleId = am.ModuleId,
                Date = am.Date,
                Status = am.Status,
                SessionType = am.SessionType,
                BatchName = am.Batch?.Course?.CourseName,
                LecturerName = am.Lecturer?.FullName,
                ModuleName = am.Module?.ModuleName
            });
        }

        public async Task<IEnumerable<AssignModuleResponseDto>> GetAssignModulesByDateAsync(DateTime date)
        {
            var dateOnly = date.Date;
            var assignModules = await _context.AssignModules
                .Include(am => am.Batch)
                    .ThenInclude(b => b!.Course)
                .Include(am => am.Lecturer)
                .Include(am => am.Module)
                .Where(am => am.Date.Date == dateOnly)
                .OrderBy(am => am.LecturerId)
                .ToListAsync();

            return assignModules.Select(am => new AssignModuleResponseDto
            {
                Id = am.Id,
                BatchId = am.BatchId,
                LecturerId = am.LecturerId,
                ModuleId = am.ModuleId,
                Date = am.Date,
                Status = am.Status,
                SessionType = am.SessionType,
                BatchName = am.Batch?.Course?.CourseName,
                LecturerName = am.Lecturer?.FullName,
                ModuleName = am.Module?.ModuleName
            });
        }

        public async Task<IEnumerable<AssignModuleResponseDto>> GetAssignModulesByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            var startDateOnly = startDate.Date;
            var endDateOnly = endDate.Date;

            var assignModules = await _context.AssignModules
                .Include(am => am.Batch)
                    .ThenInclude(b => b!.Course)
                .Include(am => am.Lecturer)
                .Include(am => am.Module)
                .Where(am => am.Date.Date >= startDateOnly && am.Date.Date <= endDateOnly)
                .OrderBy(am => am.Date)
                .ThenBy(am => am.LecturerId)
                .ToListAsync();

            return assignModules.Select(am => new AssignModuleResponseDto
            {
                Id = am.Id,
                BatchId = am.BatchId,
                LecturerId = am.LecturerId,
                ModuleId = am.ModuleId,
                Date = am.Date,
                Status = am.Status,
                SessionType = am.SessionType,
                BatchName = am.Batch?.Course?.CourseName,
                LecturerName = am.Lecturer?.FullName,
                ModuleName = am.Module?.ModuleName
            });
        }

        public async Task<IEnumerable<AssignModuleResponseDto>> GetAssignModulesByStatusAsync(string status)
        {
            var assignModules = await _context.AssignModules
                .Include(am => am.Batch)
                    .ThenInclude(b => b!.Course)
                .Include(am => am.Lecturer)
                .Include(am => am.Module)
                .Where(am => am.Status == status)
                .OrderBy(am => am.Date)
                .ToListAsync();

            return assignModules.Select(am => new AssignModuleResponseDto
            {
                Id = am.Id,
                BatchId = am.BatchId,
                LecturerId = am.LecturerId,
                ModuleId = am.ModuleId,
                Date = am.Date,
                Status = am.Status,
                SessionType = am.SessionType,
                BatchName = am.Batch?.Course?.CourseName,
                LecturerName = am.Lecturer?.FullName,
                ModuleName = am.Module?.ModuleName
            });
        }

        public async Task<IEnumerable<AssignModuleResponseDto>> GetAssignModulesBySessionTypeAsync(string sessionType)
        {
            var assignModules = await _context.AssignModules
                .Include(am => am.Batch)
                    .ThenInclude(b => b!.Course)
                .Include(am => am.Lecturer)
                .Include(am => am.Module)
                .Where(am => am.SessionType == sessionType)
                .OrderBy(am => am.Date)
                .ToListAsync();

            return assignModules.Select(am => new AssignModuleResponseDto
            {
                Id = am.Id,
                BatchId = am.BatchId,
                LecturerId = am.LecturerId,
                ModuleId = am.ModuleId,
                Date = am.Date,
                Status = am.Status,
                SessionType = am.SessionType,
                BatchName = am.Batch?.Course?.CourseName,
                LecturerName = am.Lecturer?.FullName,
                ModuleName = am.Module?.ModuleName
            });
        }

        public async Task<AssignModuleWithDetailsDto?> GetAssignModuleWithDetailsAsync(Guid id)
        {
            var assignModule = await _context.AssignModules
                .Include(am => am.Batch)
                    .ThenInclude(b => b!.Course)
                .Include(am => am.Lecturer)
                .Include(am => am.Module)
                .FirstOrDefaultAsync(am => am.Id == id);

            if (assignModule == null)
                return null;

            return new AssignModuleWithDetailsDto
            {
                Id = assignModule.Id,
                BatchId = assignModule.BatchId,
                LecturerId = assignModule.LecturerId,
                ModuleId = assignModule.ModuleId,
                Date = assignModule.Date,
                Status = assignModule.Status,
                SessionType = assignModule.SessionType,
                Batch = assignModule.Batch != null ? new BatchInfoDto
                {
                    BatchCode = assignModule.Batch.BatchCode,
                    CourseCode = assignModule.Batch.CourseCode,
                    CourseName = assignModule.Batch.Course?.CourseName
                } : null,
                Lecturer = assignModule.Lecturer != null ? new LecturerResponseDto
                {
                    EmpNo = assignModule.Lecturer.EmpNo,
                    FullName = assignModule.Lecturer.FullName,
                    Email = assignModule.Lecturer.Email
                } : null,
                Module = assignModule.Module != null ? new ModuleInfoDto
                {
                    ModuleCode = assignModule.Module.ModuleCode,
                    ModuleName = assignModule.Module.ModuleName,
                    ModuleHours = assignModule.Module.ModuleHours,
                    CourseCode = assignModule.Module.CourseCode
                } : null
            };
        }

        public async Task<Guid?> CreateAssignModuleAsync(CreateAssignModuleDto createDto)
        {
            try
            {
                // Check if batch, lecturer, and module exist
                var batchExists = await _context.Batches.AnyAsync(b => b.BatchCode == createDto.BatchId);
                var lecturerExists = await _context.Lecturers.AnyAsync(l => l.EmpNo == createDto.LecturerId);
                var moduleExists = await _context.Modules.AnyAsync(m => m.ModuleCode == createDto.ModuleId);

                if (!batchExists || !lecturerExists || !moduleExists)
                {
                    return null; // One or more references not found
                }

                var assignModule = new AssignModule
                {
                    BatchId = createDto.BatchId,
                    LecturerId = createDto.LecturerId,
                    ModuleId = createDto.ModuleId,
                    Date = createDto.Date,
                    Status = createDto.Status,
                    SessionType = createDto.SessionType
                };

                _context.AssignModules.Add(assignModule);
                await _context.SaveChangesAsync();
                return assignModule.Id;
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> UpdateAssignModuleAsync(Guid id, UpdateAssignModuleDto updateDto)
        {
            try
            {
                var assignModule = await _context.AssignModules
                    .FirstOrDefaultAsync(am => am.Id == id);

                if (assignModule == null)
                {
                    return false; // Assignment not found
                }

                bool hasChanges = false;

                if (updateDto.Date.HasValue)
                {
                    assignModule.Date = updateDto.Date.Value;
                    hasChanges = true;
                }

                if (!string.IsNullOrEmpty(updateDto.Status))
                {
                    assignModule.Status = updateDto.Status;
                    hasChanges = true;
                }

                if (!string.IsNullOrEmpty(updateDto.SessionType))
                {
                    assignModule.SessionType = updateDto.SessionType;
                    hasChanges = true;
                }

                if (!hasChanges)
                {
                    return false; // No changes to make
                }

                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteAssignModuleAsync(Guid id)
        {
            try
            {
                var assignModule = await _context.AssignModules
                    .FirstOrDefaultAsync(am => am.Id == id);

                if (assignModule == null)
                {
                    return false; // Assignment not found
                }

                _context.AssignModules.Remove(assignModule);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> AssignModuleExistsAsync(Guid id)
        {
            return await _context.AssignModules.AnyAsync(am => am.Id == id);
        }
    }
}
