using CVForm.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CVForm.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkExperienceController : Controller
    {
        private readonly CVFormDBContext _cvFormDBContext;
        public WorkExperienceController(CVFormDBContext cvFormDBContext)
        {
            _cvFormDBContext = cvFormDBContext;
        }

        [HttpGet("GetWorkExperience/{cvid}")]
        public async Task<ActionResult<IEnumerable<WorkExperienceModel>>> GetWorkExperience(string cvid)
        {
            if (_cvFormDBContext.WorkExperience == null)
            {
                return NotFound();
            }
            // Convert the string parameter to Guid
            if (!Guid.TryParse(cvid, out Guid cvIdGuid))
            {
                return BadRequest("Invalid id format");
            }
            return await _cvFormDBContext.WorkExperience.Where(work => work.CVID == cvid).ToListAsync();
        }

        [HttpGet("GetWorkExperienceDetail/{id}")]
        public async Task<ActionResult<WorkExperienceModel>> GetWorkExperienceDetail(string id)
        {
            if (_cvFormDBContext.WorkExperience == null)
            {
                return NotFound();
            }
            // Convert the string parameter to Guid
            if (!Guid.TryParse(id, out Guid workIdGuid))
            {
                return BadRequest("Invalid id format");
            }
            // Search for the CVModel with the converted Guid
            var work = await _cvFormDBContext.WorkExperience.FindAsync(workIdGuid);

            if (work == null)
            {
                return NotFound();
            }

            return work;
        }

        [HttpPost("CreateWorkExperience")]
        public async Task<ActionResult<WorkExperienceModel>> PostWorkExperience(WorkExperienceModel work)
        {
            _cvFormDBContext.WorkExperience.Add(work);
            await _cvFormDBContext.SaveChangesAsync();

            return Ok(); ;
        }

        [HttpPut("UpdateWorkExperience/{id}")]
        public async Task<ActionResult<WorkExperienceModel>> UpdateWorkExperience(string id, WorkExperienceModel work)
        {
            if (id != work.WorkExperienceID.ToString())
            {
                return BadRequest();
            }
            _cvFormDBContext.Entry(work).State = EntityState.Modified;
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

        [HttpDelete("DeleteWorkExperience/{id}")]
        public async Task<ActionResult> DeleteWorkExperience(string id)
        {
            if (_cvFormDBContext.CV == null)
            {
                return NotFound();
            }
            // Convert the string parameter to Guid
            if (!Guid.TryParse(id, out Guid workIdGuid))
            {
                return BadRequest("Invalid id format");
            }

            // Search for the CVModel with the converted Guid
            var work = await _cvFormDBContext.WorkExperience.FindAsync(workIdGuid);
            if (work == null)
            {
                return NotFound();
            }
            _cvFormDBContext.WorkExperience.Remove(work);
            await _cvFormDBContext.SaveChangesAsync();

            return Ok();
        }
    }
}
