using autoschedular.Model;
using autoschedular.Model.DTOs;

namespace autoschedular.Services
{
    public interface ICourseServices
    {
        Task<CourseResponseDto?> GetCourseAsync(string courseCode);
        Task<bool> CreateCourseAsync(CreateCourseDto createCourseDto);
        Task<bool> UpdateCourseAsync(string courseCode, UpdateCourseDto updateCourseDto);
        Task<bool> DeleteCourseAsync(string courseCode);
        Task<bool> CourseExistsAsync(string courseCode);
    }
}
