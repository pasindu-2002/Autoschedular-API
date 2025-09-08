using Microsoft.AspNetCore.Mvc;
using autoschedular.Model.DTOs;
using autoschedular.Services;

namespace autoschedular.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LecturerTimetableController : ControllerBase
    {
        private readonly ILecturerTimetableService _lecturerTimetableService;

        public LecturerTimetableController(ILecturerTimetableService lecturerTimetableService)
        {
            _lecturerTimetableService = lecturerTimetableService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllLecturerTimetables()
        {
            var timetables = await _lecturerTimetableService.GetAllLecturerTimetablesAsync();
            
            return Ok(new ApiResponse<IEnumerable<LecturerTimetableResponseDto>>
            {
                Message = "Lecturer timetables fetched successfully",
                Data = timetables
            });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetLecturerTimetableById(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new ApiResponse<object>
                {
                    Message = "Invalid timetable ID"
                });
            }

            var timetable = await _lecturerTimetableService.GetLecturerTimetableByIdAsync(id);
            
            if (timetable == null)
            {
                return NotFound(new ApiResponse<object>
                {
                    Message = "Lecturer timetable not found"
                });
            }

            return Ok(new ApiResponse<LecturerTimetableResponseDto>
            {
                Message = "Lecturer timetable fetched successfully",
                Data = timetable
            });
        }

        [HttpGet("lecturer/{lecturerId}")]
        public async Task<IActionResult> GetLecturerTimetablesByLecturerId(string lecturerId)
        {
            if (string.IsNullOrEmpty(lecturerId))
            {
                return BadRequest(new ApiResponse<object>
                {
                    Message = "Please provide lecturer ID"
                });
            }

            var timetables = await _lecturerTimetableService.GetLecturerTimetablesByLecturerIdAsync(lecturerId);
            
            return Ok(new ApiResponse<IEnumerable<LecturerTimetableResponseDto>>
            {
                Message = "Lecturer timetables fetched successfully",
                Data = timetables
            });
        }

        [HttpGet("date/{date}")]
        public async Task<IActionResult> GetLecturerTimetablesByDate(DateTime date)
        {
            var timetables = await _lecturerTimetableService.GetLecturerTimetablesByDateAsync(date);
            
            return Ok(new ApiResponse<IEnumerable<LecturerTimetableResponseDto>>
            {
                Message = "Lecturer timetables for the specified date fetched successfully",
                Data = timetables
            });
        }

        [HttpGet("daterange")]
        public async Task<IActionResult> GetLecturerTimetablesByDateRange([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            if (startDate > endDate)
            {
                return BadRequest(new ApiResponse<object>
                {
                    Message = "Start date cannot be greater than end date"
                });
            }

            var timetables = await _lecturerTimetableService.GetLecturerTimetablesByDateRangeAsync(startDate, endDate);
            
            return Ok(new ApiResponse<IEnumerable<LecturerTimetableResponseDto>>
            {
                Message = "Lecturer timetables for the specified date range fetched successfully",
                Data = timetables
            });
        }

        [HttpPost]
        public async Task<IActionResult> CreateLecturerTimetable([FromBody] CreateLecturerTimetableDto createDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResponse<object>
                {
                    Message = "Missing required fields"
                });
            }

            var success = await _lecturerTimetableService.CreateLecturerTimetableAsync(createDto);
            
            if (!success)
            {
                return BadRequest(new ApiResponse<object>
                {
                    Message = "Failed to create lecturer timetable. Please ensure the lecturer exists."
                });
            }

            return CreatedAtAction(nameof(GetLecturerTimetableById), 
                new { id = 0 }, // We don't have the ID here, but this follows REST conventions
                new ApiResponse<object>
                {
                    Message = "Lecturer timetable created successfully"
                });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateLecturerTimetable(int id, [FromBody] UpdateLecturerTimetableDto updateDto)
        {
            if (id <= 0)
            {
                return BadRequest(new ApiResponse<object>
                {
                    Message = "Invalid timetable ID"
                });
            }

            if (string.IsNullOrEmpty(updateDto.Description) && !updateDto.Date.HasValue)
            {
                return BadRequest(new ApiResponse<object>
                {
                    Message = "No fields to update"
                });
            }

            var success = await _lecturerTimetableService.UpdateLecturerTimetableAsync(id, updateDto);
            
            if (!success)
            {
                return NotFound(new ApiResponse<object>
                {
                    Message = "Lecturer timetable not found or no changes made"
                });
            }

            return Ok(new ApiResponse<object>
            {
                Message = "Lecturer timetable updated successfully"
            });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLecturerTimetable(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new ApiResponse<object>
                {
                    Message = "Invalid timetable ID"
                });
            }

            var success = await _lecturerTimetableService.DeleteLecturerTimetableAsync(id);
            
            if (!success)
            {
                return NotFound(new ApiResponse<object>
                {
                    Message = "Lecturer timetable not found"
                });
            }

            return Ok(new ApiResponse<object>
            {
                Message = "Lecturer timetable deleted successfully"
            });
        }
    }
}
