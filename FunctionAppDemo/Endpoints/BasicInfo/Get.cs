using System.IO;
using System.Net;
using System.Security.Claims;
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
    public class Get
    {
        private readonly ILogger<Get> _logger;

        public Get(ILogger<Get> log)
        {
            _logger = log;
        }

        [FunctionName(nameof(BasicInfo.Get.GetBasicInfo))]
        [OpenApiOperation(operationId: "BasicInfo_Get")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(string), Description = "The OK response")]
        public async Task<IActionResult> GetBasicInfo(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "api/BasicInfo")] HttpRequest req,
            ClaimsPrincipal claimsPrincipal)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            var responseMessage = "Not authenticated";

            if (claimsPrincipal.Identity.IsAuthenticated)
            {
                responseMessage += $"{claimsPrincipal.Identity.Name}\r\n";

                if (claimsPrincipal.IsInRole("Read.Data"))
                {
                    responseMessage = "Is in Read.Data\r\n";
                }

                foreach (var claim in claimsPrincipal.Claims)
                {
                    responseMessage += $"Claim {claim.Type}: {claim.Value}\r\n";
                }
            }

            return new OkObjectResult(responseMessage);
        }
    }
}

