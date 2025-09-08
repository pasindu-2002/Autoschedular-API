using autoschedular.Model.DTOs;

namespace autoschedular.Services
{
    public interface IStudentService
    {
        Task<StudentResponseDto?> GetStudentAsync(string stuId, string password);
        Task<List<StudentResponseDto>> GetAllStudentsAsync();
        Task<bool> CreateStudentAsync(CreateStudentDto createStudentDto);
        Task<bool> UpdateStudentAsync(string stuId, UpdateStudentDto updateStudentDto);
        Task<bool> DeleteStudentAsync(string stuId);
        Task<bool> StudentExistsAsync(string stuId);
        Task<List<StudentResponseDto>> GetStudentsByBatchAsync(string batchCode);
        Task<bool> BatchExistsAsync(string batchCode);
    }
}
