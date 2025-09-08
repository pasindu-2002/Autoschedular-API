using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace autoschedular.Model
{
    public class LecturerTimetable
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [MaxLength(50)]
        public string LecturerId { get; set; } = string.Empty;
        
        [MaxLength(255)]
        public string? Description { get; set; }
        
        [Required]
        public DateTime Date { get; set; }

        // Navigation property
        [ForeignKey("LecturerId")]
        public virtual Lecturer? Lecturer { get; set; }
    }
}
