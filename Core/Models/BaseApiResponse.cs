using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace ApiExample.Core.Models
{
    public class BaseApiResponse
    {
        public enum ResponseCodes
        {
            /// <summary>
            /// Ok
            /// </summary>
            Ok = 200,
            /// <summary>
            /// Created
            /// </summary>
            Created = 201,
            /// <summary>
            /// ValidationError
            /// </summary>
            ValidationError = 400,
            /// <summary>
            /// Unauthorized
            /// </summary>
            Unauthorized = 401,
            /// <summary>
            /// NotFound
            /// </summary>
            NotFound = 404,
            /// <summary>
            /// Duplicated
            /// </summary>
            Duplicated = 409,
            /// <summary>
            /// NotManaged
            /// </summary>
            NotManaged = 410,
            /// <summary>
            /// Locked
            /// </summary>
            Locked = 423,
            /// <summary>
            /// Maximum number of products
            /// </summary>
            MaxProducts = 428,
            /// <summary>
            /// Too many tries
            /// </summary>
            TooManyTries = 429,
            /// <summary>
            /// ServerError
            /// </summary>
            ServerError = 500
        }

        /// <summary>
        /// All good!
        /// </summary>
        /// <example>200</example>
        [DefaultValue(ResponseCodes.Ok)]
        public ResponseCodes Status { get; set; } = ResponseCodes.Ok;

        /// <summary>
        /// Trace of the current request
        /// </summary>
        public string TraceId { get; set; }

        public BaseApiResponse ServerError(string message)
        {
            Status = ResponseCodes.ServerError;
            /*Errors = new List<ErrorApiResponse>
            {
                new ErrorApiResponse
                {
                    Code = ResponseCodes.ServerError.ToString(),
                    Message = message,
                    Path = $"server error"
                }
            };
            */
            return this;
        }


        public static BadRequestObjectResult BadRequestResponse(ActionContext actionContext)
        {
            return new BadRequestObjectResult(new BaseApiResponse
            {
                TraceId = actionContext.HttpContext.TraceIdentifier,
                Status = ResponseCodes.ValidationError,
                /*Errors = actionContext.ModelState
                 .Where(modelError => modelError.Value.Errors.Count > 0)
                 .Select(modelError => new ErrorApiResponse
                 {
                     Path = $"/{modelError.Key.ToLower(CultureInfo.InvariantCulture)}",
                     Message = modelError.Value.Errors.FirstOrDefault().ErrorMessage,
                     Code = ResponseCodes.ValidationError.ToString(),
                 }).ToList()*/
            });
        }

        public TResponse ServerError<TResponse>(string message) where TResponse : BaseApiResponse
        {
            TResponse response = (TResponse)this;

            Status = ResponseCodes.ServerError;
            /*Errors = new List<ErrorApiResponse>
            {
                new ErrorApiResponse
                {
                    Code = ResponseCodes.ServerError.ToString(),
                    Message = message,
                    Path = $"server error"
                }
            };*/

            return response;
        }
        public TResponse Unauthorize<TResponse>(string field, string message, string mensaje = null) where TResponse : BaseApiResponse
        {
            return BaseResponse<TResponse>(ResponseCodes.Unauthorized, field, message, mensaje, 0);
        }

        public TResponse NotManage<TResponse>(string field, string message) where TResponse : BaseApiResponse
        {
            return BaseResponse<TResponse>(ResponseCodes.NotManaged, field, message, null, 0);
        }

        public TResponse NotManage<TResponse>(string field, string message, string mensaje) where TResponse : BaseApiResponse
        {
            return BaseResponse<TResponse>(ResponseCodes.NotManaged, field, message, mensaje, 0);
        }

        private TResponse BaseResponse<TResponse>(ResponseCodes code,
            string field,
            string message,
            string mensaje,
            int time) where TResponse : BaseApiResponse
        {
            TResponse response = (TResponse)this;

            Status = code;
            /*response.Errors = new List<ErrorApiResponse>
            {
                new ErrorApiResponse
                {
                    Code = code.ToString(),
                    Message = message,
                    Mensaje = mensaje,
                    Path = $"/{field}",
                    Time = time
                }
            };*/

            return response;
        }
    }
}
