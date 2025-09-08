using Microsoft.AspNetCore.Mvc;
using autoschedular.Model.DTOs;
using autoschedular.Services;

namespace autoschedular.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AssignModuleController : ControllerBase
    {
        private readonly IAssignModuleService _assignModuleService;

        public AssignModuleController(IAssignModuleService assignModuleService)
        {
            _assignModuleService = assignModuleService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAssignModules()
        {
            var assignments = await _assignModuleService.GetAllAssignModulesAsync();
            
            return Ok(new ApiResponse<IEnumerable<AssignModuleResponseDto>>
            {
                Message = "Module assignments fetched successfully",
                Data = assignments
            });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAssignModuleById(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest(new ApiResponse<object>
                {
                    Message = "Invalid assignment ID"
                });
            }

            var assignment = await _assignModuleService.GetAssignModuleByIdAsync(id);
            
            if (assignment == null)
            {
                return NotFound(new ApiResponse<object>
                {
                    Message = "Module assignment not found"
                });
            }

            return Ok(new ApiResponse<AssignModuleResponseDto>
            {
                Message = "Module assignment fetched successfully",
                Data = assignment
            });
        }

        [HttpGet("{id}/details")]
        public async Task<IActionResult> GetAssignModuleWithDetails(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest(new ApiResponse<object>
                {
                    Message = "Invalid assignment ID"
                });
            }

            var assignment = await _assignModuleService.GetAssignModuleWithDetailsAsync(id);
            
            if (assignment == null)
            {
                return NotFound(new ApiResponse<object>
                {
                    Message = "Module assignment not found"
                });
            }

            return Ok(new ApiResponse<AssignModuleWithDetailsDto>
            {
                Message = "Module assignment with details fetched successfully",
                Data = assignment
            });
        }

        [HttpGet("batch/{batchId}")]
        public async Task<IActionResult> GetAssignModulesByBatchId(string batchId)
        {
            if (string.IsNullOrEmpty(batchId))
            {
                return BadRequest(new ApiResponse<object>
                {
                    Message = "Please provide batch ID"
                });
            }

            var assignments = await _assignModuleService.GetAssignModulesByBatchIdAsync(batchId);
            
            return Ok(new ApiResponse<IEnumerable<AssignModuleResponseDto>>
            {
                Message = "Module assignments for batch fetched successfully",
                Data = assignments
            });
        }

        [HttpGet("lecturer/{lecturerId}")]
        public async Task<IActionResult> GetAssignModulesByLecturerId(string lecturerId)
        {
            if (string.IsNullOrEmpty(lecturerId))
            {
                return BadRequest(new ApiResponse<object>
                {
                    Message = "Please provide lecturer ID"
                });
            }

            var assignments = await _assignModuleService.GetAssignModulesByLecturerIdAsync(lecturerId);
            
            return Ok(new ApiResponse<IEnumerable<AssignModuleResponseDto>>
            {
                Message = "Module assignments for lecturer fetched successfully",
                Data = assignments
            });
        }

        [HttpGet("module/{moduleId}")]
        public async Task<IActionResult> GetAssignModulesByModuleId(string moduleId)
        {
            if (string.IsNullOrEmpty(moduleId))
            {
                return BadRequest(new ApiResponse<object>
                {
                    Message = "Please provide module ID"
                });
            }

            var assignments = await _assignModuleService.GetAssignModulesByModuleIdAsync(moduleId);
            
            return Ok(new ApiResponse<IEnumerable<AssignModuleResponseDto>>
            {
                Message = "Module assignments for module fetched successfully",
                Data = assignments
            });
        }

        [HttpGet("date/{date}")]
        public async Task<IActionResult> GetAssignModulesByDate(DateTime date)
        {
            var assignments = await _assignModuleService.GetAssignModulesByDateAsync(date);
            
            return Ok(new ApiResponse<IEnumerable<AssignModuleResponseDto>>
            {
                Message = "Module assignments for the specified date fetched successfully",
                Data = assignments
            });
        }

        [HttpGet("daterange")]
        public async Task<IActionResult> GetAssignModulesByDateRange([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            if (startDate > endDate)
            {
                return BadRequest(new ApiResponse<object>
                {
                    Message = "Start date cannot be greater than end date"
                });
            }

            var assignments = await _assignModuleService.GetAssignModulesByDateRangeAsync(startDate, endDate);
            
            return Ok(new ApiResponse<IEnumerable<AssignModuleResponseDto>>
            {
                Message = "Module assignments for the specified date range fetched successfully",
                Data = assignments
            });
        }

        [HttpGet("status/{status}")]
        public async Task<IActionResult> GetAssignModulesByStatus(string status)
        {
            if (string.IsNullOrEmpty(status))
            {
                return BadRequest(new ApiResponse<object>
                {
                    Message = "Please provide status"
                });
            }

            var assignments = await _assignModuleService.GetAssignModulesByStatusAsync(status);
            
            return Ok(new ApiResponse<IEnumerable<AssignModuleResponseDto>>
            {
                Message = "Module assignments with specified status fetched successfully",
                Data = assignments
            });
        }

        [HttpGet("sessiontype/{sessionType}")]
        public async Task<IActionResult> GetAssignModulesBySessionType(string sessionType)
        {
            if (string.IsNullOrEmpty(sessionType))
            {
                return BadRequest(new ApiResponse<object>
                {
                    Message = "Please provide session type"
                });
            }

            var assignments = await _assignModuleService.GetAssignModulesBySessionTypeAsync(sessionType);
            
            return Ok(new ApiResponse<IEnumerable<AssignModuleResponseDto>>
            {
                Message = "Module assignments with specified session type fetched successfully",
                Data = assignments
            });
        }

        [HttpPost]
        public async Task<IActionResult> CreateAssignModule([FromBody] CreateAssignModuleDto createDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResponse<object>
                {
                    Message = "Missing required fields"
                });
            }

            var createdId = await _assignModuleService.CreateAssignModuleAsync(createDto);
            
            if (createdId == null)
            {
                return BadRequest(new ApiResponse<object>
                {
                    Message = "Failed to create module assignment. Please ensure the batch, lecturer, and module exist."
                });
            }

            return CreatedAtAction(nameof(GetAssignModuleById), 
                new { id = createdId },
                new ApiResponse<object>
                {
                    Message = "Module assignment created successfully"
                });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAssignModule(Guid id, [FromBody] UpdateAssignModuleDto updateDto)
        {
            if (id == Guid.Empty)
            {
                return BadRequest(new ApiResponse<object>
                {
                    Message = "Invalid assignment ID"
                });
            }

            if (!updateDto.Date.HasValue && 
                string.IsNullOrEmpty(updateDto.Status) && 
                string.IsNullOrEmpty(updateDto.SessionType))
            {
                return BadRequest(new ApiResponse<object>
                {
                    Message = "No fields to update"
                });
            }

            var success = await _assignModuleService.UpdateAssignModuleAsync(id, updateDto);
            
            if (!success)
            {
                return NotFound(new ApiResponse<object>
                {
                    Message = "Module assignment not found or no changes made"
                });
            }

            return Ok(new ApiResponse<object>
            {
                Message = "Module assignment updated successfully"
            });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAssignModule(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest(new ApiResponse<object>
                {
                    Message = "Invalid assignment ID"
                });
            }

            var success = await _assignModuleService.DeleteAssignModuleAsync(id);
            
            if (!success)
            {
                return NotFound(new ApiResponse<object>
                {
                    Message = "Module assignment not found"
                });
            }

            return Ok(new ApiResponse<object>
            {
                Message = "Module assignment deleted successfully"
            });
        }
    }
}
