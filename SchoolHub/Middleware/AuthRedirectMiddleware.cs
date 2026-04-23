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
            bool isProtectedPage = path.StartsWith("/projects")
                || path.StartsWith("/myprojects")
                || path.StartsWith("/editproject");
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
