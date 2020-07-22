using HttpTracker.Options;
using Microsoft.Extensions.Options;
using System;
using System.Data;
using System.Data.SQLite;

namespace HttpTracker
{
    public class SQLiteProvider : IDbConnectionProvider
    {
        public SQLiteProvider(IOptions<HttpTrackerSQLiteOptions> options)
        {
            Options = options.Value;

            if (string.IsNullOrEmpty(Options.ConnectionString))
                throw new Exception("SQLite 配置有误，请检查。");

            Connection = new SQLiteConnection(Options.ConnectionString);
        }

        public HttpTrackerSQLiteOptions Options { get; }

        public IDbConnection Connection { get; }
    }
}