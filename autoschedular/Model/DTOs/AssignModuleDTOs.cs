using System.ComponentModel.DataAnnotations;

namespace autoschedular.Model.DTOs
{
    public class CreateAssignModuleDto
    {
        [Required]
        [MaxLength(50)]
        public string BatchId { get; set; } = string.Empty;
        
        [Required]
        [MaxLength(50)]
        public string LecturerId { get; set; } = string.Empty;
        
        [Required]
        [MaxLength(50)]
        public string ModuleId { get; set; } = string.Empty;
        
        [Required]
        public DateTime Date { get; set; }
        
        [Required]
        [MaxLength(20)]
        public string Status { get; set; } = string.Empty;
        
        [Required]
        [MaxLength(50)]
        public string SessionType { get; set; } = string.Empty;
    }

    public class UpdateAssignModuleDto
    {
        public DateTime? Date { get; set; }
        
        [MaxLength(20)]
        public string? Status { get; set; }
        
        [MaxLength(50)]
        public string? SessionType { get; set; }
    }

    public class AssignModuleResponseDto
    {
        public Guid Id { get; set; }
        public string BatchId { get; set; } = string.Empty;
        public string LecturerId { get; set; } = string.Empty;
        public string ModuleId { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public string Status { get; set; } = string.Empty;
        public string SessionType { get; set; } = string.Empty;
        public string? BatchName { get; set; }
        public string? LecturerName { get; set; }
        public string? ModuleName { get; set; }
    }

    public class AssignModuleWithDetailsDto
    {
        public Guid Id { get; set; }
        public string BatchId { get; set; } = string.Empty;
        public string LecturerId { get; set; } = string.Empty;
        public string ModuleId { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public string Status { get; set; } = string.Empty;
        public string SessionType { get; set; } = string.Empty;
        public BatchInfoDto? Batch { get; set; }
        public LecturerResponseDto? Lecturer { get; set; }
        public ModuleInfoDto? Module { get; set; }
    }

    public class ModuleInfoDto
    {
        public string ModuleCode { get; set; } = string.Empty;
        public string ModuleName { get; set; } = string.Empty;
        public int ModuleHours { get; set; }
        public string CourseCode { get; set; } = string.Empty;
    }
}
