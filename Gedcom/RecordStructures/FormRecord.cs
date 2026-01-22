using Newtonsoft.Json;

namespace Gedcom.RecordStructures;

// The Gedcom Standard 5.5.1 documentation is at the end of this file.
[JsonConverter(typeof(FormJsonConverter))]
public class FormRecord : RecordStructureBase
{
    public FormRecord() : base() { }
    public FormRecord(Record record) : base(record) { }

    public string Type => GetValue(Tag.Type);

    public override string ToString() => $"{Type}";
}

internal class FormJsonConverter : JsonConverter<FormRecord>
{
    public override FormRecord? ReadJson(JsonReader reader, Type objectType, FormRecord? existingValue, bool hasExistingValue, JsonSerializer serializer) => throw new NotImplementedException();

    public override void WriteJson(JsonWriter writer, FormRecord? formRecord, JsonSerializer serializer)
    {
        if (formRecord == null) throw new ArgumentNullException(nameof(formRecord));

        serializer.Serialize(writer, new FormJson(formRecord));
    }
}

internal class FormJson : GedcomJson
{
    public FormJson(FormRecord formRecord)
    {
        Type = JsonString(formRecord.Type);
    }

    public string? Type { get; set; }
}

#region MULTIMEDIA_FORMAT p. 26
/* 

n @XREF:OBJE@ OBJE {1:1}
    +1 FILE <MULTIMEDIA_FILE_REFN> {1:M} p.54
        +2 FORM <MULTIMEDIA_FORMAT> {1:1} p.54
            +3 TYPE <SOURCE_MEDIA_TYPE> {0:1} p.62

*/
#endregion