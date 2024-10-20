namespace QansApiV2.CustomMiddleware
{
    public class Middleware1
    {


        public RequestDelegate _next;

        public Middleware1(RequestDelegate next)
        {
            _next = next;  
        }



        public async Task Invoke(HttpContext httpContext)
        {
            Console.WriteLine( "Before Invoke Middleware 1");

            await _next.Invoke(httpContext);

            Console.WriteLine("After invoke middleware 1");

        }

    }
}
