using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using Application;

namespace WebUI;

[SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
public class CustomResponse
{
    public int StatusCode { get; init; } = 200;
    public string Message { get; init; } = ApiConstants.NoMessage;

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public object? Data { get; init; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public Pagination? MetaData { get; init; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public List<LinkData>? LinkData { get; init; }
}