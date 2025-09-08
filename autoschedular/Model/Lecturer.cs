using System.ComponentModel.DataAnnotations;

namespace autoschedular.Model
{
    public class Lecturer
    {
        [Key]
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
        public string Password { get; set; } = string.Empty;

        // Navigation property for Batches (One-to-Many)
        public virtual ICollection<Batch> Batches { get; set; } = new List<Batch>();
        
        // Navigation property for LecturerTimetables (One-to-Many)
        public virtual ICollection<LecturerTimetable> LecturerTimetables { get; set; } = new List<LecturerTimetable>();
    }
}
