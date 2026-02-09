using System.Text.Json;
using System.Text.Json.Serialization;

namespace Gedcom.RecordStructures;

// The Gedcom Standard 5.5.1 documentation is at the end of this file.
[JsonConverter(typeof(FormRecordJsonConverter))]
public class FormRecord : RecordStructureBase
{
    public FormRecord() : base() { }
    public FormRecord(Record record) : base(record) { }

    private string? _mediaType = null;
    public string MediaType => _mediaType ??= GetValue(ExtensionTag.MediaType);

    private string? _sourceType = null;
    public string SourceType => _sourceType ??= GetValue(ExtensionTag.SourceType);

    private string? _type = null;
    public string Type => _type ??= GetValue(Tag.Type);

    public override string ToString() => $"{Type}";
}

internal sealed class FormRecordJsonConverter : JsonConverter<FormRecord>
{
    public override FormRecord? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException();

    public override void Write(Utf8JsonWriter writer, FormRecord value, JsonSerializerOptions options)
    {
        ArgumentNullException.ThrowIfNull(value);
        JsonSerializer.Serialize(writer, new FormDto(value), GedcomDto.SerializationOptions);
    }
}

public class FormDto(FormRecord formRecord) : GedcomDto
{
    public string? MediaType { get; set; } = String(formRecord.MediaType);
    public string? SourceType { get; set; } = String(formRecord.SourceType);
    public string? Type { get; set; } = String(formRecord.Type);
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