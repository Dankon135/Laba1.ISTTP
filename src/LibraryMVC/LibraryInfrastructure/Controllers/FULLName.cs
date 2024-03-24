using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LibraryDomain.Model;
using LibraryInfrastructure;

namespace LibraryInfrastructure.Controllers
{
    public class FULLName : Controller
    {
        private readonly DblibraryContext _context;

        public FULLName(DblibraryContext context)
        {
            _context = context;
        }

        // GET: FULLName
        public async Task<IActionResult> Index()
        {
            var dblibraryContext = _context.Personnel.Include(p => p.Departament).Include(p => p.Laboratory).Include(p => p.Position);
            return View(await dblibraryContext.ToListAsync());
        }

        // GET: FULLName/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var personnel = await _context.Personnel
                .Include(p => p.Departament)
                .Include(p => p.Laboratory)
                .Include(p => p.Position)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (personnel == null)
            {
                return NotFound();
            }

            return View(personnel);
        }

        // GET: FULLName/Create
        public IActionResult Create()
        {
            ViewData["DepartamentId"] = new SelectList(_context.Departaments, "Id", "Name");
            ViewData["LaboratoryId"] = new SelectList(_context.Laboratories, "Id", "Name");
            ViewData["PositionId"] = new SelectList(_context.Positions, "Id", "Title");
            return View();
        }

        // POST: FULLName/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FullName,DepartamentId,LaboratoryId,PositionId,PositionStart,PositionEnd,PersonnelId")] Personnel personnel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(personnel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DepartamentId"] = new SelectList(_context.Departaments, "Id", "Name", personnel.DepartamentId);
            ViewData["LaboratoryId"] = new SelectList(_context.Laboratories, "Id", "Name", personnel.LaboratoryId);
            ViewData["PositionId"] = new SelectList(_context.Positions, "Id", "Title", personnel.PositionId);
            return View(personnel);
        }

        // GET: FULLName/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var personnel = await _context.Personnel.FindAsync(id);
            if (personnel == null)
            {
                return NotFound();
            }
            ViewData["DepartamentId"] = new SelectList(_context.Departaments, "Id", "Name", personnel.DepartamentId);
            ViewData["LaboratoryId"] = new SelectList(_context.Laboratories, "Id", "Name", personnel.LaboratoryId);
            ViewData["PositionId"] = new SelectList(_context.Positions, "Id", "Title", personnel.PositionId);
            return View(personnel);
        }

        // POST: FULLName/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FullName,DepartamentId,LaboratoryId,PositionId,PositionStart,PositionEnd,PersonnelId")] Personnel personnel)
        {
            if (id != personnel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(personnel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonnelExists(personnel.Id))
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
            ViewData["DepartamentId"] = new SelectList(_context.Departaments, "Id", "Name", personnel.DepartamentId);
            ViewData["LaboratoryId"] = new SelectList(_context.Laboratories, "Id", "Name", personnel.LaboratoryId);
            ViewData["PositionId"] = new SelectList(_context.Positions, "Id", "Title", personnel.PositionId);
            return View(personnel);
        }

        // GET: FULLName/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var personnel = await _context.Personnel
                .Include(p => p.Departament)
                .Include(p => p.Laboratory)
                .Include(p => p.Position)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (personnel == null)
            {
                return NotFound();
            }

            return View(personnel);
        }

        // POST: FULLName/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var personnel = await _context.Personnel.FindAsync(id);
            if (personnel != null)
            {
                _context.Personnel.Remove(personnel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PersonnelExists(int id)
        {
            return _context.Personnel.Any(e => e.Id == id);
        }
    }
}
