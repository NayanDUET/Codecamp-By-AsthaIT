namespace web_api.TcpServer;

public class HTTPException : Exception
{
    public int StatusCode { get; }

    public HTTPException(int code, string message)
        : base(message)
    {
        StatusCode = code;
    }
}