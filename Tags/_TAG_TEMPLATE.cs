using System.Text.Json;
using System.Text.Json.Serialization;

namespace Gedcom.Tags;

[JsonConverter(typeof(_TAGNAME_JsonConverter))]
public class _TAGNAME_ : TagBase
{
    public _TAGNAME_(Record record) : base(record) { }
}

public class _TAGNAME_JsonConverter : JsonConverter<_TAGNAME_>
{
    public override _TAGNAME_? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException();
    
    public override void Write(Utf8JsonWriter writer, _TAGNAME_ _TAGNAME_, JsonSerializerOptions options)
    {
        var jsonObject = new
        {
        };

        JsonSerializer.Serialize(writer, jsonObject, options);
    }
}

#region STRUCTURE_NAME (_TAGNAME_) p. 
/* 
https://gedcom.io/specifications/ged551.pdf


*/
#endregion