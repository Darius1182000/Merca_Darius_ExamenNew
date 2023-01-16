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
    public class ManagersController : ControllerBase
    {
        private readonly LibraryContext _context;

        public ManagersController(LibraryContext context)
        {
            _context = context;
        }

        // GET: api/Managers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Managers>>> GetManagers()
        {
            return await _context.Managers.ToListAsync();
        }

        // GET: api/Managers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Managers>> GetManagers(int id)
        {
            var managers = await _context.Managers.FindAsync(id);

            if (managers == null)
            {
                return NotFound();
            }

            return managers;
        }

        // PUT: api/Managers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutManagers(int id, Managers managers)
        {
            if (id != managers.ID)
            {
                return BadRequest();
            }

            _context.Entry(managers).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ManagersExists(id))
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

        // POST: api/Managers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Managers>> PostManagers(Managers managers)
        {
            _context.Managers.Add(managers);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetManagers", new { id = managers.ID }, managers);
        }

        // DELETE: api/Managers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteManagers(int id)
        {
            var managers = await _context.Managers.FindAsync(id);
            if (managers == null)
            {
                return NotFound();
            }

            _context.Managers.Remove(managers);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ManagersExists(int id)
        {
            return _context.Managers.Any(e => e.ID == id);
        }
    }
}
