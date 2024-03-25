using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LibraryDomain.Model;
using LibraryInfrastructure;
using ClosedXML.Excel;

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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Import(IFormFile fileExcel)
        {
            if (ModelState.IsValid)
            {
                if (fileExcel != null)
                {
                    using (var stream = new MemoryStream())
                    {
                        await fileExcel.CopyToAsync(stream);
                        using (var workBook = new XLWorkbook(stream))
                        {
                            var worksheet = workBook.Worksheets.First();
                            foreach (var row in worksheet.RowsUsed().Skip(1)) // Пропускаем заголовок
                            {
                                var researcherWork = new ResearcherWork
                                {
                                    ScientificWorkId = int.Parse(row.Cell(1).Value.ToString()),
                                    Contribution = row.Cell(2).Value.ToString(),
                                    CreatedAt = DateOnly.FromDateTime(row.Cell(3).GetDateTime()),
                                    Title = row.Cell(4).Value.ToString()
                                };

                                _context.ResearcherWorks.Add(researcherWork);
                            }
                        }
                    }

                    await _context.SaveChangesAsync();
                }

                return RedirectToAction(nameof(Index));
            }

            return View();
        }
        public async Task<IActionResult> Export()
        {
            using (var workbook = new XLWorkbook())
            {
                var researcherWorks = await _context.ResearcherWorks.ToListAsync();
                var worksheet = workbook.Worksheets.Add("ResearcherWorks");

                // Заголовки столбцов
                worksheet.Cell("A1").Value = "Scientific Work ID";
                worksheet.Cell("B1").Value = "Contribution";
                worksheet.Cell("C1").Value = "Created At";
                worksheet.Cell("D1").Value = "Title";

                worksheet.Row(1).Style.Font.Bold = true;

                int row = 2;
                foreach (var work in researcherWorks)
                {
                    worksheet.Cell(row, 1).Value = work.ScientificWorkId;
                    worksheet.Cell(row, 2).Value = work.Contribution;
                    worksheet.Cell(row, 3).Value = work.CreatedAt.ToString();
                    worksheet.Cell(row, 4).Value = work.Title;
                    row++;
                }

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();

                    return File(content,
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        "ResearcherWorks.xlsx");
                }
            }
        }


    }

}
