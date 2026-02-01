using Gedcom.Core;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Gedcom.RecordStructures;

// The Gedcom Standard 5.5.1 documentation is at the end of this file.
[JsonConverter(typeof(FileRecordJsonConverter))]
public class FileRecord : RecordStructureBase
{
    public FileRecord() : base() { }
    public FileRecord(Record record) : base(record) { }

    private FormRecord? formRecord = null;
    public FormRecord FormRecord => formRecord ??= First<FormRecord>(Tag.Format);

    private string? title = null;
    public string Title => title ??= GetValue(Tag.Title);

    public override string ToString() => $"{Title}";
}

internal sealed class FileRecordJsonConverter : JsonConverter<FileRecord>
{
    public override FileRecord? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException();

    public override void Write(Utf8JsonWriter writer, FileRecord value, JsonSerializerOptions options)
    {
        ArgumentNullException.ThrowIfNull(value);
        JsonSerializer.Serialize(writer, new FileJson(value), options);
    }
}

public class FileJson(FileRecord fileRecord) : GedcomJson
{
    public FormJson? Form { get; set; } = JsonRecord(new FormJson(fileRecord.FormRecord));
    public string? Title { get; set; } = JsonString(fileRecord.Title);
    public override string ToString() => $"{Title}";
}

#region MULTIMEDIA_FILE_REFN p. 26
/* 

n @XREF:OBJE@ OBJE {1:1}
    +1 FILE <MULTIMEDIA_FILE_REFN> {1:M} p.54

*/
#endregion