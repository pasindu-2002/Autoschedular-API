using Microsoft.AspNetCore.Mvc;
using autoschedular.Model.DTOs;
using autoschedular.Services;

namespace autoschedular.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LecturerController : ControllerBase
    {
        private readonly ILecturerService _lecturerService;

        public LecturerController(ILecturerService lecturerService)
        {
            _lecturerService = lecturerService;
        }

        [HttpGet]
        public async Task<IActionResult> GetLecturer([FromQuery] string empNo, [FromQuery] string password)
        {
            if (string.IsNullOrEmpty(empNo) || string.IsNullOrEmpty(password))
            {
                return BadRequest(new ApiResponse<object>
                {
                    Message = "Please provide emp_no and password"
                });
            }

            var lecturer = await _lecturerService.GetLecturerAsync(empNo, password);
            
            if (lecturer == null)
            {
                return NotFound(new ApiResponse<object>
                {
                    Message = "Lecturer not found or invalid password"
                });
            }

            return Ok(new ApiResponse<LecturerResponseDto>
            {
                Message = "Lecturer fetched successfully",
                Data = lecturer
            });
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllLecturers()
        {
            var lecturers = await _lecturerService.GetAllLecturersAsync();
            
            return Ok(new ApiResponse<List<LecturerResponseDto>>
            {
                Message = "All lecturers fetched successfully",
                Data = lecturers
            });
        }

        [HttpGet("{empNo}/batches")]
        public async Task<IActionResult> GetLecturerWithBatches(string empNo)
        {
            if (string.IsNullOrEmpty(empNo))
            {
                return BadRequest(new ApiResponse<object>
                {
                    Message = "Please provide emp_no"
                });
            }

            var lecturer = await _lecturerService.GetLecturerWithBatchesAsync(empNo);
            
            if (lecturer == null)
            {
                return NotFound(new ApiResponse<object>
                {
                    Message = "Lecturer not found"
                });
            }

            return Ok(new ApiResponse<LecturerWithBatchesDto>
            {
                Message = "Lecturer with batches fetched successfully",
                Data = lecturer
            });
        }

        [HttpPost]
        public async Task<IActionResult> CreateLecturer([FromBody] CreateLecturerDto createLecturerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResponse<object>
                {
                    Message = "Missing required fields"
                });
            }

            var success = await _lecturerService.CreateLecturerAsync(createLecturerDto);
            
            if (!success)
            {
                return Conflict(new ApiResponse<object>
                {
                    Message = "Lecturer with this employee number already exists"
                });
            }

            return CreatedAtAction(nameof(GetLecturer), 
                new { empNo = createLecturerDto.EmpNo, password = "***" },
                new ApiResponse<object>
                {
                    Message = "Lecturer added successfully"
                });
        }

        [HttpPut]
        public async Task<IActionResult> UpdateLecturer([FromQuery] string empNo, [FromBody] UpdateLecturerDto updateLecturerDto)
        {
            if (string.IsNullOrEmpty(empNo))
            {
                return BadRequest(new ApiResponse<object>
                {
                    Message = "Please provide emp_no"
                });
            }

            if (string.IsNullOrEmpty(updateLecturerDto.FullName) && 
                string.IsNullOrEmpty(updateLecturerDto.Email) && 
                string.IsNullOrEmpty(updateLecturerDto.Password))
            {
                return BadRequest(new ApiResponse<object>
                {
                    Message = "No fields to update"
                });
            }

            var success = await _lecturerService.UpdateLecturerAsync(empNo, updateLecturerDto);
            
            if (!success)
            {
                return NotFound(new ApiResponse<object>
                {
                    Message = "Lecturer not found or no changes made"
                });
            }

            return Ok(new ApiResponse<object>
            {
                Message = "Lecturer updated successfully"
            });
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteLecturer([FromQuery] string empNo)
        {
            if (string.IsNullOrEmpty(empNo))
            {
                return BadRequest(new ApiResponse<object>
                {
                    Message = "Please provide emp_no"
                });
            }

            var success = await _lecturerService.DeleteLecturerAsync(empNo);
            
            if (!success)
            {
                return NotFound(new ApiResponse<object>
                {
                    Message = "Lecturer not found"
                });
            }

            return Ok(new ApiResponse<object>
            {
                Message = "Lecturer deleted successfully"
            });
        }
    }
}
