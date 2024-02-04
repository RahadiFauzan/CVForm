using System.ComponentModel.DataAnnotations;

namespace CVForm.Models
{
    public class OtherExperienceModel
    {
        [Key]
        public Guid OtherExperienceID { get; set; } = Guid.NewGuid();
        public string CVID { get; set; } = string.Empty;
        public string OrganizationName { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public string StartDate { get; set; } = string.Empty;
        public string EndDate { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string CertificateLink { get; set; } = string.Empty;
    }
}
