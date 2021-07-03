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
    public class LessonsController : ControllerBase
    {
        private readonly authApiContext _context;
        private readonly IDataRepository<Lesson> _repo;

        public LessonsController(authApiContext context, IDataRepository<Lesson> repo)
        {
            _context = context;
            _repo = repo;
        }

        // GET: api/Lessons
        [HttpGet]
        public IEnumerable<Lesson> GetLessons()
        {
            var query = _context.Lesson.
                Include(l => l.Section)
                .ThenInclude(it => it.Course)
                .ToList();
            return query;
        }

        // GET: api/Lessons/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetLesson([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var course = await _context.Lesson.FindAsync(id);

            if (course == null)
            {
                return NotFound();
            }

            return Ok(course);
        }

        // PUT: api/Lessons/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLesson([FromRoute] Guid id, [FromBody] Lesson lesson)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != lesson.id)
            {
                return BadRequest();
            }

            if (id != lesson.id)
            {
                return BadRequest();
            }

            _context.Entry(lesson).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LessonExists(id))
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

        // POST: api/Lessons
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Lesson>> PostUser(Lesson lesson)
        {
            _context.Lesson.Add(lesson);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLesson", new { id = lesson.id }, lesson);
        }

        // DELETE: api/Lessons/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Lesson>> DeleteLesson(Guid id)
        {
            var lesson = await _context.Lesson.FindAsync(id);
            if (lesson == null)
            {
                return NotFound();
            }

            _context.Lesson.Remove(lesson);
            await _context.SaveChangesAsync();

            return lesson;
        }
        private bool LessonExists(Guid id)
        {
            return _context.Lesson.Any(e => e.id == id);
        }
    }
}
