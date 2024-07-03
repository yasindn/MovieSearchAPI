using Microsoft.AspNetCore.Mvc;
using MovieSearchAPI.Data;
using System;
using System.Globalization;
using System.Threading.Tasks;

namespace MovieSearchAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly ISearch _search;

        public AdminController(ISearch search)
        {
            _search = search;
        }

        [HttpGet("requests")]
        public async Task<IActionResult> GetAllRequests(int page = 1, int pageSize = 50)
        {
            var requests = await _search.GetAllSearchRequestsAsync(page, pageSize);
            return Ok(requests);
        }

        [HttpGet("requests/{id}")]
        public async Task<IActionResult> GetRequest(string id)
        {
            var request = await _search.GetSearchRequestByIdAsync(id);
            if (request == null)
                return NotFound();
            return Ok(request);
        }

        [HttpGet("requests/date-range")]
        public async Task<IActionResult> GetRequestsByDateRange(DateTime start, DateTime end)
        {
            var requests = await _search.GetSearchRequestsByDateRangeAsync(start, end);
            return Ok(requests);
        }

        [HttpGet("usage-report")]
        public async Task<IActionResult> GetDailyUsageReport(string date)
        {
            if (!DateTime.TryParseExact(date, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime reportDate))
            {
                return BadRequest("Proper date format (DD-MM-YYYY)!");
            }

            var report = await _search.GetDailyUsageReportAsync(reportDate);

            return Ok(report);
        }

        [HttpDelete("requests/{id}")]
        public async Task<IActionResult> DeleteRequest(string id)
        {
            await _search.DeleteSearchRequestAsync(id);
            return NoContent();
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchRequests(string term)
        {
            var results = await _search.SearchRequestsAsync(term);
            return Ok(results);
        }
    }
}
