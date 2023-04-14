using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using dotnetapp.Models; 

namespace dotnetapp.Controllers 
{
    [ApiController]
    [Route("[controller]")]
    public class FitnessTrackerController : ControllerBase
    {
        private readonly FitnessTrackerDbContext _context;

        public FitnessTrackerController(FitnessTrackerDbContext context)
        {
            _context = context;
        }

        // GET: api/FitnessTracker
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FitnessTracker>>> GetFitnessTrackers()
        {
            return await _context.FitnessTrackers.ToListAsync();
        }

        // GET: api/FitnessTracker/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FitnessTracker>> GetFitnessTracker(int id)
        {
            var FitnessTracker = await _context.FitnessTrackers.FindAsync(id);

            if (FitnessTracker == null)
            {
                return NotFound();
            }

            return FitnessTracker;
        }

        // POST: api/FitnessTracker
        [HttpPost]
        public async Task<ActionResult<FitnessTracker>> PostFitnessTracker(FitnessTracker FitnessTracker)
        {
            _context.FitnessTrackers.Add(FitnessTracker);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetFitnessTracker), new { id = FitnessTracker.Id }, FitnessTracker);
        }
// PUT: api/FitnessTracker/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFitnessTracker(int id, FitnessTracker FitnessTracker)
        {
            if (id != FitnessTracker.Id)
            {
                return BadRequest();
            }

            _context.Entry(FitnessTracker).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FitnessTrackerExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }
             // DELETE: api/FitnessTracker/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFitnessTracker(int id)
        {
            var FitnessTracker = await _context.FitnessTrackers.FindAsync(id);
            if (FitnessTracker == null)
            {
                return NotFound();
            }

            _context.FitnessTrackers.Remove(FitnessTracker);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FitnessTrackerExists(int id)
        {
            return _context.FitnessTrackers.Any(e => e.Id == id);
        }
    }
}
