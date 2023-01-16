using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LibraryModel.Data;
using LibraryModel.Models;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Authorization;

namespace Merca_Darius_ExamenNew.Controllers
{
    [Authorize(Policy = "Managers")]
    public class AttendancesController : Controller
    {
        private readonly LibraryContext _context;

        public AttendancesController(LibraryContext context)
        {
            _context = context;
        }

        // GET: Attendances
        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? pageNumber)
        {
            ViewData["CurrentSOrt"] = sortOrder;
            ViewData["EmployeeNameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "employee_desc" : "";
            ViewData["AbReasonSortParm"] = sortOrder == "AbsenceReason" ? "abreason_desc" : "AbsenceReason";
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";


            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewData["CurrentFIlter"] = searchString;

            var attendance = from b in _context.Attendances
                            select b;
            if (!String.IsNullOrEmpty(searchString))
            {
                attendance = attendance.Where(s => s.EmployeeName.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "employee_desc":
                    attendance = attendance.OrderByDescending(b => b.EmployeeName);
                    break;
                case "abreason_desc":
                    attendance = attendance.OrderByDescending(b => b.AbsenceReason.Length);
                    break;
                case "AbsenceReason":
                    attendance = attendance.OrderBy(b => b.AbsenceReason.Length);
                    break;
                case "date_desc":
                    attendance = attendance.OrderByDescending(b => b.Date);
                    break;
                case "Date":
                    attendance = attendance.OrderBy(b => b.Date);
                    break;
                default:
                    attendance = attendance.OrderBy(b => b.EmployeeName);
                    break;
            }
            int pageSize = 2;
            return View(await PaginatedList<Attendances>.CreateAsync(attendance.AsNoTracking(), pageNumber ??
           1, pageSize));
        }
        

        // GET: Attendances/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Attendances == null)
            {
                return NotFound();
            }

            var attendances = await _context.Attendances
                .FirstOrDefaultAsync(m => m.ID == id);
            if (attendances == null)
            {
                return NotFound();
            }

            return View(attendances);
        }

        // GET: Attendances/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Attendances/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,EmployeeName,Date,InTime,OutTime,AbsenceReason")] Attendances attendances)
        {
            if (ModelState.IsValid)
            {
                _context.Add(attendances);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(attendances);
        }

        // GET: Attendances/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Attendances == null)
            {
                return NotFound();
            }

            var attendances = await _context.Attendances.FindAsync(id);
            if (attendances == null)
            {
                return NotFound();
            }
            return View(attendances);
        }

        // POST: Attendances/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,EmployeeName,Date,InTime,OutTime,AbsenceReason")] Attendances attendances)
        {
            if (id != attendances.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(attendances);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AttendancesExists(attendances.ID))
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
            return View(attendances);
        }

        // GET: Attendances/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Attendances == null)
            {
                return NotFound();
            }

            var attendances = await _context.Attendances
                .FirstOrDefaultAsync(m => m.ID == id);
            if (attendances == null)
            {
                return NotFound();
            }

            return View(attendances);
        }

        // POST: Attendances/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Attendances == null)
            {
                return Problem("Entity set 'LibraryContext.Attendances'  is null.");
            }
            var attendances = await _context.Attendances.FindAsync(id);
            if (attendances != null)
            {
                _context.Attendances.Remove(attendances);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AttendancesExists(int id)
        {
          return _context.Attendances.Any(e => e.ID == id);
        }
    }
}
