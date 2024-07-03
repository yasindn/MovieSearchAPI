using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using MovieSearchAPI.Models;
using System.Linq;

namespace MovieSearchAPI.Data
{
    public class Search : ISearch
    {
        private readonly IMongoCollection<Request> _searchRequests;

        public Search(IMongoDbContext context, IDatabaseSettings settings)
        {
            _searchRequests = context.GetCollection<Request>(settings.CollectionName);
        }

        public async Task AddSearchRequestAsync(Request searchRequest)
        {
            await _searchRequests.InsertOneAsync(searchRequest);
        }

        public async Task<List<Request>> GetAllSearchRequestsAsync(int page, int pageSize)
        {
            return await _searchRequests.Find(_ => true)
                    .Skip((page - 1) * pageSize)
                    .Limit(pageSize)
                    .ToListAsync();
        }

        public async Task<Request> GetSearchRequestByIdAsync(string id)
        {
            return await _searchRequests.Find(r => r.Id == id).FirstOrDefaultAsync();
        }

        public async Task<List<Request>> GetSearchRequestsByDateRangeAsync(DateTime start, DateTime end)
        {
            return await _searchRequests.Find(r => r.Timestamp >= start && r.Timestamp <= end).ToListAsync();
        }

        public async Task<DailyUsage> GetDailyUsageReportAsync(DateTime date)
        {
            var startOfDay = date.Date;
            var endOfDay = startOfDay.AddDays(1).AddTicks(-1);

            var result = await _searchRequests.Aggregate()
                .Match(r => r.Timestamp >= startOfDay && r.Timestamp <= endOfDay)
                .Group(r => r.Timestamp.Date, g => new DailyUsage { Date = g.Key, Count = g.Count() })
                .FirstOrDefaultAsync();

            return result ?? new DailyUsage
            {
                Date = startOfDay,
                Count = 0
            };
        }

        public async Task DeleteSearchRequestAsync(string id)
        {
            await _searchRequests.DeleteOneAsync(r => r.Id == id);
        }

        public async Task<List<Request>> SearchRequestsAsync(string searchTerm)
        {
            return await _searchRequests.Find(r =>
                r.SearchToken.Contains(searchTerm) ||
                r.ImdbID.Contains(searchTerm) ||
                r.IpAddress.Contains(searchTerm)
            ).ToListAsync();
        }
    }
}
