using HttpTracker.Domain;
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
        public HttpTrackerLogRepository(IElasticsearchProvider elasticsearchProvider, HttpTrackerElasticsearchOptions options, string yearMonth) : base(elasticsearchProvider)
        {
            IndexName = $"{options.IndexPrefix}_{IndexConsts.IndexNames.HttpTrackerLog}_{yearMonth}";
        }

        protected override string IndexName { get; }

        public async Task<HttpTrackerResponse<PagedList<HttpTrackerLog>>> SearchAsync(string type, string keyword, int page, int limit)
        {
            var response = new HttpTrackerResponse<PagedList<HttpTrackerLog>>();

            var query = new List<QueryContainer>();
            if (string.IsNullOrEmpty(type))
            {
                query.Add(new MatchPhraseQuery
                {
                    Field = new Field("Type"),
                    Query = type
                });
            }
            if (string.IsNullOrEmpty(keyword))
            {
                query.Add(new MatchPhraseQuery
                {
                    Field = new Field("Description"),
                    Query = keyword
                });
            }

            var searchResponse = await Client.SearchAsync<HttpTrackerLog>(x => x.Index(IndexName)
                                             .Query(x => x.Bool(x => x.Should(query.ToArray())))
                                             .From((page - 1) * limit)
                                             .Take(limit)
                                             .Sort(s => s.Descending(x => x.CreationTime)));
            var total = Convert.ToInt32(searchResponse.Total);
            var list = searchResponse.Documents.ToList();

            response.IsSuccess(new PagedList<HttpTrackerLog>(total, list));
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