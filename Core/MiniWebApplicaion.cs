using System.Net;
using web_api.TcpServers;
namespace web_api.Core;

internal class MiniWebAppllication
{

    private readonly Router _router = new();
    public EndPoint MapGet(string pattern, Delegate handler)
    {
        
      return _router.MapGet(pattern,handler);

    }
     public async Task RunAsync(int port)
    {
        
        var server = new TcpServer(port,_router);
        await server.StartAsync();
    }
}