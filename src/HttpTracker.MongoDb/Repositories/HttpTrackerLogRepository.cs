using HttpTracker.Domain;
using HttpTracker.Dto;
using HttpTracker.Dto.Params;
using HttpTracker.Options;
using HttpTracker.Response;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Threading.Tasks;

namespace HttpTracker.Repositories
{
    public class HttpTrackerLogRepository : MongoDbRepositoryBase, IHttpTrackerLogRepository
    {
        public HttpTrackerLogRepository(IMongoDbProvider mongoDbProvider, HttpTrackerMongoDbOptions options, string name) : base(mongoDbProvider)
        {
            var collectionName = $"{DbConsts.CollectionNames.HttpTrackerLog}_{name}";
            if (!string.IsNullOrEmpty(options.CollectionPrefix))
            {
                collectionName = $"{options.CollectionPrefix}_{collectionName}";
            }

            CollectionName = collectionName;
        }

        protected override string CollectionName { get; }

        public async Task<HttpTrackerResponse> InitAsync()
        {
            return await Task.FromResult(new HttpTrackerResponse());
        }

        public async Task<HttpTrackerResponse<PagedList<HttpTrackerLogDto>>> QueryAsync(SearchInput input)
        {
            var response = new HttpTrackerResponse<PagedList<HttpTrackerLogDto>>();

            var collection = Database.GetCollection<HttpTrackerLog>(CollectionName);

            //var query = collection.AsQueryable();

            //if (string.IsNullOrEmpty(type))
            //{
            //    query = query.Where(x => x.Type.Contains(type));
            //}
            //if (string.IsNullOrEmpty(keyword))
            //{
            //    query = query.Where(x => x.Description.Contains(keyword));
            //}

            //var total = await query.CountAsync();
            //var list = await query.OrderByDescending(x => x.CreationTime).Take((page - 1) * limit).Skip(limit).ToListAsync();

            //response.IsSuccess(new PagedList<HttpTrackerLog>(total, list));
            return response;
        }

        public async Task<HttpTrackerResponse> InsertAsync(HttpTrackerLog httpTrackerLog)
        {
            var response = new HttpTrackerResponse();
            if (httpTrackerLog == null)
            {
                response.IsFailed("HttpTrackerLog is null");
                return response;
            }

            try
            {
                var collection = Database.GetCollection<HttpTrackerLog>(CollectionName);

                await collection.InsertOneAsync(httpTrackerLog);
            }
            catch (Exception ex)
            {
                response.IsFailed(ex);
            }

            return response;
        }
    }
}