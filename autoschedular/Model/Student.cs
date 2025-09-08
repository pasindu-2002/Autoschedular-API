using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace autoschedular.Model
{
    public class Student
    {
        [Key]
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
        public string Password { get; set; } = string.Empty;
        
        // Foreign key for Batch relationship
        [Required]
        [MaxLength(50)]
        public string BatchCode { get; set; } = string.Empty;

        // Navigation property
        [ForeignKey(nameof(BatchCode))]
        public virtual Batch? Batch { get; set; }
    }
}
