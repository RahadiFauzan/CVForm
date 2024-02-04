using System.ComponentModel.DataAnnotations;

namespace CVForm.Models
{
    public class WorkExperienceModel
    {
        [Key]
        public Guid WorkExperienceID { get; set; } = Guid.NewGuid();
        public string CVID { get; set; } = string.Empty;
        public string CompanyName { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public string StartDate { get; set; } = string.Empty;
        public string EndDate { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}
