using Microsoft.AspNetCore.Mvc;
using autoschedular.Model.DTOs;
using autoschedular.Services;

namespace autoschedular.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetStudent([FromQuery] string stuId, [FromQuery] string password)
        {
            if (string.IsNullOrEmpty(stuId) || string.IsNullOrEmpty(password))
            {
                return BadRequest(new ApiResponse<object>
                {
                    Message = "Please provide stu_id and password"
                });
            }

            var student = await _studentService.GetStudentAsync(stuId, password);
            
            if (student == null)
            {
                return NotFound(new ApiResponse<object>
                {
                    Message = "Student not found or invalid password"
                });
            }

            return Ok(new ApiResponse<StudentResponseDto>
            {
                Message = "Student fetched successfully",
                Data = student
            });
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllStudents()
        {
            var students = await _studentService.GetAllStudentsAsync();
            
            return Ok(new ApiResponse<List<StudentResponseDto>>
            {
                Message = "All students fetched successfully",
                Data = students
            });
        }

        [HttpPost]
        public async Task<IActionResult> CreateStudent([FromBody] CreateStudentDto createStudentDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResponse<object>
                {
                    Message = "Missing required fields"
                });
            }

            var success = await _studentService.CreateStudentAsync(createStudentDto);
            
            if (!success)
            {
                return Conflict(new ApiResponse<object>
                {
                    Message = "Student with this ID already exists"
                });
            }

            return CreatedAtAction(nameof(GetStudent), 
                new { stuId = createStudentDto.StuId, password = "***" },
                new ApiResponse<object>
                {
                    Message = "Student added successfully"
                });
        }

        [HttpPut]
        public async Task<IActionResult> UpdateStudent([FromQuery] string stuId, [FromBody] UpdateStudentDto updateStudentDto)
        {
            if (string.IsNullOrEmpty(stuId))
            {
                return BadRequest(new ApiResponse<object>
                {
                    Message = "Please provide stu_id"
                });
            }

            if (string.IsNullOrEmpty(updateStudentDto.FullName) && 
                string.IsNullOrEmpty(updateStudentDto.Email) && 
                string.IsNullOrEmpty(updateStudentDto.Password) && 
                string.IsNullOrEmpty(updateStudentDto.BatchCode))
            {
                return BadRequest(new ApiResponse<object>
                {
                    Message = "No fields to update"
                });
            }

            var success = await _studentService.UpdateStudentAsync(stuId, updateStudentDto);
            
            if (!success)
            {
                return NotFound(new ApiResponse<object>
                {
                    Message = "Student not found or no changes made"
                });
            }

            return Ok(new ApiResponse<object>
            {
                Message = "Student updated successfully"
            });
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteStudent([FromQuery] string stuId)
        {
            if (string.IsNullOrEmpty(stuId))
            {
                return BadRequest(new ApiResponse<object>
                {
                    Message = "Please provide stu_id"
                });
            }

            var success = await _studentService.DeleteStudentAsync(stuId);
            
            if (!success)
            {
                return NotFound(new ApiResponse<object>
                {
                    Message = "Student not found"
                });
            }

            return Ok(new ApiResponse<object>
            {
                Message = "Student deleted successfully"
            });
        }

        [HttpGet("batch/{batchCode}")]
        public async Task<IActionResult> GetStudentsByBatch(string batchCode)
        {
            if (string.IsNullOrEmpty(batchCode))
            {
                return BadRequest(new ApiResponse<object>
                {
                    Message = "Please provide batch_code"
                });
            }

            var students = await _studentService.GetStudentsByBatchAsync(batchCode);
            
            return Ok(new ApiResponse<List<StudentResponseDto>>
            {
                Message = $"Students in batch {batchCode} fetched successfully",
                Data = students
            });
        }
    }
}
