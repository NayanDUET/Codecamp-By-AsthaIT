
using middleware;
var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

// TestDelegate func  = (string value)=>10;
// new PipelineBuilder().Test(func);

// MiddlewareDelegate func1 = (HttpContext context,Func<HttpContext,Task> next) => Task.CompletedTask;
// new PipelineBuilder().Use(func1);

var pipeline = new PipelineBuilder()
        .Use(async (context, next) =>
        {     
            Console.WriteLine("Before next");
            await next(context);
            Console.WriteLine("After next");
        })
        .Use(async (context, next) =>
        {     
            Console.WriteLine("Before nayan");
            await next(context);
            Console.WriteLine("After nayan");
        })
        .Build();
app.Run(ctx=>pipeline(ctx));


app.Run();


