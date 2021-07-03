using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using authApi.Data;
using authApi.Models;

namespace authApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WatchLogController : ControllerBase
    {
        private readonly authApiContext _context;
        private readonly IDataRepository<WatchLog> _repo;
        public WatchLogController(authApiContext context, IDataRepository<WatchLog> repo)
        {
            _context = context;
            _repo = repo;
        }


        // GET: api/WatchLogs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WatchLog>>> GetWatchLog()
        {
            return await _context.WatchLog.ToListAsync();
        }

        // GET: api/WatchLogs/5
        [HttpGet("{id}")]
        public IEnumerable<WatchLog> GetWatchLogs()
        {
            return _context.WatchLog.OrderByDescending(p => p.id);
        }

        // PUT: api/WatchLogs/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWatchLog([FromRoute] Guid id, [FromBody] WatchLog watchLog)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != watchLog.id)
            {
                return BadRequest();
            }

            if (id != watchLog.id)
            {
                return BadRequest();
            }

            _context.Entry(watchLog).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WatchLogExists(id))
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


        // POST: api/WatchLogs
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<IActionResult> PostWatchLog([FromBody] WatchLog watchLog)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _repo.Add(watchLog);
            var save = await _repo.SaveAsync(watchLog);

            return CreatedAtAction("GetWatchLog", new { id = watchLog.id }, watchLog);
        }

        // DELETE: api/WatchLogs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWatchLog([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var watchLog = await _context.WatchLog.FindAsync(id);
            if (watchLog == null)
            {
                return NotFound();
            }

            _repo.Delete(watchLog);
            var save = await _repo.SaveAsync(watchLog);

            return Ok(watchLog);
        }

        private bool WatchLogExists(Guid id)
        {
            return _context.WatchLog.Any(e => e.id == id);
        }
    }
}
