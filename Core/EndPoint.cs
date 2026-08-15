using web_api.TcpServers;

namespace web_api.Core;

public class EndPoint
{
    public string Path { get; }
    public string Method { get; }
    public Delegate Handler { get; }

    public EndPoint(string path, string method, Delegate handler)
    {
        Path = path;
        Method = method;
        Handler = handler;
    }

    public bool Matches(RequestContext context)
    {
        return Method.Equals(context.Method, StringComparison.OrdinalIgnoreCase)
            && Path.Equals(context.Path, StringComparison.OrdinalIgnoreCase);
    }
}