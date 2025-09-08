using Microsoft.EntityFrameworkCore;
using autoschedular.Model;
using autoschedular.Model.DTOs;

namespace autoschedular.Services.implementation
{
    public class LecturerTimetableService : ILecturerTimetableService
    {
        private readonly AutoSchedularDbContext _context;

        public LecturerTimetableService(AutoSchedularDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<LecturerTimetableResponseDto>> GetAllLecturerTimetablesAsync()
        {
            var timetables = await _context.LecturerTimetables
                .Include(lt => lt.Lecturer)
                .OrderBy(lt => lt.Date)
                .ToListAsync();

            return timetables.Select(lt => new LecturerTimetableResponseDto
            {
                Id = lt.Id,
                LecturerId = lt.LecturerId,
                Description = lt.Description,
                Date = lt.Date,
                LecturerName = lt.Lecturer?.FullName
            });
        }

        public async Task<LecturerTimetableResponseDto?> GetLecturerTimetableByIdAsync(int id)
        {
            var timetable = await _context.LecturerTimetables
                .Include(lt => lt.Lecturer)
                .FirstOrDefaultAsync(lt => lt.Id == id);

            if (timetable == null)
                return null;

            return new LecturerTimetableResponseDto
            {
                Id = timetable.Id,
                LecturerId = timetable.LecturerId,
                Description = timetable.Description,
                Date = timetable.Date,
                LecturerName = timetable.Lecturer?.FullName
            };
        }

        public async Task<IEnumerable<LecturerTimetableResponseDto>> GetLecturerTimetablesByLecturerIdAsync(string lecturerId)
        {
            var timetables = await _context.LecturerTimetables
                .Include(lt => lt.Lecturer)
                .Where(lt => lt.LecturerId == lecturerId)
                .OrderBy(lt => lt.Date)
                .ToListAsync();

            return timetables.Select(lt => new LecturerTimetableResponseDto
            {
                Id = lt.Id,
                LecturerId = lt.LecturerId,
                Description = lt.Description,
                Date = lt.Date,
                LecturerName = lt.Lecturer?.FullName
            });
        }

        public async Task<IEnumerable<LecturerTimetableResponseDto>> GetLecturerTimetablesByDateAsync(DateTime date)
        {
            var dateOnly = date.Date;
            var timetables = await _context.LecturerTimetables
                .Include(lt => lt.Lecturer)
                .Where(lt => lt.Date.Date == dateOnly)
                .OrderBy(lt => lt.LecturerId)
                .ToListAsync();

            return timetables.Select(lt => new LecturerTimetableResponseDto
            {
                Id = lt.Id,
                LecturerId = lt.LecturerId,
                Description = lt.Description,
                Date = lt.Date,
                LecturerName = lt.Lecturer?.FullName
            });
        }

        public async Task<IEnumerable<LecturerTimetableResponseDto>> GetLecturerTimetablesByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            var startDateOnly = startDate.Date;
            var endDateOnly = endDate.Date;

            var timetables = await _context.LecturerTimetables
                .Include(lt => lt.Lecturer)
                .Where(lt => lt.Date.Date >= startDateOnly && lt.Date.Date <= endDateOnly)
                .OrderBy(lt => lt.Date)
                .ThenBy(lt => lt.LecturerId)
                .ToListAsync();

            return timetables.Select(lt => new LecturerTimetableResponseDto
            {
                Id = lt.Id,
                LecturerId = lt.LecturerId,
                Description = lt.Description,
                Date = lt.Date,
                LecturerName = lt.Lecturer?.FullName
            });
        }

        public async Task<bool> CreateLecturerTimetableAsync(CreateLecturerTimetableDto createDto)
        {
            try
            {
                // Check if lecturer exists
                var lecturerExists = await _context.Lecturers
                    .AnyAsync(l => l.EmpNo == createDto.LecturerId);

                if (!lecturerExists)
                {
                    return false; // Lecturer not found
                }

                var timetable = new LecturerTimetable
                {
                    LecturerId = createDto.LecturerId,
                    Description = createDto.Description,
                    Date = createDto.Date
                };

                _context.LecturerTimetables.Add(timetable);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> UpdateLecturerTimetableAsync(int id, UpdateLecturerTimetableDto updateDto)
        {
            try
            {
                var timetable = await _context.LecturerTimetables
                    .FirstOrDefaultAsync(lt => lt.Id == id);

                if (timetable == null)
                {
                    return false; // Timetable not found
                }

                bool hasChanges = false;

                if (!string.IsNullOrEmpty(updateDto.Description))
                {
                    timetable.Description = updateDto.Description;
                    hasChanges = true;
                }

                if (updateDto.Date.HasValue)
                {
                    timetable.Date = updateDto.Date.Value;
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

        public async Task<bool> DeleteLecturerTimetableAsync(int id)
        {
            try
            {
                var timetable = await _context.LecturerTimetables
                    .FirstOrDefaultAsync(lt => lt.Id == id);

                if (timetable == null)
                {
                    return false; // Timetable not found
                }

                _context.LecturerTimetables.Remove(timetable);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> LecturerTimetableExistsAsync(int id)
        {
            return await _context.LecturerTimetables.AnyAsync(lt => lt.Id == id);
        }
    }
}
