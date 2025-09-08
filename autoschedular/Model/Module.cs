using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace autoschedular.Model
{
    public class Module
    {
        [Key]
        [MaxLength(50)]
        public string ModuleCode { get; set; } = string.Empty;
        
        [Required]
        [MaxLength(255)]
        public string ModuleName { get; set; } = string.Empty;
        
        [Required]
        public int ModuleHours { get; set; }
        
        [Required]
        [MaxLength(50)]
        public string CourseCode { get; set; } = string.Empty;

        // Navigation property for Course (Many-to-One)
        [ForeignKey(nameof(CourseCode))]
        public virtual Course? Course { get; set; }
    }
}
