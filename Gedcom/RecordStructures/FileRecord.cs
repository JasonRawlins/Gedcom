using Newtonsoft.Json;

namespace Gedcom.RecordStructures;

// The Gedcom Standard 5.5.1 documentation is at the end of this file.
[JsonConverter(typeof(FileJsonConverter))]
public class FileRecord : RecordStructureBase
{
    public FileRecord() : base() { }
    public FileRecord(Record record) : base(record) { }

    public FormRecord FormRecord => First<FormRecord>(Tag.Format);
    public string Title => GetValue(Tag.Title);

    public override string ToString() => $"{Title}";
}

internal class FileJsonConverter : JsonConverter<FileRecord>
{
    public override FileRecord? ReadJson(JsonReader reader, Type objectType, FileRecord? existingValue, bool hasExistingValue, JsonSerializer serializer) => throw new NotImplementedException();

    public override void WriteJson(JsonWriter writer, FileRecord? fileRecord, JsonSerializer serializer)
    {
        if (fileRecord == null) throw new ArgumentNullException(nameof(fileRecord));

        serializer.Serialize(writer, new FileJson(fileRecord));
    }
}

internal class FileJson : GedcomJson
{
    public FileJson(FileRecord fileRecord)
    {
        Title = JsonString(fileRecord.Title);
    }

    public string? Title { get; set; }
}

#region MULTIMEDIA_FILE_REFN p. 26
/* 

n @XREF:OBJE@ OBJE {1:1}
    +1 FILE <MULTIMEDIA_FILE_REFN> {1:M} p.54

*/
#endregion