using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LibraryModel.Data;
using LibraryModel.Models;
using Microsoft.AspNetCore.Authorization;

namespace Merca_Darius_ExamenNew.Controllers
{
    [Authorize(Policy = "Managers")]
    public class ManagersController : Controller
    {
        private readonly LibraryContext _context;

        public ManagersController(LibraryContext context)
        {
            _context = context;
        }

        // GET: Managers
        public async Task<IActionResult> Index()
        {
              return View(await _context.Managers.ToListAsync());
        }

        // GET: Managers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Managers == null)
            {
                return NotFound();
            }

            var managers = await _context.Managers
                .FirstOrDefaultAsync(m => m.ID == id);
            if (managers == null)
            {
                return NotFound();
            }

            return View(managers);
        }

        // GET: Managers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Managers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Manager,Department")] Managers managers)
        {
            if (ModelState.IsValid)
            {
                _context.Add(managers);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(managers);
        }

        // GET: Managers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Managers == null)
            {
                return NotFound();
            }

            var managers = await _context.Managers.FindAsync(id);
            if (managers == null)
            {
                return NotFound();
            }
            return View(managers);
        }

        // POST: Managers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Manager,Department")] Managers managers)
        {
            if (id != managers.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(managers);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ManagersExists(managers.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(managers);
        }

        // GET: Managers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Managers == null)
            {
                return NotFound();
            }

            var managers = await _context.Managers
                .FirstOrDefaultAsync(m => m.ID == id);
            if (managers == null)
            {
                return NotFound();
            }

            return View(managers);
        }

        // POST: Managers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Managers == null)
            {
                return Problem("Entity set 'LibraryContext.Managers'  is null.");
            }
            var managers = await _context.Managers.FindAsync(id);
            if (managers != null)
            {
                _context.Managers.Remove(managers);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ManagersExists(int id)
        {
          return _context.Managers.Any(e => e.ID == id);
        }
    }
}
