using Microsoft.EntityFrameworkCore;
using autoschedular.Model;
using autoschedular.Model.DTOs;

namespace autoschedular.Services.implementation
{
    public class LecturerService : ILecturerService
    {
        private readonly AutoSchedularDbContext _context;

        public LecturerService(AutoSchedularDbContext context)
        {
            _context = context;
        }

        public async Task<LecturerResponseDto?> GetLecturerAsync(string empNo, string password)
        {
            var lecturer = await _context.Lecturers
                .FirstOrDefaultAsync(l => l.EmpNo == empNo);

            if (lecturer == null)
            {
                return null; // Lecturer not found
            }

            // Verify password
            if (!BCrypt.Net.BCrypt.Verify(password, lecturer.Password))
            {
                return null; // Invalid password
            }

            return new LecturerResponseDto
            {
                EmpNo = lecturer.EmpNo,
                FullName = lecturer.FullName,
                Email = lecturer.Email
            };
        }

        public async Task<List<LecturerResponseDto>> GetAllLecturersAsync()
        {
            var lecturers = await _context.Lecturers
                .Select(l => new LecturerResponseDto
                {
                    EmpNo = l.EmpNo,
                    FullName = l.FullName,
                    Email = l.Email
                })
                .ToListAsync();

            return lecturers;
        }

        public async Task<bool> CreateLecturerAsync(CreateLecturerDto createLecturerDto)
        {
            try
            {
                // Check if lecturer already exists
                var existingLecturer = await _context.Lecturers
                    .AnyAsync(l => l.EmpNo == createLecturerDto.EmpNo);

                if (existingLecturer)
                {
                    return false; // Lecturer already exists
                }

                // Hash the password
                var hashedPassword = BCrypt.Net.BCrypt.HashPassword(createLecturerDto.Password);

                var lecturer = new Lecturer
                {
                    EmpNo = createLecturerDto.EmpNo,
                    FullName = createLecturerDto.FullName,
                    Email = createLecturerDto.Email,
                    Password = hashedPassword
                };

                _context.Lecturers.Add(lecturer);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> UpdateLecturerAsync(string empNo, UpdateLecturerDto updateLecturerDto)
        {
            try
            {
                var lecturer = await _context.Lecturers
                    .FirstOrDefaultAsync(l => l.EmpNo == empNo);

                if (lecturer == null)
                {
                    return false; // Lecturer not found
                }

                bool hasChanges = false;

                if (!string.IsNullOrEmpty(updateLecturerDto.FullName))
                {
                    lecturer.FullName = updateLecturerDto.FullName;
                    hasChanges = true;
                }

                if (!string.IsNullOrEmpty(updateLecturerDto.Email))
                {
                    lecturer.Email = updateLecturerDto.Email;
                    hasChanges = true;
                }

                if (!string.IsNullOrEmpty(updateLecturerDto.Password))
                {
                    lecturer.Password = BCrypt.Net.BCrypt.HashPassword(updateLecturerDto.Password);
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

        public async Task<bool> DeleteLecturerAsync(string empNo)
        {
            try
            {
                var lecturer = await _context.Lecturers
                    .FirstOrDefaultAsync(l => l.EmpNo == empNo);

                if (lecturer == null)
                {
                    return false; // Lecturer not found
                }

                _context.Lecturers.Remove(lecturer);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> LecturerExistsAsync(string empNo)
        {
            return await _context.Lecturers.AnyAsync(l => l.EmpNo == empNo);
        }

        public async Task<LecturerWithBatchesDto?> GetLecturerWithBatchesAsync(string empNo)
        {
            var lecturer = await _context.Lecturers
                .Include(l => l.Batches)
                    .ThenInclude(b => b.Course)
                .FirstOrDefaultAsync(l => l.EmpNo == empNo);

            if (lecturer == null)
            {
                return null;
            }

            return new LecturerWithBatchesDto
            {
                EmpNo = lecturer.EmpNo,
                FullName = lecturer.FullName,
                Email = lecturer.Email,
                Batches = lecturer.Batches.Select(b => new BatchInfoDto
                {
                    BatchCode = b.BatchCode,
                    CourseCode = b.CourseCode,
                    CourseName = b.Course?.CourseName
                }).ToList()
            };
        }
    }
}
