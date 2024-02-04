using CVForm.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CVForm.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CVController  : ControllerBase
    {
        private readonly CVFormDBContext _cvFormDBContext;
        public CVController(CVFormDBContext cvFormDBContext)
        {
            _cvFormDBContext = cvFormDBContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CVModel>>> GetCVs()
        {
            if (_cvFormDBContext.CV == null)
            {
                return NotFound();
            }
            return await _cvFormDBContext.CV.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CVModel>> GetCVDetail(string id)
        {
            if (_cvFormDBContext.CV == null)
            {
                return NotFound();
            }
            // Convert the string parameter to Guid
            if (!Guid.TryParse(id, out Guid cvIdGuid))
            {
                return BadRequest("Invalid id format");
            }
            // Search for the CVModel with the converted Guid
            var cv = await _cvFormDBContext.CV.FindAsync(cvIdGuid);

            if (cv == null)
            {
                return NotFound();
            }

            return cv;
        }

        [HttpPost]
        public async Task<ActionResult<CVModel>> PostCV(CVModel cv)
        {
            _cvFormDBContext.CV.Add(cv);
            await _cvFormDBContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCVDetail), new { id = cv.CVID }, cv);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<CVModel>> UpdateCV(string id, CVModel cv)
        {
            if (id != cv.CVID.ToString())
            {
                return BadRequest();
            }
            _cvFormDBContext.Entry(cv).State = EntityState.Modified;
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

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCV(string id)
        {
            if (_cvFormDBContext.CV == null)
            {
                return NotFound();
            }
            // Convert the string parameter to Guid
            if (!Guid.TryParse(id, out Guid cvIdGuid))
            {
                return BadRequest("Invalid id format");
            }

            // Search for the CVModel with the converted Guid
            var cv = await _cvFormDBContext.CV.FindAsync(cvIdGuid);
            if (cv == null)
            {
                return NotFound();
            }
            _cvFormDBContext.CV.Remove(cv);
            await _cvFormDBContext.SaveChangesAsync();

            return Ok();
        }
    }
}
