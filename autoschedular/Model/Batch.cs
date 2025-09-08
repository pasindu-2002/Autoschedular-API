using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace autoschedular.Model
{
    public class Batch
    {
        [Key]
        [MaxLength(50)]
        public string BatchCode { get; set; } = string.Empty;
        
        [Required]
        [MaxLength(50)]
        public string CourseCode { get; set; } = string.Empty;
        
        [Required]
        [MaxLength(50)]
        public string CourseDirector { get; set; } = string.Empty;

        // Navigation property for Course
        [ForeignKey(nameof(CourseCode))]
        public virtual Course? Course { get; set; }

        // Navigation property for Lecturer
        [ForeignKey(nameof(CourseDirector))]
        public virtual Lecturer? Lecturer { get; set; }

        // Navigation property for Students (One-to-Many)
        public virtual ICollection<Student> Students { get; set; } = new List<Student>();
    }
}
