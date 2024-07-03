using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using MovieSearchAPI.Models;

namespace MovieSearchAPI.Data
{
    public interface ISearch
    {
        Task AddSearchRequestAsync(Request searchRequest);
        Task<List<Request>> GetAllSearchRequestsAsync(int page, int pageSize);
        Task<Request> GetSearchRequestByIdAsync(string id);
        Task<List<Request>> GetSearchRequestsByDateRangeAsync(DateTime start, DateTime end);
        Task<DailyUsage> GetDailyUsageReportAsync(DateTime date);
        Task DeleteSearchRequestAsync(string id);
        Task<List<Request>> SearchRequestsAsync(string searchTerm);

    }
}
