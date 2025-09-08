using autoschedular.Model.DTOs;

namespace autoschedular.Services
{
    public interface ILecturerTimetableService
    {
        Task<IEnumerable<LecturerTimetableResponseDto>> GetAllLecturerTimetablesAsync();
        Task<LecturerTimetableResponseDto?> GetLecturerTimetableByIdAsync(int id);
        Task<IEnumerable<LecturerTimetableResponseDto>> GetLecturerTimetablesByLecturerIdAsync(string lecturerId);
        Task<IEnumerable<LecturerTimetableResponseDto>> GetLecturerTimetablesByDateAsync(DateTime date);
        Task<IEnumerable<LecturerTimetableResponseDto>> GetLecturerTimetablesByDateRangeAsync(DateTime startDate, DateTime endDate);
        Task<bool> CreateLecturerTimetableAsync(CreateLecturerTimetableDto createDto);
        Task<bool> UpdateLecturerTimetableAsync(int id, UpdateLecturerTimetableDto updateDto);
        Task<bool> DeleteLecturerTimetableAsync(int id);
        Task<bool> LecturerTimetableExistsAsync(int id);
    }
}
