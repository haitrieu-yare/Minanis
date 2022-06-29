using System.Text.Json.Serialization;
using Application;

namespace API;

public class CustomResponse
{
    public int StatusCode { get; init; } = 200;
    public string Message { get; init; } = ApiConstants.NoMessage;
    public object? Data { get; init; }
    public Pagination? MetaData { get; init; }
    public List<LinkData>? LinkData { get; init; }
}