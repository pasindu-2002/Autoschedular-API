using System.ComponentModel.DataAnnotations;

namespace autoschedular.Model.DTOs
{
    public class CreateModuleDTO
    {
        [Required(ErrorMessage = "Module code is required")]
        [MaxLength(50, ErrorMessage = "Module code cannot exceed 50 characters")]
        public string ModuleCode { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Module name is required")]
        [MaxLength(255, ErrorMessage = "Module name cannot exceed 255 characters")]
        public string ModuleName { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Module hours is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Module hours must be greater than 0")]
        public int ModuleHours { get; set; }
        
        [Required(ErrorMessage = "Course code is required")]
        [MaxLength(50, ErrorMessage = "Course code cannot exceed 50 characters")]
        public string CourseCode { get; set; } = string.Empty;
    }

    public class UpdateModuleDTO
    {
        [MaxLength(255, ErrorMessage = "Module name cannot exceed 255 characters")]
        public string? ModuleName { get; set; }
        
        [Range(1, int.MaxValue, ErrorMessage = "Module hours must be greater than 0")]
        public int? ModuleHours { get; set; }
        
        [MaxLength(50, ErrorMessage = "Course code cannot exceed 50 characters")]
        public string? CourseCode { get; set; }
    }

    public class ModuleResponseDTO
    {
        public string ModuleCode { get; set; } = string.Empty;
        public string ModuleName { get; set; } = string.Empty;
        public int ModuleHours { get; set; }
        public string CourseCode { get; set; } = string.Empty;
        public string? CourseName { get; set; }
    }

}
