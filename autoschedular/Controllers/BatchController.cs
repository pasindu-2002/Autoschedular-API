using Microsoft.AspNetCore.Mvc;
using autoschedular.Model.DTOs;
using autoschedular.Services;

namespace autoschedular.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BatchController : ControllerBase
    {
        private readonly IBatchService _batchService;

        public BatchController(IBatchService batchService)
        {
            _batchService = batchService;
        }

        [HttpGet]
        public async Task<IActionResult> GetBatch([FromQuery] string batchCode)
        {
            if (string.IsNullOrEmpty(batchCode))
            {
                return BadRequest(new ApiResponse<object>
                {
                    Message = "Please provide batch_code"
                });
            }

            var batch = await _batchService.GetBatchAsync(batchCode);
            
            if (batch == null)
            {
                return NotFound(new ApiResponse<object>
                {
                    Message = "Batch not found"
                });
            }

            return Ok(new ApiResponse<BatchResponseDto>
            {
                Message = "Batch fetched successfully",
                Data = batch
            });
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllBatches()
        {
            var batches = await _batchService.GetAllBatchesAsync();
            
            return Ok(new ApiResponse<List<BatchResponseDto>>
            {
                Message = "All batches fetched successfully",
                Data = batches
            });
        }

        [HttpPost]
        public async Task<IActionResult> CreateBatch([FromBody] CreateBatchDto createBatchDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResponse<object>
                {
                    Message = "Missing required fields"
                });
            }

            var success = await _batchService.CreateBatchAsync(createBatchDto);
            
            if (!success)
            {
                return Conflict(new ApiResponse<object>
                {
                    Message = "Batch with this code already exists or lecturer not found"
                });
            }

            return CreatedAtAction(nameof(GetBatch), 
                new { batchCode = createBatchDto.BatchCode },
                new ApiResponse<object>
                {
                    Message = "Batch added successfully"
                });
        }

        [HttpPut]
        public async Task<IActionResult> UpdateBatch([FromQuery] string batchCode, [FromBody] UpdateBatchDto updateBatchDto)
        {
            if (string.IsNullOrEmpty(batchCode))
            {
                return BadRequest(new ApiResponse<object>
                {
                    Message = "Please provide batch_code"
                });
            }

            if (string.IsNullOrEmpty(updateBatchDto.CourseCode) && string.IsNullOrEmpty(updateBatchDto.CourseDirector))
            {
                return BadRequest(new ApiResponse<object>
                {
                    Message = "No fields to update"
                });
            }

            var success = await _batchService.UpdateBatchAsync(batchCode, updateBatchDto);
            
            if (!success)
            {
                return NotFound(new ApiResponse<object>
                {
                    Message = "Batch not found, lecturer not found, or no changes made"
                });
            }

            return Ok(new ApiResponse<object>
            {
                Message = "Batch updated successfully"
            });
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteBatch([FromQuery] string batchCode)
        {
            if (string.IsNullOrEmpty(batchCode))
            {
                return BadRequest(new ApiResponse<object>
                {
                    Message = "Please provide batch_code"
                });
            }

            var success = await _batchService.DeleteBatchAsync(batchCode);
            
            if (!success)
            {
                return NotFound(new ApiResponse<object>
                {
                    Message = "Batch not found"
                });
            }

            return Ok(new ApiResponse<object>
            {
                Message = "Batch deleted successfully"
            });
        }
    }
}