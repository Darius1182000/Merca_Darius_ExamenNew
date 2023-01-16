using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LibraryModel.Data;
using LibraryModel.Models;

namespace LibraryWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttendancesController : ControllerBase
    {
        private readonly LibraryContext _context;

        public AttendancesController(LibraryContext context)
        {
            _context = context;
        }

        // GET: api/Attendances
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Attendances>>> GetAttendances()
        {
            return await _context.Attendances.ToListAsync();
        }

        // GET: api/Attendances/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Attendances>> GetAttendances(int id)
        {
            var attendances = await _context.Attendances.FindAsync(id);

            if (attendances == null)
            {
                return NotFound();
            }

            return attendances;
        }

        // PUT: api/Attendances/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAttendances(int id, Attendances attendances)
        {
            if (id != attendances.ID)
            {
                return BadRequest();
            }

            _context.Entry(attendances).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AttendancesExists(id))
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

        // POST: api/Attendances
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Attendances>> PostAttendances(Attendances attendances)
        {
            _context.Attendances.Add(attendances);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAttendances", new { id = attendances.ID }, attendances);
        }

        // DELETE: api/Attendances/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAttendances(int id)
        {
            var attendances = await _context.Attendances.FindAsync(id);
            if (attendances == null)
            {
                return NotFound();
            }

            _context.Attendances.Remove(attendances);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AttendancesExists(int id)
        {
            return _context.Attendances.Any(e => e.ID == id);
        }
    }
}
