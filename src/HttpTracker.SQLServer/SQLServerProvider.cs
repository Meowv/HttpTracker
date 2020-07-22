using HttpTracker.Options;
using Microsoft.Extensions.Options;
using System;
using System.Data;
using System.Data.SqlClient;

namespace HttpTracker
{
    public class SQLServerProvider : IDbConnectionProvider
    {
        public SQLServerProvider(IOptions<HttpTrackerSQLServerOptions> options)
        {
            Options = options.Value;

            if (string.IsNullOrEmpty(Options.ConnectionString))
                throw new Exception("SQLServer 配置有误，请检查。");

            Connection = new SqlConnection(Options.ConnectionString);
        }

        public HttpTrackerSQLServerOptions Options { get; }

        public IDbConnection Connection { get; }
    }
}