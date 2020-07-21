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

        public async Task<HttpTrackerResponse<PagedList<HttpTrackerLogDto>>> QueryAsync(QueryInput input)
        {
            var response = new HttpTrackerResponse<PagedList<HttpTrackerLogDto>>();

            var collection = Database.GetCollection<HttpTrackerLog>(CollectionName);

            var query = collection.AsQueryable();

            if (!string.IsNullOrEmpty(input.Type))
            {
                query = query.Where(x => x.Type.Contains(input.Type));
            }
            if (!string.IsNullOrEmpty(input.Keyword))
            {
                query = query.Where(x => x.Description.Contains(input.Keyword));
            }

            var total = await query.CountAsync();
            var list = await query.OrderByDescending(x => x.CreationTime)
                                  .Skip((input.Page - 1) * input.Limit)
                                  .Take(input.Limit)
                                  .Select(x => new HttpTrackerLogDto
                                  {
                                      Type = x.Type,
                                      Description = x.Description,
                                      Request = new RequestInfo
                                      {
                                          UserAgent = x.UserAgent,
                                          Method = x.Method,
                                          Url = x.Url,
                                          Referrer = x.Referrer,
                                          IpAddress = x.IpAddress,
                                          Milliseconds = x.Milliseconds,
                                          RequestBody = x.RequestBody,
                                          Cookies = x.Cookies,
                                          Headers = x.Headers
                                      },
                                      Response = new ResponseInfo
                                      {
                                          StatusCode = x.StatusCode,
                                          ResponseBody = x.ResponseBody
                                      },
                                      Server = new ServerInfo
                                      {
                                          ServerName = x.ServerName,
                                          PId = x.PId,
                                          Host = x.Host,
                                          Port = x.Port
                                      },
                                      Exception = new ExceptionInfo
                                      {
                                          ExceptionType = x.ExceptionType,
                                          Message = x.Message,
                                          StackTrace = x.StackTrace
                                      },
                                      CreationTime = x.CreationTime
                                  }).ToListAsync();

            response.IsSuccess(new PagedList<HttpTrackerLogDto>(total, list));
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