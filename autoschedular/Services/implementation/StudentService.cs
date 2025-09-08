using Microsoft.EntityFrameworkCore;
using autoschedular.Model;
using autoschedular.Model.DTOs;

namespace autoschedular.Services.implementation
{
    public class StudentService : IStudentService
    {
        private readonly AutoSchedularDbContext _context;

        public StudentService(AutoSchedularDbContext context)
        {
            _context = context;
        }

        public async Task<StudentResponseDto?> GetStudentAsync(string stuId, string password)
        {
            var student = await _context.Students
                .FirstOrDefaultAsync(s => s.StuId == stuId);

            if (student == null)
            {
                return null; // Student not found
            }

            // Verify password
            if (!BCrypt.Net.BCrypt.Verify(password, student.Password))
            {
                return null; // Invalid password
            }

            return new StudentResponseDto
            {
                StuId = student.StuId,
                FullName = student.FullName,
                Email = student.Email,
                BatchCode = student.BatchCode
            };
        }

        public async Task<bool> CreateStudentAsync(CreateStudentDto createStudentDto)
        {
            try
            {
                // Check if student already exists
                var existingStudent = await _context.Students
                    .AnyAsync(s => s.StuId == createStudentDto.StuId);

                if (existingStudent)
                {
                    return false; // Student already exists
                }

                // Check if batch exists
                var batchExists = await BatchExistsAsync(createStudentDto.BatchCode);
                if (!batchExists)
                {
                    return false; // Batch does not exist
                }

                // Hash the password
                var hashedPassword = BCrypt.Net.BCrypt.HashPassword(createStudentDto.Password);

                var student = new Student
                {
                    StuId = createStudentDto.StuId,
                    FullName = createStudentDto.FullName,
                    Email = createStudentDto.Email,
                    Password = hashedPassword,
                    BatchCode = createStudentDto.BatchCode
                };

                _context.Students.Add(student);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> UpdateStudentAsync(string stuId, UpdateStudentDto updateStudentDto)
        {
            try
            {
                var student = await _context.Students
                    .FirstOrDefaultAsync(s => s.StuId == stuId);

                if (student == null)
                {
                    return false; // Student not found
                }

                bool hasChanges = false;

                if (!string.IsNullOrEmpty(updateStudentDto.FullName))
                {
                    student.FullName = updateStudentDto.FullName;
                    hasChanges = true;
                }

                if (!string.IsNullOrEmpty(updateStudentDto.Email))
                {
                    student.Email = updateStudentDto.Email;
                    hasChanges = true;
                }

                if (!string.IsNullOrEmpty(updateStudentDto.Password))
                {
                    student.Password = BCrypt.Net.BCrypt.HashPassword(updateStudentDto.Password);
                    hasChanges = true;
                }

                if (!string.IsNullOrEmpty(updateStudentDto.BatchCode))
                {
                    // Check if new batch exists
                    var batchExists = await BatchExistsAsync(updateStudentDto.BatchCode);
                    if (!batchExists)
                    {
                        return false; // Batch does not exist
                    }
                    
                    student.BatchCode = updateStudentDto.BatchCode;
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

        public async Task<bool> DeleteStudentAsync(string stuId)
        {
            try
            {
                var student = await _context.Students
                    .FirstOrDefaultAsync(s => s.StuId == stuId);

                if (student == null)
                {
                    return false; // Student not found
                }

                _context.Students.Remove(student);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> StudentExistsAsync(string stuId)
        {
            return await _context.Students.AnyAsync(s => s.StuId == stuId);
        }

        public async Task<List<StudentResponseDto>> GetStudentsByBatchAsync(string batchCode)
        {
            var students = await _context.Students
                .Where(s => s.BatchCode == batchCode)
                .Select(s => new StudentResponseDto
                {
                    StuId = s.StuId,
                    FullName = s.FullName,
                    Email = s.Email,
                    BatchCode = s.BatchCode
                })
                .ToListAsync();

            return students;
        }

        public async Task<bool> BatchExistsAsync(string batchCode)
        {
            return await _context.Batches.AnyAsync(b => b.BatchCode == batchCode);
        }
    }
}
