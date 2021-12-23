using ApiExample.Core.Models;
using ApiExample.Core.Request;
using ApiExample.Infrastructure;
using ApiExample.Resources;
using ApiExample.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ApiExample.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    [Produces("application/json")]
    public class AccountController : ControllerBase
    {
        private readonly ILogger _logger;
        private IAccountService _userService;
        private IJwtAuthManager _jwtAuthManager;
        public AccountController(
            ILoggerFactory loggerFactory,
            IJwtAuthManager jwtAuthManager,
            IAccountService accountService)
        {
            _logger = loggerFactory.CreateLogger<AccountController>();
            _jwtAuthManager = jwtAuthManager;
            _userService = accountService;
            
        }

        /// <summary>
        /// Login user
        /// </summary>
        /// <param name="request">Request data</param>
        /// <returns>The login result</returns>
        /// <response code="200">LoginResult</response>
        /// <response code="400">
        /// Status - Error code: meaning. <br></br>
        /// 400 - Validation error: Some of the fields do not comply with the expected format <br></br>
        /// errors: [ { "code": "ValidationError", "message": "Some validation error.", "path": "/property" }] <br></br>
        /// </response>
        /// <response code="500">A server error occurred in the service</response>
        [ProducesResponseType(typeof(LoginResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseApi401ResponseExample), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(BaseApi500ResponseExample), StatusCodes.Status500InternalServerError)]
        [AllowAnonymous]
        [HttpPost("login")]
        public ActionResult Login([FromBody] LoginRequest request)
        {

            if (!_userService.IsValidUserCredentials(request.UserName, request.Password))
            {
                return Unauthorized();
            }

            var claims = new[]
            {
                new Claim(ClaimTypes.Name,request.UserName)
            };

            var jwtResult = _jwtAuthManager.GenerateTokens(request.UserName, claims, DateTime.Now);
            _logger.LogInformation($"User [{request.UserName}] logged in the system.");
            return Ok(new LoginResult
            {
                UserName = request.UserName,
                AccessToken = jwtResult.AccessToken,
                RefreshToken = jwtResult.RefreshToken.TokenString
            });
        }

        /// <summary>
        /// Save a user
        /// </summary>
        /// <param name="request">Request data</param>
        /// <returns>Ok</returns>
        /// <response code="200">Ok</response>
        /// <response code="500">A server error occurred in the service</response>
        [ProducesResponseType(typeof(LoginResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseApi500ResponseExample), StatusCodes.Status500InternalServerError)]
        [AllowAnonymous]
        [HttpPost("save")]
        public ActionResult Save([FromBody] LoginRequest request)
        {
            _userService.CreateAccount(request.UserName, request.Password);
            _logger.LogInformation($"User [{request.UserName}] created in the system.");
            return Ok();
        }

        [HttpPost("logout")]
        [Authorize]
        public ActionResult Logout()
        {
            // optionally "revoke" JWT token on the server side --> add the current token to a block-list
            // https://github.com/auth0/node-jsonwebtoken/issues/375

            var userName = User.Identity?.Name;
            _jwtAuthManager.RemoveRefreshTokenByUserName(userName);
            _logger.LogInformation($"User [{userName}] logged out the system.");
            return Ok();
        }

        [HttpPost("refresh-token")]
        [Authorize]
        public async Task<ActionResult> RefreshToken([FromBody] RefreshTokenRequest request)
        {
            try
            {
                var userName = User.Identity?.Name;
                _logger.LogInformation($"User [{userName}] is trying to refresh JWT token.");

                if (string.IsNullOrWhiteSpace(request.RefreshToken))
                {
                    return Unauthorized();
                }

                var accessToken = await HttpContext.GetTokenAsync("Bearer", "access_token");
                var jwtResult = _jwtAuthManager.Refresh(request.RefreshToken, accessToken, DateTime.Now);
                _logger.LogInformation($"User [{userName}] has refreshed JWT token.");
                return Ok(new LoginResult
                {
                    UserName = userName,
                    AccessToken = jwtResult.AccessToken,
                    RefreshToken = jwtResult.RefreshToken.TokenString
                });
            }
            catch (SecurityTokenException e)
            {
                return Unauthorized(e.Message); // return 401 so that the client side can redirect the user to login page
            }
        }

    }
}
