using System.Text.Json;
using System.Text.Json.Serialization;

namespace Gedcom.Tags;

[JsonConverter(typeof(REFNJsonConverter))]
public class REFN : TagBase
{
    public REFN(Record record) : base(record) { }

    public string TYPE => V(C.TYPE);
}

public class REFNJsonConverter : JsonConverter<REFN>
{
    public override REFN? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException();
    
    public override void Write(Utf8JsonWriter writer, REFN refn, JsonSerializerOptions options)
    {
        var jsonObject = new
        {
            Id = refn.Value,
            Type = refn.TYPE
        };

        JsonSerializer.Serialize(writer, jsonObject, options);
    }
}

#region USER_REFERENCE_TYPE (REFN) p. 27
/* 
https://gedcom.io/specifications/ged551.pdf

n @
    +1 REFN <USER_REFERENCE_NUMBER> {0:M} p.63, 64
        +2 TYPE <USER_REFERENCE_TYPE> {0:1} p.64

*/
#endregion