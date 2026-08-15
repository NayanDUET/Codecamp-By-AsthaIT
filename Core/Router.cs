using System.IO.Pipelines;
using web_api.TcpServers;

namespace web_api.Core;

public class Router
{
    
    public readonly List<EndPoint> _endpoints = [];

    public EndPoint MapGet(string path,Delegate hadler)
    {
        var endpoint = new EndPoint(path,"GET",hadler);
        _endpoints.Add(endpoint);
        return endpoint;
    }

    public string Resolve(RequestContext context)
    {
        
        var endpoint = _endpoints.FirstOrDefault(ep => ep.Matches(context));

        if(endpoint is null) return "404 is not found";

        var method = endpoint.Handler.Method;
        var args = new object?[1];

        args[0] = context;

        var Result = method.Invoke(endpoint.Handler.Target,args);

        return Result?.ToString()??"";


    }
}