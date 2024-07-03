using MovieSearchAPI.Models;
using System.Threading.Tasks;

namespace MovieSearchAPI.Services
{
    public interface IMovieService
    {
        Task<Movie> SearchMovieAsync(string title);
    }
}
