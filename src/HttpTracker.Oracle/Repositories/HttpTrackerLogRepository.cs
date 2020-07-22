using HttpTracker.Domain;
using HttpTracker.Dto;
using HttpTracker.Dto.Params;
using HttpTracker.Options;
using HttpTracker.Response;
using System;
using System.Threading.Tasks;

namespace HttpTracker.Repositories
{
    public class HttpTrackerLogRepository : OracleRepositoryBase, IHttpTrackerLogRepository
    {
        public HttpTrackerLogRepository(IDbConnectionProvider dbConnectionProvider, HttpTrackerOracleOptions options, string name) : base(dbConnectionProvider)
        {
            var tableName = $"{DbConsts.TableNames.HttpTrackerLog}_{name}";
            if (!string.IsNullOrEmpty(options.TablePrefix))
            {
                tableName = $"{options.TablePrefix}_{tableName}";
            }

            TableName = tableName;
        }

        protected override string TableName { get; }

        public Task<HttpTrackerResponse> InitAsync()
        {
            throw new NotImplementedException();
        }

        public Task<HttpTrackerResponse> InsertAsync(HttpTrackerLog httpTrackerLog)
        {
            throw new NotImplementedException();
        }

        public Task<HttpTrackerResponse<PagedList<HttpTrackerLogDto>>> QueryAsync(QueryInput input)
        {
            throw new NotImplementedException();
        }
    }
}