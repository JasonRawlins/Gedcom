using System.Text.Json;
using System.Text.Json.Serialization;

namespace Gedcom.RecordStructures;

// The Gedcom Standard 5.5.1 documentation is at the end of this file.
[JsonConverter(typeof(MultimediaFormatJsonConverter))]
public class MultimediaFormat : RecordStructureBase
{
    public MultimediaFormat() : base() { }
    public MultimediaFormat(Record record) : base(record) { }

    private string? _sourceMediaType = null;
    public string SourceMediaType => _sourceMediaType ??= GetValue(Tag.Media);

    public override string ToString() => $"{Record.Value}, {SourceMediaType}";
}

internal sealed class MultimediaFormatJsonConverter : JsonConverter<MultimediaFormat>
{
    public override MultimediaFormat? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException();

    public override void Write(Utf8JsonWriter writer, MultimediaFormat value, JsonSerializerOptions options)
    {
        ArgumentNullException.ThrowIfNull(value);
        JsonSerializer.Serialize(writer, new MultimediaFormatJson(value), options);
    }
}

public class MultimediaFormatJson(MultimediaFormat multimediaFormat) : GedcomJson
{
    public string? SourceMediaType { get; set; } = JsonString(multimediaFormat.SourceMediaType);
    public override string ToString() => $"{SourceMediaType}";
}

#region MULTIMEDIA_FORMAT p. 54
/* 

n OBJE
    +1 FILE <MULTIMEDIA_FILE_REFN> {1:M} p.54
        +2 FORM <MULTIMEDIA_FORMAT> {1:1} p.54
            +3 MEDI <SOURCE_MEDIA_TYPE> {0:1} p.62

*/
#endregion