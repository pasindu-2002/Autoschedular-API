using System.ComponentModel.DataAnnotations;

namespace autoschedular.Model.DTOs
{
    public class CreatePoStaffDto
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

    public class UpdatePoStaffDto
    {
        [MaxLength(255)]
        public string? FullName { get; set; }
        
        [MaxLength(255)]
        [EmailAddress]
        public string? Email { get; set; }
        
        [MinLength(6)]
        public string? Password { get; set; }
    }

    public class PoStaffLoginDto
    {
        [Required]
        public string EmpNo { get; set; } = string.Empty;
        
        [Required]
        public string Password { get; set; } = string.Empty;
    }

    public class PoStaffResponseDto
    {
        public string EmpNo { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
}
