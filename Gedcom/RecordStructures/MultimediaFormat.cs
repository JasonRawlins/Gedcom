using Newtonsoft.Json;

namespace Gedcom.RecordStructures;

// The Gedcom Standard 5.5.1 documentation is at the end of this file.
[JsonConverter(typeof(MultimediaFormatJsonConverter))]
public class MultimediaFormat : RecordStructureBase
{
    public MultimediaFormat() : base() { }
    public MultimediaFormat(Record record) : base(record) { }

    public string SourceMediaType => GetValue(Tag.Media);

    public override string ToString() => $"{Record.Value}, {SourceMediaType}";
}

internal class MultimediaFormatJsonConverter : JsonConverter<MultimediaFormat>
{
    public override MultimediaFormat? ReadJson(JsonReader reader, Type objectType, MultimediaFormat? existingValue, bool hasExistingValue, JsonSerializer serializer) => throw new NotImplementedException();

    public override void WriteJson(JsonWriter writer, MultimediaFormat? multimediaFormat, JsonSerializer serializer)
    {
        if (multimediaFormat == null) throw new ArgumentNullException(nameof(multimediaFormat));

        serializer.Serialize(writer, new MultimediaFormatJson(multimediaFormat));
    }
}

public class MultimediaFormatJson : GedcomJson
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