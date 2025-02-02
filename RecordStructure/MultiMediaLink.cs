namespace Gedcom.RecordStructure;

//[JsonConverter(typeof(MultiMediaLinkJsonConverter))]
public class MultiMediaLink : RecordStructureBase
{
    public MultiMediaLink() : base() { }
    public MultiMediaLink(Record record) : base(record) { }

    public string File => V(C.FILE);
    public string Form => V(C.FORM);
    public string SourceMediaType => V(C.MEDI);
    public string Title => V(C.TITL);
}

//public class MultiMediaLinkJsonConverter : JsonConverter<MultiMediaLink>
//{
//    public override MultiMediaLink? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException();
    
//    public override void Write(Utf8JsonWriter writer, MultiMediaLink obje, JsonSerializerOptions options)
//    {
//        var jsonObject = new
//        {
//            obje.File,
//            Id = obje.Xref,
//            obje.SourceMediaType,
//            obje.Title
//        };

//        JsonSerializer.Serialize(writer, jsonObject, options);
//    }
//}

#region MULTIMEDIA_LINK (OBJE) p. 37
/* 
https://gedcom.io/specifications/ged551.pdf

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