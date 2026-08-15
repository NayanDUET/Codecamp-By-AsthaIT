using System.Text;

namespace web_api.TcpServers;

public class HTTPBodyPerser
{
    public static string Parse(byte[] rawBody)
    {
        
         if(rawBody.Length == 0) return string.Empty;
         
         return Encoding.UTF8.GetString(rawBody);
    }
}