using Gedcom.Core;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Gedcom.RecordStructures;

// The Gedcom Standard 5.5.1 documentation is at the end of this file.
[JsonConverter(typeof(FormRecordJsonConverter))]
public class FormRecord : RecordStructureBase
{
    public FormRecord() : base() { }
    public FormRecord(Record record) : base(record) { }

    public string MediaType => GetValue(ExtensionTag.MediaType);
    public string SourceType => GetValue(ExtensionTag.SourceType);
    public string Type => GetValue(Tag.Type);

    public override string ToString() => $"{Type}";
}

internal sealed class FormRecordJsonConverter : JsonConverter<FormRecord>
{
    public override FormRecord? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException();

    public override void Write(Utf8JsonWriter writer, FormRecord value, JsonSerializerOptions options)
    {
        ArgumentNullException.ThrowIfNull(value);
        JsonSerializer.Serialize(writer, new FormJson(value), options);
    }
}

public class FormJson(FormRecord formRecord) : GedcomJson
{
    public string? MediaType { get; set; } = JsonString(formRecord.MediaType);
    public string? SourceType { get; set; } = JsonString(formRecord.SourceType);
    public string? Type { get; set; } = JsonString(formRecord.Type);
    public override string ToString() => $"{Type}";
}

#region MULTIMEDIA_FORMAT p. 26
/* 

n @XREF:OBJE@ OBJE {1:1}
    +1 FILE <MULTIMEDIA_FILE_REFN> {1:M} p.54
        +2 FORM <MULTIMEDIA_FORMAT> {1:1} p.54
            +3 TYPE <SOURCE_MEDIA_TYPE> {0:1} p.62

*/
#endregion