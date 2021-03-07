using FullStack.API.Exceptions;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace FullStack.API.Helpers
{
    public class ValidationErrorHandlingMiddleware
    {
        private readonly RequestDelegate next;
        public ValidationErrorHandlingMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (ValidationException ex)
            {
                await HandleValidationExceptionAsync(context, ex);
            }
        }

        private static Task HandleValidationExceptionAsync(HttpContext context, ValidationException exception)
        {
            var result = JsonConvert.SerializeObject(exception.Errors);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.UnprocessableEntity;

            return context.Response.WriteAsync(result);
        }
    }
}
