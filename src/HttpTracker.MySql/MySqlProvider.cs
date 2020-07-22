using HttpTracker.Options;
using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace HttpTracker
{
    public class MySqlProvider : IDbConnectionProvider
    {
        public MySqlProvider(IOptions<HttpTrackerMySqlOptions> options)
        {
            Options = options.Value;

            if (string.IsNullOrEmpty(Options.ConnectionString))
                throw new Exception("MySql 配置有误，请检查。");

            Connection = new MySqlConnection(Options.ConnectionString);
        }

        public HttpTrackerMySqlOptions Options { get; }

        public IDbConnection Connection { get; }
    }
}