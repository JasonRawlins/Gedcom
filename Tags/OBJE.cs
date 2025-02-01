using System.Text.Json;
using System.Text.Json.Serialization;

namespace Gedcom.Tags;

[JsonConverter(typeof(OBJEJsonConverter))]
public class OBJE : TagBase
{
    public OBJE(Record record) : base(record) { }

    public string FILE => V(C.FILE);
    public string FORM => V(C.FORM);
    public string MEDI => V(C.MEDI);
    public string TITL => V(C.TITL);
    public string XREF => Record.Value;
}

public class OBJEJsonConverter : JsonConverter<OBJE>
{
    public override OBJE? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException();
    
    public override void Write(Utf8JsonWriter writer, OBJE obje, JsonSerializerOptions options)
    {
        var jsonObject = new
        {
            Id = obje.XREF,
            File = obje.FILE,
            MediaFormat = obje.MEDI,
            Title = obje.TITL
        };

        JsonSerializer.Serialize(writer, jsonObject, options);
    }
}

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