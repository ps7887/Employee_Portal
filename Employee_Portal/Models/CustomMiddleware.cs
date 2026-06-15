namespace Employee_Portal.Models
{
    public class CustomMiddleware
    {
        private readonly RequestDelegate _middleware;
        public CustomMiddleware(RequestDelegate middleware)
        {
            _middleware = middleware;
        }
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _middleware(context);
            }
            catch (Exception ex)
            {
                await context.Response
                    .WriteAsync("Error");
            }
        }
    }
}
