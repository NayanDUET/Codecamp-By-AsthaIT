using System.Net;
using System.Net.Sockets;
using System.Text;

namespace web_api.TcpServer;

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

     public TcpServer(int port)
    {
         _port = port;
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
         var body =  HTTPBodyPerser.perse(rawBody);

     //     var buffer = new Byte[1024];

     //     var byteCount = await stream.ReadAsync(buffer);
     //     var requestText = Encoding.UTF8.GetString(buffer,0,byteCount);

     //     var lines = requestText.Split("\r\n");

     //     var requestLine = lines[0].Split(' ');

     //     var method = requestLine[0];
     //     var path = requestLine[1];

         var response = $"Hey we recived a";

         var responseInByte = Encoding.UTF8.GetBytes(

            "HTTP/1.1 200 OK\r\n"+ 
            "Content-Length: "+response.Length+ "\r\n\r\n"+
            response
         );

         await stream.WriteAsync(responseInByte);
    }
}