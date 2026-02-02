using System.Text.Json;
using System.Text.Json.Serialization;

namespace Gedcom.RecordStructures;

// The Gedcom Standard 5.5.1 documentation is at the end of this file.
[JsonConverter(typeof(MultimediaFileReferenceNumberJsonConverter))]
public class MultimediaFileReferenceNumber : RecordStructureBase
{
    public MultimediaFileReferenceNumber() { }
    public MultimediaFileReferenceNumber(Record record) : base(record) { }

    private MultimediaFormat? multimediaFormat = null;
    public MultimediaFormat MultimediaFormat => multimediaFormat ??= First<MultimediaFormat>(Tag.Format);

    public override string ToString() => $"{Record.Value}";
}

internal sealed class MultimediaFileReferenceNumberJsonConverter : JsonConverter<MultimediaFileReferenceNumber>
{
    public override MultimediaFileReferenceNumber? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException();

    public override void Write(Utf8JsonWriter writer, MultimediaFileReferenceNumber value, JsonSerializerOptions options)
    {
        ArgumentNullException.ThrowIfNull(value);
        JsonSerializer.Serialize(writer, new MultimediaFileReferenceNumberJson(value), options);
    }
}

public class MultimediaFileReferenceNumberJson(MultimediaFileReferenceNumber multimediaFileReferenceNumber) : GedcomJson
{
    public MultimediaFormatJson? MultimediaFormat { get; set; } = JsonRecord(new MultimediaFormatJson(multimediaFileReferenceNumber.MultimediaFormat));
    public override string ToString() => $"{MultimediaFormat?.SourceMediaType}";
}

#region STRUCTURE_NAME p. 37
/* 

n OBJE
    +1 FILE <MULTIMEDIA_FILE_REFN> {1:M} p.54
        +2 FORM <MULTIMEDIA_FORMAT> {1:1} p.54

*/
#endregion