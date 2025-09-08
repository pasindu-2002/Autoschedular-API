using autoschedular.Model.DTOs;

namespace autoschedular.Services
{
    public interface ITimeTableService
    {
        Task<GenerateTimetableResponseDTO> GenerateTimetableAsync(GenerateTimetableRequestDTO request);
    }
}
