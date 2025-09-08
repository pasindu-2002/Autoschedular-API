using System.ComponentModel.DataAnnotations;

namespace autoschedular.Model.DTOs
{
    public class CreateStudentDto
    {
        [Required]
        [MaxLength(50)]
        public string StuId { get; set; } = string.Empty;
        
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
        
        [Required]
        [MaxLength(50)]
        public string BatchCode { get; set; } = string.Empty;
    }

    public class UpdateStudentDto
    {
        [MaxLength(255)]
        public string? FullName { get; set; }
        
        [MaxLength(255)]
        [EmailAddress]
        public string? Email { get; set; }
        
        [MinLength(6)]
        public string? Password { get; set; }
        
        [MaxLength(50)]
        public string? BatchCode { get; set; }
    }

    public class StudentLoginDto
    {
        [Required]
        public string StuId { get; set; } = string.Empty;
        
        [Required]
        public string Password { get; set; } = string.Empty;
    }

    public class StudentResponseDto
    {
        public string StuId { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string BatchCode { get; set; } = string.Empty;
    }
}
