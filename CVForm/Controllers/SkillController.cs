using CVForm.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CVForm.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SkillController : Controller
    {
        private readonly CVFormDBContext _cvFormDBContext;
        public SkillController(CVFormDBContext cvFormDBContext)
        {
            _cvFormDBContext = cvFormDBContext;
        }

        [HttpGet("GetSkill/{cvid}")]
        public async Task<ActionResult<IEnumerable<SkillsModel>>> GetSkill(string cvid)
        {
            if (_cvFormDBContext.Skills == null)
            {
                return NotFound();
            }
            // Convert the string parameter to Guid
            if (!Guid.TryParse(cvid, out Guid cvIdGuid))
            {
                return BadRequest("Invalid id format");
            }
            return await _cvFormDBContext.Skills.Where(skill => skill.CVID == cvid).ToListAsync();
        }

        [HttpGet("GetSkillDetail/{id}")]
        public async Task<ActionResult<SkillsModel>> GetSkillDetail(string id)
        {
            if (_cvFormDBContext.Skills == null)
            {
                return NotFound();
            }
            // Convert the string parameter to Guid
            if (!Guid.TryParse(id, out Guid skillIdGuid))
            {
                return BadRequest("Invalid id format");
            }
            // Search for the CVModel with the converted Guid
            var skill = await _cvFormDBContext.Skills.FindAsync(skillIdGuid);

            if (skill == null)
            {
                return NotFound();
            }

            return skill;
        }

        [HttpPost("CreateSkill")]
        public async Task<ActionResult<SkillsModel>> PostSkill(SkillsModel skill)
        {
            _cvFormDBContext.Skills.Add(skill);
            await _cvFormDBContext.SaveChangesAsync();

            return Ok(); ;
        }

        [HttpPut("UpdateSkill/{id}")]
        public async Task<ActionResult<SkillsModel>> UpdateSkill(string id, SkillsModel skill)
        {
            if (id != skill.SkillID.ToString())
            {
                return BadRequest();
            }
            _cvFormDBContext.Entry(skill).State = EntityState.Modified;
            try
            {
                await _cvFormDBContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
            return Ok();
        }

        [HttpDelete("DeleteSkill/{id}")]
        public async Task<ActionResult> DeleteSkill(string id)
        {
            if (_cvFormDBContext.CV == null)
            {
                return NotFound();
            }
            // Convert the string parameter to Guid
            if (!Guid.TryParse(id, out Guid skillIdGuid))
            {
                return BadRequest("Invalid id format");
            }

            // Search for the CVModel with the converted Guid
            var skill = await _cvFormDBContext.Skills.FindAsync(skillIdGuid);
            if (skill == null)
            {
                return NotFound();
            }
            _cvFormDBContext.Skills.Remove(skill);
            await _cvFormDBContext.SaveChangesAsync();

            return Ok();
        }
    }
}
