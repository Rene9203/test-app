using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestApp.Application.Exceptions;
using TestApp.Application.ViewModels;

namespace TestApp.Infraestructure.Filter
{
    public class HttpGlobalExceptionFilter : IExceptionFilter
    {
        private readonly IHostingEnvironment _env;

        public HttpGlobalExceptionFilter(IHostingEnvironment env)
        {
            _env = env;
        }

        public void OnException(ExceptionContext context)
        {
            if (context.Exception.GetType() == typeof(ValidationException))
            {
                IEnumerable<ValidationFailure> validationErrors = (context.Exception as ValidationException)?.Errors;
                Dictionary<string, List<string>> validations = new Dictionary<string, List<string>>();
                if (validationErrors != null)
                    foreach (ValidationFailure validationFailure in validationErrors)
                        if (validations.ContainsKey(validationFailure.PropertyName))
                            validations[validationFailure.PropertyName].Add(validationFailure.ErrorMessage);
                        else
                            validations.Add(validationFailure.PropertyName,
                                new List<string>() { validationFailure.ErrorMessage });

                ApiResponse json = new ApiResponse
                {
                    Success = false,
                    ValidationErrors = validations
                };

                context.Result = new BadRequestObjectResult(json);
                context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
            }
            else if (context.Exception.GetType() == typeof(ApiException))
            {
                ApiException apiException = (context.Exception as ApiException);
                ApiResponse json = new ApiResponse
                {
                    Success = false,
                    Errors = apiException.Messages
                };
                if (apiException.StatusCode.Equals(StatusCodes.Status404NotFound))
                {
                    context.Result = new NotFoundObjectResult(json);
                    context.HttpContext.Response.StatusCode = apiException.StatusCode;
                }
                if (apiException.StatusCode.Equals(StatusCodes.Status400BadRequest))
                {
                    context.Result = new BadRequestObjectResult(json);
                    context.HttpContext.Response.StatusCode = apiException.StatusCode;
                }
                if (apiException.StatusCode.Equals(StatusCodes.Status403Forbidden))
                {
                    context.Result = new ForbidResult("Sorry you don't have permission");
                    context.HttpContext.Response.StatusCode = apiException.StatusCode;
                }
            }
            else
            {
                ApiResponse json = new ApiResponse
                {
                    Success = false,
                    Errors = new[] { "Sorry, your request cannot be processed at this time. We are currently making some changes in the system to improve  user experience. Please try again later and if the error continues, contact us." }
                };
            }
            context.ExceptionHandled = true;
        }

    }
}
