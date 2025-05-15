using Newtonsoft.Json;

namespace Gedcom.RecordStructures;

// The Gedcom Standard 5.1.1 documentation is at the end of this file.
[JsonConverter(typeof(MultimediaLinkJsonConverter))]
public class MultimediaLink : RecordStructureBase
{
    public MultimediaLink() : base() { }
    public MultimediaLink(Record record) : base(record) { }

    public string DescriptiveTitle => _(C.TITL);
    public List<MultimediaFileReferenceNumber> MultimediaFileReferenceNumbers => List<MultimediaFileReferenceNumber>(C.FILE);
    public string SourceMediaType => _(C.MEDI);
    public string Xref => Record.Value;

    public override string ToString() => $"{Record.Value}, {SourceMediaType}, {DescriptiveTitle}";
}

internal class MultimediaLinkJsonConverter : JsonConverter<MultimediaLink>
{
    public override MultimediaLink? ReadJson(JsonReader reader, Type objectType, MultimediaLink? existingValue, bool hasExistingValue, JsonSerializer serializer) => throw new NotImplementedException();

    public override void WriteJson(JsonWriter writer, MultimediaLink? multimediaLink, JsonSerializer serializer)
    {
        if (multimediaLink == null) throw new ArgumentNullException(nameof(multimediaLink));

        serializer.Serialize(writer, new MultimediaLinkJson(multimediaLink));
    }
}

internal class MultimediaLinkJson : GedcomJson
{
    public MultimediaLinkJson(MultimediaLink multimediaLink)
    {
        DescriptiveTitle = JsonString(multimediaLink.DescriptiveTitle);
        MultimediaFileReferenceNumbers = JsonList(multimediaLink.MultimediaFileReferenceNumbers);
        SourceMediaType = JsonString(multimediaLink.SourceMediaType);
        Xref = multimediaLink.Xref;
    }

    public string? DescriptiveTitle { get; set; }
    public List<MultimediaFileReferenceNumber>? MultimediaFileReferenceNumbers { get; set; }
    public string? SourceMediaType { get; set; }
    public string? Xref { get; set; }
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