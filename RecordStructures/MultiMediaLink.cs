using System.Text.Json;
using System.Text.Json.Serialization;

namespace Gedcom.RecordStructures;

[JsonConverter(typeof(MultimediaLinkJsonConverter))]
public class MultimediaLink : RecordStructureBase
{
    public MultimediaLink() : base() { }
    public MultimediaLink(Record record) : base(record) { }

    public List<MultimediaFileReferenceNumber> MultimediaFileReferenceNumbers => List<MultimediaFileReferenceNumber>(C.FILE);
    public string SourceMediaType => _(C.MEDI);
    public string DescriptiveTitle => _(C.TITL);
}

internal class MultimediaLinkJsonConverter : JsonConverter<MultimediaLink>
{
    public override MultimediaLink? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException();
    public override void Write(Utf8JsonWriter writer, MultimediaLink multimediaLink, JsonSerializerOptions options)
    {
        var multimediaLinkJson = new MultimediaLinkJson(multimediaLink);
        JsonSerializer.Serialize(writer, multimediaLinkJson, multimediaLinkJson.GetType(), options);
    }
}

internal class MultimediaLinkJson : GedcomJson
{
    public MultimediaLinkJson(MultimediaLink multimediaLink)
    {
        Xref = multimediaLink.Xref;
        MultimediaFileReferenceNumbers = JsonList(multimediaLink.MultimediaFileReferenceNumbers);
        SourceMediaType = JsonString(multimediaLink.SourceMediaType);
        DescriptiveTitle = JsonString(multimediaLink.DescriptiveTitle);
    }

    public List<MultimediaFileReferenceNumber>? MultimediaFileReferenceNumbers { get; set; }
    public string? SourceMediaType { get; set; }
    public string? DescriptiveTitle { get; set; }
}

#region MULTIMEDIA_LINK p. 37
/* 

MULTIMEDIA_LINK: =

n OBJE @<XREF:OBJE>@ {1:1} p.26
|
n OBJE
    +1 FILE <MULTIMEDIA_FILE_REFN> {1:M} p.54
        +2 FORM <MULTIMEDIA_FORMAT> {1:1} p.54
            +3 MEDI <SOURCE_MEDIA_TYPE> {0:1} p.62
    +1 TITL <DESCRIPTIVE_TITLE> {0:1} p.48

Note: some systems may have output the following 5.5 structure. The new context above was
introduced in order to allow a grouping of related multimedia files to a particular context.

*/
#endregion