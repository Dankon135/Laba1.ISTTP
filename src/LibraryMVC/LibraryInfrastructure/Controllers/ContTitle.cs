using System.Linq;
using Microsoft.AspNetCore.Mvc;
using LibraryDomain.Model;

namespace LibraryInfrastructure.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TitleChartController : ControllerBase
    {
        private readonly DblibraryContext _context;

        public TitleChartController(DblibraryContext context)
        {
            _context = context;
        }

        [HttpGet("ResearcherWorkTitleData")]
        public IActionResult GetResearcherWorkTitleData()
        {
            var data = _context.ResearcherWorks
                .GroupBy(rw => rw.Title)
                .Select(group => new
                {
                    Title = group.Key,
                    Count = group.Count()
                })
                .OrderByDescending(x => x.Count) // Сортируем по количеству для показа наиболее популярных заголовков
                .ToList();

            return Ok(data);
        }
    }
}
