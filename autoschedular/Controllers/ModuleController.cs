using autoschedular.Model.DTOs;
using autoschedular.Services;
using Microsoft.AspNetCore.Mvc;

namespace autoschedular.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModuleController : ControllerBase
    {
        private readonly IModuleService _moduleService;

        public ModuleController(IModuleService moduleService)
        {
            _moduleService = moduleService;
        }

        /// <summary>
        /// Get a specific module by module code
        /// </summary>
        /// <param name="moduleCode">The module code to search for</param>
        /// <returns>Module details</returns>
        [HttpGet("{moduleCode}")]
        public async Task<IActionResult> GetModule(string moduleCode)
        {
            if (string.IsNullOrWhiteSpace(moduleCode))
            {
                return BadRequest(new ApiResponse<object>
                {
                    StatusCode = 400,
                    Message = "Please provide module code",
                    Data = null
                });
            }

            var result = await _moduleService.GetModuleAsync(moduleCode);
            return StatusCode(result.StatusCode, result);
        }

        /// <summary>
        /// Get all modules
        /// </summary>
        /// <returns>List of all modules</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllModules()
        {
            var result = await _moduleService.GetAllModulesAsync();
            return StatusCode(result.StatusCode, result);
        }

        /// <summary>
        /// Create a new module
        /// </summary>
        /// <param name="createModuleDto">Module data to create</param>
        /// <returns>Success message</returns>
        [HttpPost]
        public async Task<IActionResult> CreateModule([FromBody] CreateModuleDTO createModuleDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResponse<object>
                {
                    StatusCode = 400,
                    Message = "Missing required fields",
                    Data = null,
                    Details = ModelState
                });
            }

            var result = await _moduleService.CreateModuleAsync(createModuleDto);
            return StatusCode(result.StatusCode, result);
        }

        /// <summary>
        /// Update an existing module
        /// </summary>
        /// <param name="moduleCode">The module code to update</param>
        /// <param name="updateModuleDto">Updated module data</param>
        /// <returns>Success message</returns>
        [HttpPut("{moduleCode}")]
        public async Task<IActionResult> UpdateModule(string moduleCode, [FromBody] UpdateModuleDTO updateModuleDto)
        {
            if (string.IsNullOrWhiteSpace(moduleCode))
            {
                return BadRequest(new ApiResponse<object>
                {
                    StatusCode = 400,
                    Message = "Please provide module_code",
                    Data = null
                });
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResponse<object>
                {
                    StatusCode = 400,
                    Message = "Invalid data provided",
                    Data = null,
                    Details = ModelState
                });
            }

            var result = await _moduleService.UpdateModuleAsync(moduleCode, updateModuleDto);
            return StatusCode(result.StatusCode, result);
        }

        /// <summary>
        /// Delete a module
        /// </summary>
        /// <param name="moduleCode">The module code to delete</param>
        /// <returns>Success message</returns>
        [HttpDelete("{moduleCode}")]
        public async Task<IActionResult> DeleteModule(string moduleCode)
        {
            if (string.IsNullOrWhiteSpace(moduleCode))
            {
                return BadRequest(new ApiResponse<object>
                {
                    StatusCode = 400,
                    Message = "Please provide module_code",
                    Data = null
                });
            }

            var result = await _moduleService.DeleteModuleAsync(moduleCode);
            return StatusCode(result.StatusCode, result);
        }
    }
}
