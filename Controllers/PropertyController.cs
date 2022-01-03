using ApiExample.Core.Models;
using ApiExample.Core.Request;
using ApiExample.Resources;
using ApiExample.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
//using Microsoft.Web.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
        /// </response>
        /// <response code="401">The user is Unauthorized to use the resource</response>
        /// <response code="500">A server error occurred in the service</response>
        [HttpPost("createProperty")]
        [ProducesResponseType(typeof(CreatePropertyResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseApi400ResponseExample), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseApi401ResponseExample), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(BaseApi404ResponseExample), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(BaseApi500ResponseExample), StatusCodes.Status500InternalServerError)]
        [Authorize]
        public async Task<ActionResult<CreatePropertyResponse>> Post([FromBody] CreatePropertyRequest request)
        {
            try
            {
                var response = _propertyService.CreateProperty(request);

                return response.Status == BaseApiResponse.ResponseCodes.NotManaged
                    ? StatusCode((int)BaseApiResponse.ResponseCodes.ValidationError, response)
                    : StatusCode((int)response.Status, response);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }


        /// <summary>
        /// Update property
        /// </summary>
        /// <param name="request">Request data</param>
        /// <returns>The id of the property that was created</returns>
        /// <response code="200">The property id</response>
        /// <response code="400">
        /// Status - Error code: meaning. <br></br>
        /// 400 - Validation error: Some of the fields do not comply with the expected format <br></br>
        /// </response>
        /// <response code="401">The user is Unauthorized to use the resource</response>
        /// <response code="500">A server error occurred in the service</response>
        [HttpPut("updateProperty")]
        [ProducesResponseType(typeof(UpdatePropertyResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseApi400ResponseExample), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseApi401ResponseExample), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(BaseApi404ResponseExample), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(BaseApi500ResponseExample), StatusCodes.Status500InternalServerError)]
        [Authorize]
        public ActionResult<CreatePropertyResponse> UpdateProperty([FromBody] UpdatePropertyRequest request)
        {
            try
            {
                var response = _propertyService.UpdateProperty(request);

                return response.Status == BaseApiResponse.ResponseCodes.NotManaged
                    ? StatusCode((int)BaseApiResponse.ResponseCodes.ValidationError, response)
                    : StatusCode((int)response.Status, response);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        /// <summary>
        /// List all properties
        /// </summary>
        /// <response code="200">List all properties</response>
        /// <response code="400">
        /// Status - Error code: meaning. <br></br>
        /// 400 - Validation error: Some of the fields do not comply with the expected format <br></br>
        /// </response>
        /// <response code="401">The user is Unauthorized to use the resource</response>
        /// <response code="500">A server error occurred in the service</response>
        [HttpGet("getAll")]
        [ProducesResponseType(typeof(PropertiesResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseApi400ResponseExample), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseApi401ResponseExample), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(BaseApi404ResponseExample), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(BaseApi500ResponseExample), StatusCodes.Status500InternalServerError)]
        [Authorize]
        public ActionResult<PropertiesResponse> GetAll()
        {
            try
            {
                return _propertyService.ListAllProperties();

            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        /// <summary>
        /// List properties filter by name or address or code internal
        /// </summary>
        /// <response code="200">List all properties</response>
        /// <response code="400">
        /// Status - Error code: meaning. <br></br>
        /// 400 - Validation error: Some of the fields do not comply with the expected format <br></br>
        /// </response>
        /// <response code="401">The user is Unauthorized to use the resource</response>
        /// <response code="500">A server error occurred in the service</response>
        [HttpGet("{filter}")]
        [ProducesResponseType(typeof(PropertiesResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseApi400ResponseExample), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseApi401ResponseExample), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(BaseApi404ResponseExample), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(BaseApi500ResponseExample), StatusCodes.Status500InternalServerError)]
        [Authorize]
        public ActionResult<PropertiesResponse> GetProperty(string filter)
        {
            try
            {
                return _propertyService.ListPropertiesByFilter(filter);

            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }


        /*[HttpPost("addPropertyImage")]
        [Authorize]
        [ProducesResponseType(typeof(BaseApi401ResponseExample), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> AddPropertyImage(IFormFile File)
        {
            using (var memoryStream = new MemoryStream())
            {
                await File.CopyToAsync(memoryStream);

                // Upload the file if less than 2 MB
                if (memoryStream.Length < 20)
                {
                    //TODO

                }
                else
                {
                    ModelState.AddModelError("File", "The file is too large.");
                }
            }

            return Ok();
        }*/
    }
}
