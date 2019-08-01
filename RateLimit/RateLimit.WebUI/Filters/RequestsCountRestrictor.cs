using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RateLimit.WebUI.Filters
{
    public class RequestsCountRestrictorAttribute : Attribute, IAsyncActionFilter, IAsyncResultFilter
    {
        private static Semaphore _semaphore;

        public RequestsCountRestrictorAttribute(int count)
        {
            _semaphore = new Semaphore(count, count);
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            _semaphore.WaitOne();
            await next();
        }

        public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            _semaphore.Release();
            await next();
        }
    }
}
