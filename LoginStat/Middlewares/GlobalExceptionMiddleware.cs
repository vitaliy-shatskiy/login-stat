using System;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using FluentValidation;
using LoginStat.Common.Exceptions;
using Microsoft.AspNetCore.Http;

namespace LoginStat.Middlewares
{
    public sealed class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public GlobalExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                var response = context.Response;
                response.ContentType = "application/json";
                response.StatusCode = GetResponseStatusCodeFromException(error);

                var exceptionMessage = error.Message.Contains('\n')
                    ? error.Message.Split('\n', StringSplitOptions.TrimEntries)
                    : new[] {error.Message};
                var result = JsonSerializer.Serialize(new
                {
                    title = "An exception was thrown",
                    status = response.StatusCode,
                    errors = new Dictionary<string, string[]> {{error.GetType().Name, exceptionMessage}}
                });

                await response.WriteAsync(result);
            }
        }

        private static int GetResponseStatusCodeFromException(Exception error)
        {
            switch (error)
            {
                case FormatException:
                case InvalidOperationException:
                case ArgumentException:
                case BadHttpRequestException:
                case ValidationException:
                    return (int) HttpStatusCode.BadRequest;
                case NotFoundException:
                case KeyNotFoundException:
                    return (int) HttpStatusCode.NotFound;
                case AggregateException:
                    return (int) HttpStatusCode.Conflict;
                default:
                    return (int) HttpStatusCode.InternalServerError;
            }
        }
    }
}