using System.Net.Sockets;
using System.Text;

namespace web_api.TcpServers;

public class HTTPRequestReader
{
    private const int MaxHeaderSize = 32*1024;
    private const int MaxBodySize = 10*1024*1024;

    public static async Task<(byte[] Header , byte[] Body)> ReadAsync(NetworkStream stream)
    {      

         using var memorybuffer = new MemoryStream();
         var tempBuffer = new byte[4096];

         var headerEnd = -1;

        while (true)
        {
            
            var read = await stream.ReadAsync(tempBuffer);
            if(read == 0)
            {
                throw new EndOfStreamException("Clint disconnect before completing request");

                
            }

            memorybuffer.Write(tempBuffer,0,read);
            var raw = memorybuffer.ToArray();

            //\r\n\r\n

            for(var i = 0; i < raw.Length-3; i++)
            {
                
                  if(raw[i] == '\r' && raw[i+1] == '\n' && raw[i+2] == '\r' && raw[i+3] == '\n')
                {
                    headerEnd = i+4;
                    break;
                }
            }

            if(headerEnd != -1)  break;
            
            if(memorybuffer.Length > MaxHeaderSize)
            {
                
                throw new HTTPException(413,"Request header too large");
            }


        }

          var haederByte =  memorybuffer.ToArray()[..headerEnd];
          var haederText = Encoding.UTF8.GetString(haederByte);
          var contentLength = 0;

          foreach(var line in haederText.Split("\r\n")){

            if (line.StartsWith("Content-Length", StringComparison.OrdinalIgnoreCase))
            {
                _ = int.TryParse(line["Content-Length".Length..].Trim(),out contentLength);
            }
          
          }

          byte[] bodyByte = [];

        if (contentLength > 0)
        {
            
            if(contentLength > MaxBodySize)
            {
                throw new HTTPException(413,"Request body too large");
            }

            using var bodyBuffer = new MemoryStream();

            var alreadyRead = (int) memorybuffer.Length-headerEnd;

            if(alreadyRead > 0)
            {
                
                bodyBuffer.Write(bodyBuffer.ToArray(),headerEnd,alreadyRead);
            } 

            var total = alreadyRead;

            while(total < contentLength)
            {
                var n =await stream.ReadAsync(tempBuffer,0,Math.Min(tempBuffer.Length,contentLength-total));

                if(n == 0) throw new EndOfStreamException("clint disconnected during body read");

                bodyBuffer.Write(tempBuffer,0,n);
             
                total += n;   
            }

            bodyByte = bodyBuffer.ToArray();
        }

        return (haederByte,bodyByte);

    }
}