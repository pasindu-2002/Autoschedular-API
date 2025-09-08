using Microsoft.AspNetCore.Mvc;
using autoschedular.Model.DTOs;
using autoschedular.Services;

namespace autoschedular.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CourseController : ControllerBase
    {
        private readonly ICourseServices _courseServices;

        public CourseController(ICourseServices courseServices)
        {
            _courseServices = courseServices;
        }

        [HttpGet]
        public async Task<IActionResult> GetCourse([FromQuery] string courseCode)
        {
            if (string.IsNullOrEmpty(courseCode))
            {
                return BadRequest(new ApiResponse<object>
                {
                    Message = "Please provide course code"
                });
            }

            var course = await _courseServices.GetCourseAsync(courseCode);
            
            if (course == null)
            {
                return NotFound(new ApiResponse<object>
                {
                    Message = "Course not found"
                });
            }

            return Ok(new ApiResponse<CourseResponseDto>
            {
                Message = "Course fetched successfully",
                Data = course
            });
        }

        [HttpPost]
        public async Task<IActionResult> CreateCourse([FromBody] CreateCourseDto createCourseDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResponse<object>
                {
                    Message = "Missing required fields"
                });
            }

            var success = await _courseServices.CreateCourseAsync(createCourseDto);
            
            if (!success)
            {
                return Conflict(new ApiResponse<object>
                {
                    Message = "Course with this code already exists"
                });
            }

            return CreatedAtAction(nameof(GetCourse), 
                new { courseCode = createCourseDto.CourseCode },
                new ApiResponse<object>
                {
                    Message = "Course added successfully"
                });
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCourse([FromQuery] string courseCode, [FromBody] UpdateCourseDto updateCourseDto)
        {
            if (string.IsNullOrEmpty(courseCode))
            {
                return BadRequest(new ApiResponse<object>
                {
                    Message = "Please provide course_code"
                });
            }

            if (string.IsNullOrEmpty(updateCourseDto.CourseName) && string.IsNullOrEmpty(updateCourseDto.School))
            {
                return BadRequest(new ApiResponse<object>
                {
                    Message = "No fields to update"
                });
            }

            var success = await _courseServices.UpdateCourseAsync(courseCode, updateCourseDto);
            
            if (!success)
            {
                return NotFound(new ApiResponse<object>
                {
                    Message = "Course not found or no changes made"
                });
            }

            return Ok(new ApiResponse<object>
            {
                Message = "Course updated successfully"
            });
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCourse([FromQuery] string courseCode)
        {
            if (string.IsNullOrEmpty(courseCode))
            {
                return BadRequest(new ApiResponse<object>
                {
                    Message = "Please provide course_code"
                });
            }

            var success = await _courseServices.DeleteCourseAsync(courseCode);
            
            if (!success)
            {
                return NotFound(new ApiResponse<object>
                {
                    Message = "Course not found"
                });
            }

            return Ok(new ApiResponse<object>
            {
                Message = "Course deleted successfully"
            });
        }
    }
}
