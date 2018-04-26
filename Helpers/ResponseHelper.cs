using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Linq;
using System.Net;

namespace NewApp.Helpers
{
    public static class ResponseHelper
    {
        public static ObjectResult ToObjectResult(bool success = true, object data = null,
            string message = null, HttpStatusCode httpStatusCode = HttpStatusCode.OK)
        {
            if (data == null)
            {
                var apiResponse = new ApiResponseWithoutData()
                {
                    Status = success ? 1 : 0,
                    Message = message,
                };
                return new ObjectResult(apiResponse) { StatusCode = (int)httpStatusCode };
            }
            else
            {
                var apiResponse = new ApiResponseWithData()
                {
                    Status = success ? 1 : 0,
                    Data = data,
                    Message = message,
                };
                return new ObjectResult(apiResponse) { StatusCode = (int)httpStatusCode };
            }
        }

        public static ObjectResult Success(string successMsg = null, Object data = null)
        {
            return ToObjectResult(true, data, successMsg, HttpStatusCode.OK);
        }

        public static ObjectResult Created(string successMsg = null, Object data = null)
        {
            return ToObjectResult(true, data, successMsg, HttpStatusCode.Created);
        }

        public static ObjectResult BadRequest(ModelStateDictionary modelState)
        {
            string errorMsg = null;
            var error = modelState.SelectMany(x => x.Value.Errors).First();
            if (!string.IsNullOrEmpty(error.ErrorMessage))
                errorMsg = error.ErrorMessage;
            else if (error.Exception?.Message != null)
                errorMsg = error.Exception.Message;

            return ToObjectResult(false, null, errorMsg, HttpStatusCode.BadRequest);
        }

        public static ObjectResult BadRequest(string errorMsg)
        {
            return ToObjectResult(false, null, errorMsg, HttpStatusCode.BadRequest);
        }

        public static ObjectResult BadRequest(Object data)
        {
            return ToObjectResult(false, data, "failed", HttpStatusCode.BadRequest);
        }

        public static ObjectResult ForBidden(string errorMsg)
        {
            return ToObjectResult(false, null, errorMsg, HttpStatusCode.Forbidden);
        }

        public static ObjectResult Conflict(string errorMsg)
        {
            return ToObjectResult(false, null, errorMsg, HttpStatusCode.Conflict);
        }

        public static ObjectResult NotFound(string errorMsg)
        {
            return ToObjectResult(false, null, errorMsg, HttpStatusCode.NotFound);
        }

        public static ObjectResult ExpectationFailed(string errorMsg)
        {
            return ToObjectResult(false, null, errorMsg, HttpStatusCode.ExpectationFailed);
        }

        public static ObjectResult Unauthorized(string errorMsg = "Unauthorized")
        {
            return ToObjectResult(false, null, errorMsg, HttpStatusCode.Unauthorized);
        }
    }

    internal class ApiResponseWithData : ApiResponseWithoutData
    {
        public object Data { get; set; }
    }

    internal class ApiResponseWithoutData
    {
        public int Status { get; set; }

        public string Message { get; set; }
    }
}