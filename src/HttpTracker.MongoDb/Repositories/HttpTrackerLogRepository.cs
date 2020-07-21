﻿using HttpTracker.Domain;
using HttpTracker.Options;
using HttpTracker.Response;
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

        public Task<HttpTrackerResponse<PagedList<HttpTrackerLog>>> SearchAsync(string type, string keyword, int page, int limit)
        {
            throw new NotImplementedException();
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