using HttpTracker.Options;
using Microsoft.Extensions.Options;
using System;
using System.Data;
using System.Data.SqlClient;

namespace HttpTracker
{
    public class SQLServerProvider : IDbConnectionProvider
    {
        private HttpTrackerSQLServerOptions Options { get; }

        public SQLServerProvider(IOptions<HttpTrackerSQLServerOptions> options)
        {
            Options = options.Value;

            if (string.IsNullOrEmpty(Options.ConnectionString))
                throw new Exception("SQLServer 配置有误，请检查。");
        }

        public IDbConnection Connection => new SqlConnection(Options.ConnectionString);
    }
}