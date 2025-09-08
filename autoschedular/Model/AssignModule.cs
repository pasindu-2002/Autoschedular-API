using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace autoschedular.Model
{
    public class AssignModule
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        
        [Required]
        [MaxLength(50)]
        public string BatchId { get; set; } = string.Empty;
        
        [Required]
        [MaxLength(50)]
        public string LecturerId { get; set; } = string.Empty;
        
        [Required]
        [MaxLength(50)]
        public string ModuleId { get; set; } = string.Empty;
        
        [Required]
        public DateTime Date { get; set; }
        
        [Required]
        [MaxLength(20)]
        public string Status { get; set; } = string.Empty;
        
        [Required]
        [MaxLength(50)]
        public string SessionType { get; set; } = string.Empty;

        // Navigation properties
        [ForeignKey("BatchId")]
        public virtual Batch? Batch { get; set; }
        
        [ForeignKey("LecturerId")]
        public virtual Lecturer? Lecturer { get; set; }
        
        [ForeignKey("ModuleId")]
        public virtual Module? Module { get; set; }
    }
}
