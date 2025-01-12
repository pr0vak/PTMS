using System.Net;

namespace Api.Models;

public class ServerResponse
{
    public bool IsSuccess { get; set; }
    public HttpStatusCode StatusCode { get; set; }
    public object Result { get; set; }
    public List<string> ErrorMessages { get; set; }

    public ServerResponse()
    {
        IsSuccess = true;
        ErrorMessages = [];
    }
}