using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using LibraryDomain.Model;
using Microsoft.EntityFrameworkCore;

namespace LibraryInfrastructure.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChartController : ControllerBase
    {
        private readonly DblibraryContext _context;

        public ChartController(DblibraryContext context)
        {
            _context = context;
        }

        [HttpGet("ResearcherWorkTimeData")]
        public IActionResult GetResearcherWorkTimeData()
        {
            var data = _context.ResearcherWorks
                .GroupBy(rw => rw.CreatedAt)
                .Select(group => new
                {
                    Date = group.Key,
                    Count = group.Count()
                })
                .OrderBy(x => x.Date)
                .ToList();

            return Ok(data);
        }
    }
}