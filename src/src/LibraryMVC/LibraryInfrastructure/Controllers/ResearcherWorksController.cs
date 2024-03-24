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
    public class ResearcherWorksController : Controller
    {
        private readonly DblibraryContext _context;

        public ResearcherWorksController(DblibraryContext context)
        {
            _context = context;
        }

        // GET: ResearcherWorks
        public async Task<IActionResult> Index()
        {
            return View(await _context.ResearcherWorks.ToListAsync());
        }

        // GET: ResearcherWorks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var researcherWork = await _context.ResearcherWorks
                .FirstOrDefaultAsync(m => m.Id == id);
            if (researcherWork == null)
            {
                return NotFound();
            }

            return View(researcherWork);
        }

        // GET: ResearcherWorks/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ResearcherWorks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ScientificWorkId,Contribution,CreatedAt,Title")] ResearcherWork researcherWork)
        {
            if (ModelState.IsValid)
            {
                _context.Add(researcherWork);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(researcherWork);
        }

        // GET: ResearcherWorks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var researcherWork = await _context.ResearcherWorks.FindAsync(id);
            if (researcherWork == null)
            {
                return NotFound();
            }
            return View(researcherWork);
        }

        // POST: ResearcherWorks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ScientificWorkId,Contribution,CreatedAt,Title")] ResearcherWork researcherWork)
        {
            if (id != researcherWork.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(researcherWork);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ResearcherWorkExists(researcherWork.Id))
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
            return View(researcherWork);
        }

        // GET: ResearcherWorks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var researcherWork = await _context.ResearcherWorks
                .FirstOrDefaultAsync(m => m.Id == id);
            if (researcherWork == null)
            {
                return NotFound();
            }

            return View(researcherWork);
        }

        // POST: ResearcherWorks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var researcherWork = await _context.ResearcherWorks.FindAsync(id);
            if (researcherWork != null)
            {
                _context.ResearcherWorks.Remove(researcherWork);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ResearcherWorkExists(int id)
        {
            return _context.ResearcherWorks.Any(e => e.Id == id);
        }
    }
}
