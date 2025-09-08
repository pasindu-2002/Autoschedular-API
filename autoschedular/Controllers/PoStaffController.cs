using Microsoft.AspNetCore.Mvc;
using autoschedular.Model.DTOs;
using autoschedular.Services;

namespace autoschedular.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PoStaffController : ControllerBase
    {
        private readonly IPoStaffService _poStaffService;

        public PoStaffController(IPoStaffService poStaffService)
        {
            _poStaffService = poStaffService;
        }

        [HttpGet]
        public async Task<IActionResult> GetEmployee([FromQuery] string empNo, [FromQuery] string password)
        {
            if (string.IsNullOrEmpty(empNo) || string.IsNullOrEmpty(password))
            {
                return BadRequest(new ApiResponse<object>
                {
                    Message = "Please provide emp_no and password"
                });
            }

            var employee = await _poStaffService.GetEmployeeAsync(empNo, password);
            
            if (employee == null)
            {
                return NotFound(new ApiResponse<object>
                {
                    Message = "Employee not found or invalid password"
                });
            }

            return Ok(new ApiResponse<PoStaffResponseDto>
            {
                Message = "Employee fetched successfully",
                Data = employee
            });
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllEmployees()
        {
            var employees = await _poStaffService.GetAllEmployeesAsync();
            
            return Ok(new ApiResponse<List<PoStaffResponseDto>>
            {
                Message = "All employees fetched successfully",
                Data = employees
            });
        }

        [HttpPost]
        public async Task<IActionResult> CreateEmployee([FromBody] CreatePoStaffDto createPoStaffDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResponse<object>
                {
                    Message = "Missing required fields"
                });
            }

            var success = await _poStaffService.CreateEmployeeAsync(createPoStaffDto);
            
            if (!success)
            {
                return Conflict(new ApiResponse<object>
                {
                    Message = "Employee number already exists"
                });
            }

            return CreatedAtAction(nameof(GetEmployee), 
                new { empNo = createPoStaffDto.EmpNo, password = "***" },
                new ApiResponse<object>
                {
                    Message = "Employee added successfully"
                });
        }

        [HttpPut]
        public async Task<IActionResult> UpdateEmployee([FromQuery] string empNo, [FromBody] UpdatePoStaffDto updatePoStaffDto)
        {
            if (string.IsNullOrEmpty(empNo))
            {
                return BadRequest(new ApiResponse<object>
                {
                    Message = "Please provide emp_no"
                });
            }

            if (string.IsNullOrEmpty(updatePoStaffDto.FullName) && 
                string.IsNullOrEmpty(updatePoStaffDto.Email) && 
                string.IsNullOrEmpty(updatePoStaffDto.Password))
            {
                return BadRequest(new ApiResponse<object>
                {
                    Message = "No fields to update"
                });
            }

            var success = await _poStaffService.UpdateEmployeeAsync(empNo, updatePoStaffDto);
            
            if (!success)
            {
                return NotFound(new ApiResponse<object>
                {
                    Message = "Employee not found or no changes made"
                });
            }

            return Ok(new ApiResponse<object>
            {
                Message = "Employee updated successfully"
            });
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteEmployee([FromQuery] string empNo)
        {
            if (string.IsNullOrEmpty(empNo))
            {
                return BadRequest(new ApiResponse<object>
                {
                    Message = "Please provide emp_no"
                });
            }

            var success = await _poStaffService.DeleteEmployeeAsync(empNo);
            
            if (!success)
            {
                return NotFound(new ApiResponse<object>
                {
                    Message = "Employee not found"
                });
            }

            return Ok(new ApiResponse<object>
            {
                Message = "Employee deleted successfully"
            });
        }
    }
}
