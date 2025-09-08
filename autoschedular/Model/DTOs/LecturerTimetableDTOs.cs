using System.ComponentModel.DataAnnotations;

namespace autoschedular.Model.DTOs
{
    public class CreateLecturerTimetableDto
    {
        [Required]
        [MaxLength(50)]
        public string LecturerId { get; set; } = string.Empty;
        
        [MaxLength(255)]
        public string? Description { get; set; }
        
        [Required]
        public DateTime Date { get; set; }
    }

    public class UpdateLecturerTimetableDto
    {
        [MaxLength(255)]
        public string? Description { get; set; }
        
        public DateTime? Date { get; set; }
    }

    public class LecturerTimetableResponseDto
    {
        public int Id { get; set; }
        public string LecturerId { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateTime Date { get; set; }
        public string? LecturerName { get; set; }
    }

    public class LecturerTimetableWithLecturerDto
    {
        public int Id { get; set; }
        public string LecturerId { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateTime Date { get; set; }
        public LecturerResponseDto? Lecturer { get; set; }
    }
}
