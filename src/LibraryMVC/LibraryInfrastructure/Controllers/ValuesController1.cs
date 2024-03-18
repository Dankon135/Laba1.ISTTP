using System.Linq;
using Microsoft.AspNetCore.Mvc;
using LibraryDomain.Model;

namespace LibraryInfrastructure.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResearcherWorkController : ControllerBase
    {
        private readonly DblibraryContext _context;

        public ResearcherWorkController(DblibraryContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        [HttpGet("Contributions")]
        public IActionResult GetContributions()
        {
            var contributions = _context.ResearcherWorks
                .GroupBy(rw => rw.Contribution)
                .Select(group => new
                {
                    Contribution = group.Key,
                    Count = group.Count() // Подсчитываем количество каждого вида вклада
                })
                .ToList();

            return Ok(contributions);
        }
    }
}
