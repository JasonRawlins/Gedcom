using Newtonsoft.Json;

namespace Gedcom.RecordStructures;

// The Gedcom Standard 5.5.1 documentation is at the end of this file.
[JsonConverter(typeof(MultimediaFileReferenceNumberJsonConverter))]
public class MultimediaFileReferenceNumber : RecordStructureBase
{
    public MultimediaFileReferenceNumber() { }
    public MultimediaFileReferenceNumber(Record record) : base(record) { }

    public MultimediaFormat MultimediaFormat => First<MultimediaFormat>(Tag.Format);

    public override string ToString() => $"{Record.Value}";
}

internal class MultimediaFileReferenceNumberJsonConverter : JsonConverter<MultimediaFileReferenceNumber>
{
    public override MultimediaFileReferenceNumber? ReadJson(JsonReader reader, Type objectType, MultimediaFileReferenceNumber? existingValue, bool hasExistingValue, JsonSerializer serializer) => throw new NotImplementedException();

    public override void WriteJson(JsonWriter writer, MultimediaFileReferenceNumber? multimediaFileReferenceNumber, JsonSerializer serializer)
    {
        if (multimediaFileReferenceNumber == null) throw new ArgumentNullException(nameof(multimediaFileReferenceNumber));

        serializer.Serialize(writer, new MultimediaFileReferenceNumberJson(multimediaFileReferenceNumber));
    }
}

public class MultimediaFileReferenceNumberJson(MultimediaFileReferenceNumber multimediaFileReferenceNumber) : GedcomJson
{
    public MultimediaFormatJson? MultiMediaFormat { get; set; } = JsonRecord(new MultimediaFormatJson(multimediaFileReferenceNumber.MultimediaFormat));
}

#region STRUCTURE_NAME p. 37
/* 

n OBJE
    +1 FILE <MULTIMEDIA_FILE_REFN> {1:M} p.54
        +2 FORM <MULTIMEDIA_FORMAT> {1:1} p.54

*/
#endregion