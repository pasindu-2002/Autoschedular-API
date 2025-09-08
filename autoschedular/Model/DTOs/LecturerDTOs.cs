using System.ComponentModel.DataAnnotations;

namespace autoschedular.Model.DTOs
{
    public class CreateLecturerDto
    {
        [Required]
        [MaxLength(50)]
        public string EmpNo { get; set; } = string.Empty;
        
        [Required]
        [MaxLength(255)]
        public string FullName { get; set; } = string.Empty;
        
        [Required]
        [MaxLength(255)]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        
        [Required]
        [MinLength(6)]
        public string Password { get; set; } = string.Empty;
    }

    public class UpdateLecturerDto
    {
        [MaxLength(255)]
        public string? FullName { get; set; }
        
        [MaxLength(255)]
        [EmailAddress]
        public string? Email { get; set; }
        
        [MinLength(6)]
        public string? Password { get; set; }
    }

    public class LecturerLoginDto
    {
        [Required]
        public string EmpNo { get; set; } = string.Empty;
        
        [Required]
        public string Password { get; set; } = string.Empty;
    }

    public class LecturerResponseDto
    {
        public string EmpNo { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }

    public class LecturerWithBatchesDto
    {
        public string EmpNo { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public List<BatchInfoDto> Batches { get; set; } = new List<BatchInfoDto>();
    }

    public class BatchInfoDto
    {
        public string BatchCode { get; set; } = string.Empty;
        public string CourseCode { get; set; } = string.Empty;
        public string? CourseName { get; set; }
    }
}
