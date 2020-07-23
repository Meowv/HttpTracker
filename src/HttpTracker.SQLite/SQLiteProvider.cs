using HttpTracker.Options;
using Microsoft.Extensions.Options;
using System;
using System.Data;
using System.Data.SQLite;

namespace HttpTracker
{
    public class SQLiteProvider : IDbConnectionProvider
    {
        private HttpTrackerSQLiteOptions Options { get; }

        public SQLiteProvider(IOptions<HttpTrackerSQLiteOptions> options)
        {
            Options = options.Value;

            if (string.IsNullOrEmpty(Options.ConnectionString))
                throw new Exception("SQLite 配置有误，请检查。");
        }

        public IDbConnection Connection => new SQLiteConnection(Options.ConnectionString);
    }
}