using System.ComponentModel.DataAnnotations;

namespace CVForm.Models
{
    public class EducationModel
    {
        [Key]
        public Guid EducationID { get; set; } = Guid.NewGuid();
        public string CVID { get; set; } = string.Empty;
        public string School { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public string StartDate { get; set; } = string.Empty;
        public string EndDate { get; set; } = string.Empty;
        public string Degree { get; set; } = string.Empty;
        public string Major { get; set; } = string.Empty;
        public decimal GPA { get; set; }
        public string Description { get; set; } = string.Empty;
        public string CertificateLink { get; set; } = string.Empty;
    }
}
