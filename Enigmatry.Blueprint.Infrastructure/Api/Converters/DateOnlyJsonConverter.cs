using System.Text.Json;
using System.Text.Json.Serialization;

namespace Enigmatry.Blueprint.Infrastructure.Api.Converters;

public sealed class DateOnlyJsonConverter : JsonConverter<DateOnly>
{
    public override DateOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return DateOnly.Parse(reader.GetString()!);
    }

    public override void Write(Utf8JsonWriter writer, DateOnly value, JsonSerializerOptions options)
    {
        var iso8601 = value.ToString("O");
        writer.WriteStringValue(iso8601);
    }
}
