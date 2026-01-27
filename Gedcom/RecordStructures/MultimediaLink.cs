using Gedcom.Core;
using Newtonsoft.Json;

namespace Gedcom.RecordStructures;

// The Gedcom Standard 5.5.1 documentation is at the end of this file.
[JsonConverter(typeof(MultimediaLinkJsonConverter))]
public class MultimediaLink : RecordStructureBase
{
    public MultimediaLink() : base() { }
    public MultimediaLink(Record record) : base(record) { }

    public string DescriptiveTitle => GetValue(Tag.Title);
    public List<MultimediaFileReferenceNumber> MultimediaFileReferenceNumbers => List<MultimediaFileReferenceNumber>(Tag.File);
    public string SourceMediaType => GetValue(Tag.Media);
    public string Xref => Record.Value;

    public override string ToString() => $"{Record.Value}, {SourceMediaType}, {DescriptiveTitle}";
}

internal class MultimediaLinkJsonConverter : JsonConverter<MultimediaLink>
{
    public override MultimediaLink? ReadJson(JsonReader reader, Type objectType, MultimediaLink? existingValue, bool hasExistingValue, JsonSerializer serializer) => throw new NotImplementedException();

    public override void WriteJson(JsonWriter writer, MultimediaLink? multimediaLink, JsonSerializer serializer)
    {
        ArgumentNullException.ThrowIfNull(multimediaLink);
        serializer.Serialize(writer, new MultimediaLinkJson(multimediaLink));
    }
}

public class MultimediaLinkJson(MultimediaLink multimediaLink) : GedcomJson
{
    public string? DescriptiveTitle { get; set; } = JsonString(multimediaLink.DescriptiveTitle);
    public List<MultimediaFileReferenceNumberJson>? MultimediaFileReferenceNumbers { get; set; } = JsonList(multimediaLink.MultimediaFileReferenceNumbers.Select(mfrn => new MultimediaFileReferenceNumberJson(mfrn)).ToList());
    public string? SourceMediaType { get; set; } = JsonString(multimediaLink.SourceMediaType);
    public string? Xref { get; set; } = multimediaLink.Xref;
    public override string ToString() => $"{DescriptiveTitle}";
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