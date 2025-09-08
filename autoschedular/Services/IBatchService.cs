using autoschedular.Model;
using autoschedular.Model.DTOs;

namespace autoschedular.Services
{
    public interface IBatchService
    {
        Task<BatchResponseDto?> GetBatchAsync(string batchCode);
        Task<List<BatchResponseDto>> GetAllBatchesAsync();
        Task<bool> CreateBatchAsync(CreateBatchDto createBatchDto);
        Task<bool> UpdateBatchAsync(string batchCode, UpdateBatchDto updateBatchDto);
        Task<bool> DeleteBatchAsync(string batchCode);
        Task<bool> BatchExistsAsync(string batchCode);
    }
}
