using Microsoft.EntityFrameworkCore;
using autoschedular.Model;
using autoschedular.Model.DTOs;

namespace autoschedular.Services.implementation
{
    public class CourseServices : ICourseServices
    {
        private readonly AutoSchedularDbContext _context;

        public CourseServices(AutoSchedularDbContext context)
        {
            _context = context;
        }

        public async Task<CourseResponseDto?> GetCourseAsync(string courseCode)
        {
            var course = await _context.Courses
                .Where(c => c.CourseCode == courseCode)
                .Select(c => new CourseResponseDto
                {
                    CourseCode = c.CourseCode,
                    CourseName = c.CourseName,
                    School = c.School
                })
                .FirstOrDefaultAsync();

            return course;
        }

        public async Task<List<CourseResponseDto>> GetAllCoursesAsync()
        {
            var courses = await _context.Courses
                .Select(c => new CourseResponseDto
                {
                    CourseCode = c.CourseCode,
                    CourseName = c.CourseName,
                    School = c.School
                })
                .ToListAsync();

            return courses;
        }

        public async Task<bool> CreateCourseAsync(CreateCourseDto createCourseDto)
        {
            try
            {
                // Check if course already exists
                var existingCourse = await _context.Courses
                    .AnyAsync(c => c.CourseCode == createCourseDto.CourseCode);

                if (existingCourse)
                {
                    return false; // Course already exists
                }

                var course = new Course
                {
                    CourseCode = createCourseDto.CourseCode,
                    CourseName = createCourseDto.CourseName,
                    School = createCourseDto.School
                };

                _context.Courses.Add(course);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> UpdateCourseAsync(string courseCode, UpdateCourseDto updateCourseDto)
        {
            try
            {
                var course = await _context.Courses
                    .FirstOrDefaultAsync(c => c.CourseCode == courseCode);

                if (course == null)
                {
                    return false; // Course not found
                }

                bool hasChanges = false;

                if (!string.IsNullOrEmpty(updateCourseDto.CourseName))
                {
                    course.CourseName = updateCourseDto.CourseName;
                    hasChanges = true;
                }

                if (!string.IsNullOrEmpty(updateCourseDto.School))
                {
                    course.School = updateCourseDto.School;
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

        public async Task<bool> DeleteCourseAsync(string courseCode)
        {
            try
            {
                var course = await _context.Courses
                    .FirstOrDefaultAsync(c => c.CourseCode == courseCode);

                if (course == null)
                {
                    return false; // Course not found
                }

                _context.Courses.Remove(course);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> CourseExistsAsync(string courseCode)
        {
            return await _context.Courses.AnyAsync(c => c.CourseCode == courseCode);
        }
    }
}
