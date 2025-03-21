using System.Text.Json;
using System.Text.Json.Serialization;

namespace Gedcom.RecordStructures;

[JsonConverter(typeof(MultimediaFormatJsonConverter))]
public class MultimediaFormat : RecordStructureBase
{
    public MultimediaFormat() : base() { }
    public MultimediaFormat(Record record) : base(record) { }

    public string SourceMediaType => _(C.MEDI);
}

internal class MultimediaFormatJsonConverter : JsonConverter<MultimediaFormat>
{
    public override MultimediaFormat? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException();
    public override void Write(Utf8JsonWriter writer, MultimediaFormat multimediaFormat, JsonSerializerOptions options)
    {
        var multimediaFormatJson = new MultimediaFormatJson(multimediaFormat);
        JsonSerializer.Serialize(writer, multimediaFormatJson, multimediaFormatJson.GetType(), options);
    }
}

internal class MultimediaFormatJson : GedcomJson
{
    public MultimediaFormatJson(MultimediaFormat multimediaFormat)
    {
        SourceMediaType = JsonString(multimediaFormat.SourceMediaType);
    }

    public string? SourceMediaType { get; set; }
}

#region MULTIMEDIA_FORMAT p. 54
/* 

n OBJE
    +1 FILE <MULTIMEDIA_FILE_REFN> {1:M} p.54
        +2 FORM <MULTIMEDIA_FORMAT> {1:1} p.54
            +3 MEDI <SOURCE_MEDIA_TYPE> {0:1} p.62

*/
#endregion