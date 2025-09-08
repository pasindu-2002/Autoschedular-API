using autoschedular.Model;
using autoschedular.Model.DTOs;

namespace autoschedular.Services
{
    public interface ILecturerService
    {
        Task<LecturerResponseDto?> GetLecturerAsync(string empNo, string password);
        Task<LecturerWithBatchesDto?> GetLecturerWithBatchesAsync(string empNo);
        Task<bool> CreateLecturerAsync(CreateLecturerDto createLecturerDto);
        Task<bool> UpdateLecturerAsync(string empNo, UpdateLecturerDto updateLecturerDto);
        Task<bool> DeleteLecturerAsync(string empNo);
        Task<bool> LecturerExistsAsync(string empNo);
    }
}
