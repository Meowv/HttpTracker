using HttpTracker.Dto.Params;
using HttpTracker.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Threading.Tasks;

namespace HttpTracker.Controller
{
    internal class HttpTrackerController
    {
        private readonly IHttpTrackerLogRepositoryFactory _factory;

        public HttpTrackerController(IHttpTrackerLogRepositoryFactory factory)
        {
            _factory = factory;
        }

        [HttpGet]
        [Route("query")]
        public async Task<IActionResult> QueryaAsync([FromQuery] QueryInput input)
        {
            var repository = _factory.CreateInstance(HttpTrackerInstance.InstanceName);

            var response = await repository.QueryAsync(input);

            return Json(response);
        }

        private IActionResult Json(object data)
        {
            return new ContentResult
            {
                Content = JsonConvert.SerializeObject(data, new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                }),
                ContentType = "application/json;charset=utf-8",
                StatusCode = StatusCodes.Status200OK
            };
        }
    }
}