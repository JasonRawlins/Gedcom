using Gedcom.Core;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Gedcom.RecordStructures;

[JsonConverter(typeof(MultimediaFileReferenceNumberJsonConverter))]
public class MultimediaFileReferenceNumber : RecordStructureBase
{
    public MultimediaFileReferenceNumber() { }
    public MultimediaFileReferenceNumber(Record record) : base(record) { }

    public MultimediaFormat MultiMediaFormat => First<MultimediaFormat>(C.FORM);
}

internal class MultimediaFileReferenceNumberJsonConverter : JsonConverter<MultimediaFileReferenceNumber>
{
    public override MultimediaFileReferenceNumber? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException();
    public override void Write(Utf8JsonWriter writer, MultimediaFileReferenceNumber multimediaFileReferenceNumber, JsonSerializerOptions options)
    {
        var multimediaFileReferenceNumberJson = new MultimediaFileReferenceNumberJson(multimediaFileReferenceNumber);
        JsonSerializer.Serialize(writer, multimediaFileReferenceNumberJson, multimediaFileReferenceNumberJson.GetType(), options);
    }
}

internal class MultimediaFileReferenceNumberJson : GedcomJson
{
    public MultimediaFileReferenceNumberJson(MultimediaFileReferenceNumber multimediaFileReferenceNumber)
    {
        MultiMediaFormat = JsonRecord(multimediaFileReferenceNumber.MultiMediaFormat);
    }

    public MultimediaFormat? MultiMediaFormat { get; set; }
}

#region STRUCTURE_NAME p. 37
/* 

n OBJE
    +1 FILE <MULTIMEDIA_FILE_REFN> {1:M} p.54
        +2 FORM <MULTIMEDIA_FORMAT> {1:1} p.54

*/
#endregion