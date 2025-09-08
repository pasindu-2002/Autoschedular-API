using System.ComponentModel.DataAnnotations;

namespace autoschedular.Model.DTOs
{
    public class GenerateTimetableRequestDTO
    {
        [Required]
        public string LecturerId { get; set; } = string.Empty;
        
        [Required]
        public string ModuleCode { get; set; } = string.Empty;
        
        [Required]
        public string BatchCode { get; set; } = string.Empty;
        
        [Required]
        public string SessionType { get; set; } = string.Empty;
        
        [Required]
        public DateTime StartDate { get; set; }
        
        [Required]
        public DateTime EndDate { get; set; }
    }
    
    public class GenerateTimetableResponseDTO
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public List<string> GeneratedDates { get; set; } = new List<string>();
        public object? Details { get; set; }
    }
}
