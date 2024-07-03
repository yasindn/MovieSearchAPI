using Microsoft.AspNetCore.Mvc;
using MovieSearchAPI.Data;
using MovieSearchAPI.Models;
using MovieSearchAPI.Services;
using System;
using System.Threading.Tasks;

namespace MovieSearchAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SearchController : ControllerBase
    {
        private readonly IMovieService _movieService;
        private readonly ISearch _search;

        public SearchController(IMovieService movieService, ISearch search)
        {
            _movieService = movieService;
            _search = search;
        }

        [HttpGet]
        public async Task<IActionResult> SearchMovie(string title)
        {
            var startTime = DateTime.UtcNow;
            var movie = await _movieService.SearchMovieAsync(title);
            var endTime = DateTime.UtcNow;

            var searchRequest = new Request
            {
                SearchToken = title,
                ImdbID = movie.ImdbID,
                ProcessingTimeMs = (long)(endTime - startTime).TotalMilliseconds,
                Timestamp = startTime,
                IpAddress = HttpContext.Connection.RemoteIpAddress.ToString()
            };

            await _search.AddSearchRequestAsync(searchRequest);

            return Ok(movie);
        }
    }
}
