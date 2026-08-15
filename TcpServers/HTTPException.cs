namespace web_api.TcpServers;

public class HTTPException : Exception
{
    public int StatusCode { get; }

    public HTTPException(int code, string message)
        : base(message)
    {
        StatusCode = code;
    }
}