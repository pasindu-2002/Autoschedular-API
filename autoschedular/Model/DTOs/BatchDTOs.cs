using System.ComponentModel.DataAnnotations;

namespace autoschedular.Model.DTOs
{
    public class CreateBatchDto
    {
        [Required]
        [MaxLength(50)]
        public string BatchCode { get; set; } = string.Empty;
        
        [Required]
        [MaxLength(50)]
        public string CourseCode { get; set; } = string.Empty;
        
        [Required]
        [MaxLength(255)]
        public string CourseDirector { get; set; } = string.Empty;
    }

    public class UpdateBatchDto
    {
        [MaxLength(50)]
        public string? CourseCode { get; set; }
        
        [MaxLength(255)]
        public string? CourseDirector { get; set; }
    }

    public class BatchResponseDto
    {
        public string BatchCode { get; set; } = string.Empty;
        public string CourseCode { get; set; } = string.Empty;
        public string CourseDirector { get; set; } = string.Empty;
        public LecturerInfo? LecturerDetails { get; set; }
    }

    public class LecturerInfo
    {
        public string EmpNo { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
}
