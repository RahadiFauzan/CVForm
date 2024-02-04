using System.ComponentModel.DataAnnotations;

namespace CVForm.Models
{
    public class SkillsModel
    {
        [Key]
        public Guid SkillID { get; set; } = Guid.NewGuid();
        public string CVID { get; set; } = string.Empty;
        public string SkillName { get; set; } = string.Empty;
    }
}
