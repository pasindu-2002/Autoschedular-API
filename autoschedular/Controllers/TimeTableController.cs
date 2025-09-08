using autoschedular.Model.DTOs;
using autoschedular.Services;
using Microsoft.AspNetCore.Mvc;

namespace autoschedular.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TimeTableController : ControllerBase
    {
        private readonly ITimeTableService _timeTableService;

        public TimeTableController(ITimeTableService timeTableService)
        {
            _timeTableService = timeTableService;
        }

        /// <summary>
        /// Generates a timetable based on the provided parameters
        /// </summary>
        /// <param name="request">Timetable generation request containing lecturer, module, batch, session type, and date range</param>
        /// <returns>Response containing success status, message, and generated dates</returns>
        [HttpPost("generate")]
        public async Task<ActionResult<GenerateTimetableResponseDTO>> GenerateTimetable([FromBody] GenerateTimetableRequestDTO request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new GenerateTimetableResponseDTO
                {
                    Success = false,
                    Message = "Invalid request data",
                    Details = ModelState
                });
            }

            var result = await _timeTableService.GenerateTimetableAsync(request);

            if (!result.Success)
            {
                if (result.Message.Contains("Missing required fields") || 
                    result.Message.Contains("not supported") ||
                    result.Message.Contains("Not enough available dates"))
                {
                    return BadRequest(result);
                }
                else if (result.Message.Contains("not found"))
                {
                    return NotFound(result);
                }
                else if (result.Message.Contains("Database error"))
                {
                    return StatusCode(500, result);
                }
            }

            return Ok(result);
        }
    }
}
