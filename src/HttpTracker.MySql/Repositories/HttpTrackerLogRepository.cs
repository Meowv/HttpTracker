using Dapper;
using HttpTracker.Domain;
using HttpTracker.Dto;
using HttpTracker.Dto.Params;
using HttpTracker.Options;
using HttpTracker.Response;
using System;
using System.Threading.Tasks;

namespace HttpTracker.Repositories
{
    public class HttpTrackerLogRepository : MySqlRepositoryBase, IHttpTrackerLogRepository
    {
        public HttpTrackerLogRepository(IDbConnectionProvider dbConnectionProvider, HttpTrackerMySqlOptions options, string name) : base(dbConnectionProvider)
        {
            var tableName = $"{DbConsts.TableNames.HttpTrackerLog}_{name}";
            if (!string.IsNullOrEmpty(options.TablePrefix))
            {
                tableName = $"{options.TablePrefix}_{tableName}";
            }

            TableName = tableName;
        }

        protected override string TableName { get; }

        public async Task<HttpTrackerResponse> InitAsync()
        {
            var response = new HttpTrackerResponse();

            try
            {
                using (Connection)
                {
                    var sql = $@"CREATE TABLE IF NOT EXISTS `{TableName}` (
                                  `Id` int(11) NOT NULL,
                                  `Type` varchar(20) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
                                  `Description` varchar(200) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
                                  `UserAgent` varchar(200) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
                                  `Method` varchar(20) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
                                  `Url` varchar(200) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
                                  `Referrer` varchar(200) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
                                  `IpAddress` varchar(20) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
                                  `Milliseconds` int(11) NOT NULL,
                                  `QueryString` varchar(200) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
                                  `RequestBody` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL,
                                  `Cookies` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL,
                                  `Headers` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL,
                                  `StatusCode` int(11) NOT NULL,
                                  `ResponseBody` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL,
                                  `ServerName` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
                                  `PId` int(11) NULL DEFAULT NULL,
                                  `Host` varchar(20) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
                                  `Port` int(11) NULL DEFAULT NULL,
                                  `ExceptionType` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
                                  `Message` varchar(200) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
                                  `StackTrace` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL,
                                  `CreationTime` datetime(0) NOT NULL,
                                  PRIMARY KEY (`Id`) USING BTREE
                                ) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;";

                    await Connection.ExecuteAsync(sql);
                }
            }
            catch (Exception ex)
            {
                response.IsFailed(ex);
            }

            return response;
        }

        public Task<HttpTrackerResponse<PagedList<HttpTrackerLogDto>>> QueryAsync(QueryInput input)
        {
            throw new NotImplementedException();
        }

        public Task<HttpTrackerResponse> InsertAsync(HttpTrackerLog httpTrackerLog)
        {
            throw new NotImplementedException();
        }
    }
}