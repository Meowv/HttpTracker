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
    public class HttpTrackerLogRepository : SQLServerRepositoryBase, IHttpTrackerLogRepository
    {
        public HttpTrackerLogRepository(IDbConnectionProvider dbConnectionProvider, HttpTrackerSQLServerOptions options, string name) : base(dbConnectionProvider)
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
                    var sql = $@"IF NOT EXISTS (SELECT * from sysobjects where id = object_id('{TableName}'))
                                 BEGIN
                                     CREATE TABLE [dbo].[{TableName}](
	                                     [Id] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	                                     [Type] [nvarchar](20) NOT NULL,
	                                     [Description] [nvarchar](200) NULL,
	                                     [UserAgent] [nvarchar](200) NULL,
	                                     [Method] [nvarchar](20) NOT NULL,
	                                     [Url] [nvarchar](200) NOT NULL,
	                                     [Referrer] [nvarchar](200) NULL,
	                                     [IpAddress] [nvarchar](20) NOT NULL,
	                                     [Milliseconds] [int] NOT NULL,
	                                     [QueryString] [nvarchar](200) NULL,
	                                     [RequestBody] [nvarchar](max) NULL,
	                                     [Cookies] [nvarchar](max) NULL,
	                                     [Headers] [nvarchar](max) NULL,
	                                     [StatusCode] [int] NOT NULL,
	                                     [ResponseBody] [nvarchar](max) NULL,
	                                     [ServerName] [nvarchar](50) NULL,
	                                     [PId] [int] NULL,
	                                     [Host] [nvarchar](20) NULL,
	                                     [Port] [int] NULL,
	                                     [ExceptionType] [nvarchar](50) NULL,
	                                     [Message] [nvarchar](200) NULL,
	                                     [StackTrace] [nvarchar](max) NULL,
	                                     [CreationTime] [datetime] NOT NULL
                                     )
                                 END";

                    await Connection.ExecuteAsync(sql);
                }
            }
            catch (Exception ex)
            {
                response.IsFailed(ex);
            }

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
                var sql = $@"INSERT INTO [dbo].[{TableName}] ([Type] ,[Description] ,[UserAgent] ,[Method] ,[Url] ,[Referrer] ,[IpAddress] ,[Milliseconds] ,[QueryString] ,[RequestBody] ,[Cookies] ,[Headers] ,[StatusCode] ,[ResponseBody] ,[ServerName] ,[PId] ,[Host] ,[Port] ,[ExceptionType] ,[Message] ,[StackTrace] ,[CreationTime]) VALUES (@Type, @Description, @UserAgent, @Method, @Url, @Referrer, @IpAddress, @Milliseconds, @QueryString, @RequestBody, @Cookies, @Headers, @StatusCode, @ResponseBody, @ServerName, @PId, @Host, @Port, @ExceptionType, @Message, @StackTrace, @CreationTime);";

                using (Connection)
                {
                    await Connection.ExecuteAsync(sql, httpTrackerLog);
                }
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
                         SELECT Type, Description, UserAgent, Method, Url , Referrer, IpAddress, Milliseconds, QueryString, RequestBody , Cookies, Headers, StatusCode, ResponseBody, ServerName , PId, Host, Port, ExceptionType, Message , StackTrace, CreationTime FROM (SELECT ROW_NUMBER() OVER(ORDER BY CreationTime DESC) AS Number, * FROM {TableName} WHERE 1= 1 {where} ) AS t WHERE t.Number BETWEEN @page AND @limit";

            using (Connection)
            {
                var page = input.Page;
                var limit = input.Limit;

                input.Page = (page - 1) * (limit + 1);
                input.Limit = page * limit;

                var query = await Connection.QueryMultipleAsync(sql, input);

                var total = await query.ReadFirstOrDefaultAsync<int>();
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

                response.IsSuccess(new PagedList<HttpTrackerLogDto>(total, list));
            }

            return response;
        }
    }
}