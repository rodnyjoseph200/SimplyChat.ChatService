using System.Text.Json;
using Microsoft.Azure.Cosmos;

namespace ChatService.Infrastructure.Azure.CosmosDB;

public class SystemTextJsonCosmosDbSerializer : CosmosSerializer
{
    private readonly JsonSerializerOptions _jsonSerializerOptions;

    public SystemTextJsonCosmosDbSerializer(JsonSerializerOptions jsonSerializerOptions)
    {
        _jsonSerializerOptions = jsonSerializerOptions ?? throw new ArgumentNullException(nameof(jsonSerializerOptions));
    }

    public override T FromStream<T>(Stream stream)
    {
        ArgumentNullException.ThrowIfNull(stream);

        if (typeof(Stream).IsAssignableFrom(typeof(T)))
            return (T)(object)stream;

        var result = JsonSerializer.Deserialize<T>(stream, _jsonSerializerOptions);

        return result is null ?
            throw new JsonException($"Deserialization returned null for type {typeof(T).FullName}") :
            result;
    }

    public override Stream ToStream<T>(T input)
    {
        var stream = new MemoryStream();
        // Serialize the input into the stream
        JsonSerializer.Serialize(stream, input, _jsonSerializerOptions);
        stream.Position = 0;
        return stream;
    }
}