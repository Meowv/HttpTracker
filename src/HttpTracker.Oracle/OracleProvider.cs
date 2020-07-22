using HttpTracker.Options;
using Microsoft.Extensions.Options;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;

namespace HttpTracker
{
    public class OracleProvider : IDbConnectionProvider
    {
        public OracleProvider(IOptions<HttpTrackerOracleOptions> options)
        {
            Options = options.Value;

            if (string.IsNullOrEmpty(Options.ConnectionString))
                throw new Exception("Oracle 配置有误，请检查。");

            Connection = new OracleConnection(Options.ConnectionString);
        }

        public HttpTrackerOracleOptions Options { get; }

        public IDbConnection Connection { get; }
    }
}