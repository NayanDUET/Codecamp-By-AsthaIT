namespace middleware;

public delegate Task MiddlewareDelegate(HttpContext context,Func<HttpContext,Task> next);

public delegate int TestDelegate(string value);
public class PipelineBuilder
{
     private readonly List<MiddlewareDelegate> _middleware = new();
     public PipelineBuilder Use(MiddlewareDelegate middleware)
    {
        

          _middleware.Add(middleware);
          return this;
    }

    public Func<HttpContext , Task> Build()
    {
        Func<HttpContext,Task> EndNode = (context) =>
        {
             Console.WriteLine("End");
             return Task.CompletedTask;
            
        };
        for(var i= _middleware.Count - 1; i >= 0; i--)
        {
            
            var current = _middleware[i];
            var next = EndNode;
            EndNode = ctx => current(ctx,next);

        }

        return EndNode;

    }

    public void Test(TestDelegate test)
    {
        var value = test("nayan");
        //Console.WriteLine(value);
    }
}