using System.ComponentModel.DataAnnotations;

namespace CVForm.Models
{
    public class CVModel
    {
        [Key]
        public Guid CVID { get; set; } = Guid.NewGuid();
        public string CVName { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string EmailAddress { get; set; } = string.Empty;
        public string LinkedinProfile { get; set; } = string.Empty;
        public string PortfolioURL { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}
