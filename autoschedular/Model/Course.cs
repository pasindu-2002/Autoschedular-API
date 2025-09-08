using System.ComponentModel.DataAnnotations;

namespace autoschedular.Model
{
    public class Course
    {
        [Key]
        [MaxLength(50)]
        public string CourseCode { get; set; } = string.Empty;
        
        [Required]
        [MaxLength(255)]
        public string CourseName { get; set; } = string.Empty;
        
        [Required]
        [MaxLength(255)]
        public string School { get; set; } = string.Empty;

        // Navigation property for Batches (One-to-Many)
        public virtual ICollection<Batch> Batches { get; set; } = new List<Batch>();
        
        // Navigation property for Modules (One-to-Many)
        public virtual ICollection<Module> Modules { get; set; } = new List<Module>();
    }
}
