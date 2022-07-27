using System.IO;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;

namespace FunctionAppDemo.Endpoints.BasicInfo
{
    public class Create
    {
        private readonly ILogger<Create> _logger;

        public Create(ILogger<Create> log)
        {
            _logger = log;
        }

        [FunctionName(nameof(Create.CreateBasicInfo))]
        [OpenApiOperation(operationId: "BasicInfo_Create")]
        [OpenApiRequestBody("application/json", typeof(CreateBasicInfoModel), Description = "Basic Info To Create", Example = typeof(CreateBasicInfoModel))]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.Created, contentType: "application/json", bodyType: typeof(CreateBasicInfoModel), Description = "The OK response")]
        [OpenApiResponseWithoutBody(HttpStatusCode.NoContent, Description = "No content response")]
        public async Task<IActionResult> CreateBasicInfo(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "api/BasicInfo")] CreateBasicInfoModel createBasicInfo)
        {
            if (createBasicInfo == null)
            {
                return new NoContentResult();
            }

            return new CreatedResult("/api/BasicInfo/1234", createBasicInfo);
        }
    }
}

