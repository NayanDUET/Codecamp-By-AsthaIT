using System.Net;
using System.Net.Sockets;
using System.Text;
using web_api.Core;

namespace web_api.TcpServers;

public class RequestContext
{
     
      public string Method {get;set;} = string.Empty;
      public string Path {get;set;}= string.Empty;
      public string Version {get;set;}= string.Empty;

      public Dictionary<string,string> headers{get;set;}=[];

      public string? body{get;set;}
}

public class TcpServer
{
    
     private readonly int _port;
     private readonly Router _router;

     public TcpServer(int port,Router router)
    {
         _port = port;
         _router = router;
    }

    public async Task StartAsync()
     {
     var listener = new TcpListener(IPAddress.Any, _port);
     listener.Start();

     Console.WriteLine($"Server started on port {_port}");

     while (true)
     {
          var client = await listener.AcceptTcpClientAsync();
          _ = Task.Run(() => HandleClient(client));
     }
     }

    private async Task HandleClient(TcpClient client)
    {
        
         using var stream = client.GetStream();

         var (rawHeader, rawBody) = await HTTPRequestReader.ReadAsync(stream);
         var context =  HTTPHeaderPerser.Parse(rawHeader);
         var body =  HTTPBodyPerser.Parse(rawBody);

         var response = _router.Resolve(context);

         //var response = $"Hey we recived a";

         var responseInByte = Encoding.UTF8.GetBytes(

            "HTTP/1.1 200 OK\r\n"+ 
            "Content-Length: "+response.Length+ "\r\n\r\n"+
            response
         );

         await stream.WriteAsync(responseInByte);
    }
}