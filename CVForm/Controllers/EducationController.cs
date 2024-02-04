using CVForm.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CVForm.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EducationController : Controller
    {
        private readonly CVFormDBContext _cvFormDBContext;
        public EducationController(CVFormDBContext cvFormDBContext)
        {
            _cvFormDBContext = cvFormDBContext;
        }

        [HttpGet("GetEducation/{cvid}")]
        public async Task<ActionResult<IEnumerable<EducationModel>>> GetEducation(string cvid)
        {
            if (_cvFormDBContext.Education == null)
            {
                return NotFound();
            }
            // Convert the string parameter to Guid
            if (!Guid.TryParse(cvid, out Guid cvIdGuid))
            {
                return BadRequest("Invalid id format");
            }
            return await _cvFormDBContext.Education.Where(education => education.CVID == cvid).ToListAsync();
        }

        [HttpGet("GetEducationDetail/{id}")]
        public async Task<ActionResult<EducationModel>> GetEducationDetail(string id)
        {
            if (_cvFormDBContext.Education == null)
            {
                return NotFound();
            }
            // Convert the string parameter to Guid
            if (!Guid.TryParse(id, out Guid educationIdGuid))
            {
                return BadRequest("Invalid id format");
            }
            // Search for the CVModel with the converted Guid
            var education = await _cvFormDBContext.Education.FindAsync(educationIdGuid);

            if (education == null)
            {
                return NotFound();
            }

            return education;
        }

        [HttpPost("CreateEducation")]
        public async Task<ActionResult<EducationModel>> PostEducation(EducationModel education)
        {
            _cvFormDBContext.Education.Add(education);
            await _cvFormDBContext.SaveChangesAsync();

            return Ok(); ;
        }

        [HttpPut("UpdateEducation/{id}")]
        public async Task<ActionResult<EducationModel>> UpdateEducation(string id, EducationModel education)
        {
            if (id != education.EducationID.ToString())
            {
                return BadRequest();
            }
            _cvFormDBContext.Entry(education).State = EntityState.Modified;
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

        [HttpDelete("DeleteEducation/{id}")]
        public async Task<ActionResult> DeleteEducation(string id)
        {
            if (_cvFormDBContext.CV == null)
            {
                return NotFound();
            }
            // Convert the string parameter to Guid
            if (!Guid.TryParse(id, out Guid educationIdGuid))
            {
                return BadRequest("Invalid id format");
            }

            // Search for the CVModel with the converted Guid
            var education = await _cvFormDBContext.Education.FindAsync(educationIdGuid);
            if (education == null)
            {
                return NotFound();
            }
            _cvFormDBContext.Education.Remove(education);
            await _cvFormDBContext.SaveChangesAsync();

            return Ok();
        }
    }
}
