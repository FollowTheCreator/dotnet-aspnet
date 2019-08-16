using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using NLog;

namespace RequestTimeTrackingMiddleware.WebUI.Utils.TimeTracking
{
    public class TimeTrackingMiddleware
    {
        private readonly RequestDelegate _next;

        private readonly Logger _logger;

        public TimeTrackingMiddleware(RequestDelegate next)
        {
            _next = next;
            _logger = LogManager.GetCurrentClassLogger();
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var sw = new Stopwatch();
            sw.Start();

            await _next.Invoke(context);

            sw.Stop();

            
            _logger.Info($"{sw.ElapsedMilliseconds} ms");
        }
    }
}
