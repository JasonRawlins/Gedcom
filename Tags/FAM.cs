using System.Text.Json;
using System.Text.Json.Serialization;

namespace Gedcom.Tags;

[JsonConverter(typeof(FAMJsonConverter))]
public class FAM : TagBase
{
    public FAM(Record record) : base(record) { }

    public List<Record> Partners => List(r => r.Tag.Equals(T.WIFE) || r.Tag.Equals(T.HUSB));

    public override string ToString() => $"({string.Join(',', Partners)})";
}

public class FAMJsonConverter : JsonConverter<FAM>
{
    public override FAM? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException();

    public override void Write(Utf8JsonWriter writer, FAM fam, JsonSerializerOptions options)
    {
        var famJsonObject = new
        {
            fam.Partners
        };

        JsonSerializer.Serialize(writer, famJsonObject, options);
    }
}