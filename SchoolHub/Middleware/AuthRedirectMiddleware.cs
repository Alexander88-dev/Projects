namespace SchoolHub.Middleware
{
    public class AuthRedirectMiddleware
    {
        private readonly RequestDelegate _next;
        public AuthRedirectMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            var path = context.Request.Path.Value?.ToLower();

            if (path.StartsWith("/api"))
            {
                await _next(context);
                return;
            }
            if (path.StartsWith("swagger")) 
            {
                await _next(context);
                return;
            }

            bool isProtectedPage = path.StartsWith("/Projects")
                || path.StartsWith("/myprojects")
                || path.StartsWith("/editproject")
                || path.StartsWith("/adminprojects");
            bool isAuthenticated = context.Session.GetInt32("UserId") != null;
            if(isProtectedPage && !isAuthenticated) 
            {
                context.Response.Redirect("/Index");
                return;
            }

            await _next(context);
        }

    }
}
