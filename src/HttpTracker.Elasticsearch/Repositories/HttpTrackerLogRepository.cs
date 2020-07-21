using HttpTracker.Domain;
using HttpTracker.Dto;
using HttpTracker.Dto.Params;
using HttpTracker.Options;
using HttpTracker.Response;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HttpTracker.Repositories
{
    public class HttpTrackerLogRepository : ElasticsearchRepositoryBase, IHttpTrackerLogRepository
    {
        public HttpTrackerLogRepository(IElasticsearchProvider elasticsearchProvider, HttpTrackerElasticsearchOptions options, string name) : base(elasticsearchProvider)
        {
            var indexName = $"{IndexConsts.IndexNames.HttpTrackerLog}_{name}";
            if (!string.IsNullOrEmpty(options.IndexPrefix))
            {
                indexName = $"{options.IndexPrefix}_{indexName}";
            }

            IndexName = indexName;
        }

        protected override string IndexName { get; }

        public async Task<HttpTrackerResponse> InitAsync()
        {
            return await Task.FromResult(new HttpTrackerResponse());
        }

        public async Task<HttpTrackerResponse<PagedList<HttpTrackerLogDto>>> QueryAsync(QueryInput input)
        {
            var response = new HttpTrackerResponse<PagedList<HttpTrackerLogDto>>();

            var query = new List<QueryContainer>();
            if (!string.IsNullOrEmpty(input.Type))
            {
                query.Add(new MatchPhraseQuery
                {
                    Field = new Field("Type"),
                    Query = input.Type
                });
            }
            if (!string.IsNullOrEmpty(input.Keyword))
            {
                query.Add(new MatchPhraseQuery
                {
                    Field = new Field("Description"),
                    Query = input.Keyword
                });
            }

            var searchResponse = await Client.SearchAsync<HttpTrackerLog>(x => x.Index(IndexName)
                                             .Query(x => x.Bool(x => x.Should(query.ToArray())))
                                             .From((input.Page - 1) * input.Limit)
                                             .Take(input.Limit)
                                             .Sort(s => s.Descending(x => x.CreationTime)));

            var total = Convert.ToInt32(searchResponse.Total);
            var list = searchResponse.Documents
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
                                     }).ToList();

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
                await Client.IndexAsync(httpTrackerLog, x => x.Index(IndexName));
            }
            catch (Exception ex)
            {
                response.IsFailed(ex);
            }

            return response;
        }
    }
}