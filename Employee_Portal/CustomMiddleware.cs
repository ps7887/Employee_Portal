namespace Employee_Portal
{
    public class CustomMiddleware
    {
        private readonly IMiddleware _middleware;
        public CustomMiddleware(IMiddleware middleware)
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
                await context.Response.WriteAsync(ex.ToString());
            }
        }
    }
}
