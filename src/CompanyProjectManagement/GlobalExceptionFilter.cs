using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;

namespace CompanyProjectManagement
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        private readonly ILogger _logger;

        public GlobalExceptionFilter(ILogger<GlobalExceptionFilter> logger)
        {
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            _logger.LogError(context.Exception, context.Exception.Message);
            context.Result = new ObjectResult("An error occurred while processing the request.")
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };
        }
    }
}