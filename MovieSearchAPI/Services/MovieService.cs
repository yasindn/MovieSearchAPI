using Microsoft.Extensions.Configuration;
using MovieSearchAPI.Models;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;


namespace MovieSearchAPI.Services
{
    public class MovieService : IMovieService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public MovieService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClient = httpClientFactory.CreateClient();
            _apiKey = configuration["OMDbApiKey"];
        }

        public async Task<Movie> SearchMovieAsync(string title)
        {
            var response = await _httpClient.GetAsync($"http://www.omdbapi.com/?apikey={_apiKey}&t={Uri.EscapeDataString(title)}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Movie>(content);
        }
    }
}
