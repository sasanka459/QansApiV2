namespace QansApiV2.CustomMiddleware
{
    public class Middleware2
    {
        public RequestDelegate _next;

        public Middleware2(RequestDelegate next)
        {
            _next = next;
        }



        public async Task Invoke(HttpContext httpContext)
        {
            Console.WriteLine("Before Invoke Middleware 2");


            ///
            await _next.Invoke(httpContext);
            //after logic
            Console.WriteLine("After invoke middleware 2");

        }




    }
}
