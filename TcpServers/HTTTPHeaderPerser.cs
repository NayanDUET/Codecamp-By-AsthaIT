using System.Text;
using web_api.Core;
namespace web_api.TcpServers;

public class HTTPHeaderPerser{

    public static RequestContext Parse(byte[] rawHeader)
    {
        
        var text = Encoding.UTF8.GetString(rawHeader);

         var lines =text.Split("\r\n",StringSplitOptions.RemoveEmptyEntries);

         var requestLine = lines[0].Split(' ');

         var ctx = new RequestContext{
            
            Method = requestLine[0],
            Path = requestLine[1],
            Version = requestLine.Length > 2 ? requestLine[2] : "HTTP/1.1"
        };

        for(var i=0;i<lines.Length-1;i++){

            var colon = lines[i].IndexOf(':');

            if (colon > 0)
            {
                var key = lines[i][..colon].Trim();
                var value = lines[i][(colon+1)..].Trim();
                ctx.headers[key] = value;
                
            }
        }

        return ctx;
    }
}