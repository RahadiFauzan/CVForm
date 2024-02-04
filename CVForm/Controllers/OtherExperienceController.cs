using CVForm.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CVForm.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OtherExperienceController : Controller
    {
        private readonly CVFormDBContext _cvFormDBContext;
        public OtherExperienceController(CVFormDBContext cvFormDBContext)
        {
            _cvFormDBContext = cvFormDBContext;
        }

        [HttpGet("GetOtherExperience/{cvid}")]
        public async Task<ActionResult<IEnumerable<OtherExperienceModel>>> GetOtherExperience(string cvid)
        {
            if (_cvFormDBContext.OtherExperience == null)
            {
                return NotFound();
            }
            // Convert the string parameter to Guid
            if (!Guid.TryParse(cvid, out Guid cvIdGuid))
            {
                return BadRequest("Invalid id format");
            }
            return await _cvFormDBContext.OtherExperience.Where(other => other.CVID == cvid).ToListAsync();
        }

        [HttpGet("GetOtherExperienceDetail/{id}")]
        public async Task<ActionResult<OtherExperienceModel>> GetOtherExperienceDetail(string id)
        {
            if (_cvFormDBContext.OtherExperience == null)
            {
                return NotFound();
            }
            // Convert the string parameter to Guid
            if (!Guid.TryParse(id, out Guid otherIdGuid))
            {
                return BadRequest("Invalid id format");
            }
            // Search for the CVModel with the converted Guid
            var other = await _cvFormDBContext.OtherExperience.FindAsync(otherIdGuid);

            if (other == null)
            {
                return NotFound();
            }

            return other;
        }

        [HttpPost("CreateOtherExperience")]
        public async Task<ActionResult<OtherExperienceModel>> PostOtherExperience(OtherExperienceModel other)
        {
            _cvFormDBContext.OtherExperience.Add(other);
            await _cvFormDBContext.SaveChangesAsync();

            return Ok(); ;
        }

        [HttpPut("UpdateOtherExperience/{id}")]
        public async Task<ActionResult<OtherExperienceModel>> UpdateOtherExperience(string id, OtherExperienceModel other)
        {
            if (id != other.OtherExperienceID.ToString())
            {
                return BadRequest();
            }
            _cvFormDBContext.Entry(other).State = EntityState.Modified;
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

        [HttpDelete("DeleteOtherExperience/{id}")]
        public async Task<ActionResult> DeleteOtherExperience(string id)
        {
            if (_cvFormDBContext.CV == null)
            {
                return NotFound();
            }
            // Convert the string parameter to Guid
            if (!Guid.TryParse(id, out Guid otherIdGuid))
            {
                return BadRequest("Invalid id format");
            }

            // Search for the CVModel with the converted Guid
            var other = await _cvFormDBContext.OtherExperience.FindAsync(otherIdGuid);
            if (other == null)
            {
                return NotFound();
            }
            _cvFormDBContext.OtherExperience.Remove(other);
            await _cvFormDBContext.SaveChangesAsync();

            return Ok();
        }
    }
}
