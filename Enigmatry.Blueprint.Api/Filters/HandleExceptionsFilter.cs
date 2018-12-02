﻿using System.Collections.Generic;
using System.Linq;
using System.Net;
using Enigmatry.Blueprint.Infrastructure.Validation;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;

namespace Enigmatry.Blueprint.Api.Filters
{
    public class HandleExceptionsFilter : ExceptionFilterAttribute
    {
        private readonly ILogger<HandleExceptionsFilter> _logger;
        private readonly bool _useDeveloperExceptionPage;

        public HandleExceptionsFilter(bool useDeveloperExceptionPage, ILogger<HandleExceptionsFilter> logger)
        {
            _useDeveloperExceptionPage = useDeveloperExceptionPage;
            _logger = logger;
        }

        public override void OnException(ExceptionContext context)
        {
            if (context.Exception is ValidationException validationException)
            {
                context.Result = context.HttpContext.CreateValidationProblemDetailsResponse(validationException);
                return;
            }

            IList<MediaTypeHeaderValue> accept = context.HttpContext.Request.GetTypedHeaders().Accept;
            if (accept != null && accept.All(header => header.MediaType != "application/json"))
            {
                return;
            }

            _logger.LogError(context.Exception, "Unexpected error");
            var jsonResult = new JsonResult(GetError(context))
            {
                StatusCode = (int) HttpStatusCode.InternalServerError
            };
            context.Result = jsonResult;
        }

        private object GetError(ExceptionContext context)
        {
            return new
            {
                message = _useDeveloperExceptionPage ? context.Exception.Message : "Unexpected error",
                stackTrace = _useDeveloperExceptionPage ? context.Exception.StackTrace : string.Empty
            };
        }
    }
}