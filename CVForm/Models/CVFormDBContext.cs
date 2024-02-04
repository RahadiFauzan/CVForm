using Microsoft.EntityFrameworkCore;

namespace CVForm.Models
{
    public class CVFormDBContext : DbContext
    {
        public CVFormDBContext(DbContextOptions<CVFormDBContext> options) : base(options)
        {

        }
        public DbSet<CVModel> CV { get; set; }
        public DbSet<WorkExperienceModel> WorkExperience { get; set; }
        public DbSet<EducationModel> Education{ get; set; }
        public DbSet<OtherExperienceModel> OtherExperience { get; set; }
        public DbSet<SkillsModel> Skills { get; set; }
    }
}
