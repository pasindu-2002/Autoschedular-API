using System.ComponentModel.DataAnnotations;

namespace autoschedular.Model.DTOs
{
    public class CreateCourseDto
    {
        [Required]
        [MaxLength(50)]
        public string CourseCode { get; set; } = string.Empty;
        
        [Required]
        [MaxLength(255)]
        public string CourseName { get; set; } = string.Empty;
        
        [Required]
        [MaxLength(255)]
        public string School { get; set; } = string.Empty;
    }

    public class UpdateCourseDto
    {
        [MaxLength(255)]
        public string? CourseName { get; set; }
        
        [MaxLength(255)]
        public string? School { get; set; }
    }

    public class CourseResponseDto
    {
        public string CourseCode { get; set; } = string.Empty;
        public string CourseName { get; set; } = string.Empty;
        public string School { get; set; } = string.Empty;
    }

}
