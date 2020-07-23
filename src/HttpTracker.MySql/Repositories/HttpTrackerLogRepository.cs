using Dapper;
using HttpTracker.Domain;
using HttpTracker.Dto;
using HttpTracker.Dto.Params;
using HttpTracker.Options;
using HttpTracker.Response;
using System;
using System.Linq;
using System.Text;
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
                using var conn = _dbConnectionProvider.Connection;

                var sql = $@"CREATE TABLE IF NOT EXISTS `{TableName}` (
                               `Id` int(11) NOT NULL AUTO_INCREMENT,
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
                await conn.ExecuteAsync(sql);
            }
            catch (Exception ex)
            {
                response.IsFailed(ex);
            }

            return response;
        }

        public async Task<HttpTrackerResponse<PagedList<HttpTrackerLogDto>>> QueryAsync(QueryInput input)
        {
            var response = new HttpTrackerResponse<PagedList<HttpTrackerLogDto>>();

            var builder = new StringBuilder();

            if (!string.IsNullOrEmpty(input.Type))
            {
                builder.Append($" AND Type LIKE @Type");

                input.Type = $"%{input.Type}%";
            }
            if (!string.IsNullOrEmpty(input.Keyword))
            {
                builder.Append($" AND Description LIKE @Keyword");

                input.Keyword = $"%{input.Keyword}%";
            }

            var where = builder.ToString();

            var sql = $@"SELECT COUNT(1) FROM {TableName} WHERE 1 = 1 {where};
                         SELECT `Type`, `Description`, `UserAgent`, `Method`, `Url` , `Referrer`, `IpAddress`, `Milliseconds`, `QueryString`, `RequestBody` , `Cookies`, `Headers`, `StatusCode`, `ResponseBody`, `ServerName` , `PId`, `Host`, `Port`, `ExceptionType`, `Message` , `StackTrace`, `CreationTime` FROM {TableName} WHERE 1 = 1 {where} ORDER BY CreationTime DESC LIMIT @Limit OFFSET @Page";

            using var conn = _dbConnectionProvider.Connection;

            input.Page = (input.Page - 1) * input.Limit;

            var query = await conn.QueryMultipleAsync(sql, input);

            var total = await query.ReadFirstOrDefaultAsync<long>();
            var logs = await query.ReadAsync<HttpTrackerLog>();

            var list = logs.Select(x => new HttpTrackerLogDto
            {
                Type = x.Type,
                Description = x.Description,
                Request = new RequestInfo
                {
                    UserAgent = x.UserAgent,
                    Method = x.Method,
                    Url = x.Url,
                    Referrer = x.Referrer,
                    IpAddress = x.IpAddress,
                    Milliseconds = x.Milliseconds,
                    RequestBody = x.RequestBody,
                    Cookies = x.Cookies,
                    Headers = x.Headers
                },
                Response = new ResponseInfo
                {
                    StatusCode = x.StatusCode,
                    ResponseBody = x.ResponseBody
                },
                Server = new ServerInfo
                {
                    ServerName = x.ServerName,
                    PId = x.PId,
                    Host = x.Host,
                    Port = x.Port
                },
                Exception = new ExceptionInfo
                {
                    ExceptionType = x.ExceptionType,
                    Message = x.Message,
                    StackTrace = x.StackTrace
                },
                CreationTime = x.CreationTime
            }).ToList();

            response.IsSuccess(new PagedList<HttpTrackerLogDto>(Convert.ToInt32(total), list));
            return response;
        }

        public async Task<HttpTrackerResponse> InsertAsync(HttpTrackerLog httpTrackerLog)
        {
            var response = new HttpTrackerResponse();

            if (httpTrackerLog == null)
            {
                response.IsFailed("HttpTrackerLog is null");
                return response;
            }

            try
            {
                var sql = $@"INSERT INTO `{TableName}`(`Type`, `Description`, `UserAgent`, `Method`, `Url`, `Referrer`, `IpAddress`, `Milliseconds`, `QueryString`, `RequestBody`, `Cookies`, `Headers`, `StatusCode`, `ResponseBody`, `ServerName`, `PId`, `Host`, `Port`, `ExceptionType`, `Message`, `StackTrace`, `CreationTime`) VALUES (@Type, @Description, @UserAgent, @Method, @Url, @Referrer, @IpAddress, @Milliseconds, @QueryString, @RequestBody, @Cookies, @Headers, @StatusCode, @ResponseBody, @ServerName, @PId, @Host, @Port, @ExceptionType, @Message, @StackTrace, @CreationTime);";

                using var conn = _dbConnectionProvider.Connection;
                await conn.ExecuteAsync(sql, httpTrackerLog);
            }
            catch (Exception ex)
            {
                response.IsFailed(ex);
            }

            return response;
        }
    }
}