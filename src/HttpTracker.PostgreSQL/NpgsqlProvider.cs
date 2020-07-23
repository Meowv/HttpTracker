using HttpTracker.Options;
using Microsoft.Extensions.Options;
using Npgsql;
using System;
using System.Data;

namespace HttpTracker
{
    public class NpgsqlProvider : IDbConnectionProvider
    {
        private HttpTrackerNpgsqlOptions Options { get; }

        public NpgsqlProvider(IOptions<HttpTrackerNpgsqlOptions> options)
        {
            Options = options.Value;

            if (string.IsNullOrEmpty(Options.ConnectionString))
                throw new Exception("PostgreSQL 配置有误，请检查。");
        }

        public IDbConnection Connection => new NpgsqlConnection(Options.ConnectionString);
    }
}