using System.Text;

namespace web_api.TcpServer;

public class HTTPBodyPerser
{
    public static string perse(byte[] rawBody)
    {
        
         if(rawBody.Length == 0) return string.Empty;
         
         return Encoding.UTF8.GetString(rawBody);
    }
}