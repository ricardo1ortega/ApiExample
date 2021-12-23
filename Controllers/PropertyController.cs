using ApiExample.Core.Models;
using ApiExample.Core.Request;
using ApiExample.Resources;
using ApiExample.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace ApiExample.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    [Produces("application/json")]
    public class PropertyController : ControllerBase
    {
        private readonly ILogger _logger;
        private PropertyService _propertyService { get; set; }
        public PropertyController(ILoggerFactory loggerFactory,
            PropertyService propertyService)
        {
            _logger = loggerFactory.CreateLogger<PropertyController>();
            _propertyService = propertyService;
        }

        /// <summary>
        /// Create property
        /// </summary>
        /// <param name="request">Request data</param>
        /// <returns>The id of the property that was created</returns>
        /// <response code="200">The property id</response>
        /// <response code="400">
        /// Status - Error code: meaning. <br></br>
        /// 400 - Validation error: Some of the fields do not comply with the expected format <br></br>
        /// errors: [ { "code": "ValidationError", "message": "Some validation error.", "path": "/property" }] <br></br>
        /// </response>
        /// <response code="500">A server error occurred in the service</response>
        [HttpPost]
        [ProducesResponseType(typeof(CreatePropertyResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseApi400ResponseExample), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseApi404ResponseExample), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(BaseApi500ResponseExample), StatusCodes.Status500InternalServerError)]
        [Authorize]
        public async Task<ActionResult<CreatePropertyResponse>> Post([FromBody] CreatePropertyRequest request)
        {
            try
            {
                var response = await _propertyService.CreateProperty(request);

                return response.Status == BaseApiResponse.ResponseCodes.NotManaged
                    ? StatusCode((int)BaseApiResponse.ResponseCodes.ValidationError, response)
                    : StatusCode((int)response.Status, response);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}
