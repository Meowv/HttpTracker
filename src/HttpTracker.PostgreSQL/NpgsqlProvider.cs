using HttpTracker.Options;
using Microsoft.Extensions.Options;
using Npgsql;
using System;
using System.Data;

namespace HttpTracker
{
    public class NpgsqlProvider : IDbConnectionProvider
    {
        public NpgsqlProvider(IOptions<HttpTrackerNpgsqlOptions> options)
        {
            Options = options.Value;

            if (string.IsNullOrEmpty(Options.ConnectionString))
                throw new Exception("PostgreSQL 配置有误，请检查。");

            Connection = new NpgsqlConnection(Options.ConnectionString);
        }

        public HttpTrackerNpgsqlOptions Options { get; }

        public IDbConnection Connection { get; }
    }
}