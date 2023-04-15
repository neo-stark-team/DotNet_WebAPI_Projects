using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using dotnetapp.Models; 


namespace dotnetapp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventApiController : ControllerBase
    {
        private readonly EventApiDbContext _context;

        public EventApiController(EventApiDbContext context)
        {
            _context = context;
        }

        // GET: api/EventApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EventApi>>> GetEventApis()
        {
            return await _context.EventApis.ToListAsync();
        }

        // GET: api/EventApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EventApi>> GetEventApi(int id)
        {
            var EventApi = await _context.EventApis.FindAsync(id);

            if (EventApi == null)
            {
                return NotFound();
            }

            return EventApi;
        }

        // POST: api/EventApi
        [HttpPost]
        public async Task<ActionResult<EventApi>> PostEventApi(EventApi EventApi)
        {
            _context.EventApis.Add(EventApi);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetEventApi), new { id = EventApi.Id }, EventApi);
            // return null;
        }

        // PUT: api/EventApi/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEventApi(int id,EventApi EventApi)
        {
            if (id != EventApi.Id)
            {
                return BadRequest();
            }

            _context.Entry(EventApi).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EventApiExists(id))
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

        // DELETE: api/EventApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEventApi(int id)
        {
            var EventApi = await _context.EventApis.FindAsync(id);
            if (EventApi == null)
            {
                return NotFound();
            }

            _context.EventApis.Remove(EventApi);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EventApiExists(int id)
        {
            return _context.EventApis.Any(e => e.Id == id);
        }
    }
}
