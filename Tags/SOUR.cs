using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Gedcom.Tags;

[JsonConverter(typeof(SOURJsonConverter))]
public class SOUR : TagBase
{
    public SOUR(Record record) : base(record) { }

    public string ABBR => Value(T.ABBR);
    public string AUTH => Value(T.AUTH);
    public string PUBL => Value(T.PUBL);
    public string REFN => Value(T.REFN);
    public string RIN => Value(T.RIN);
    public string TEXT => Value(T.TEXT);
    public string TITL => Value(T.TITL);
    public string XrefSour => Record.Value;
    public static SOUR? ParseJson(string json)
    {
        throw new NotImplementedException();
    }

    public override string ToString() => $"{TITL} ({AUTH})";
}

public class SOURJsonConverter : JsonConverter<SOUR>
{
    public override SOUR? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => SOUR.ParseJson(reader.GetString());
    
    public override void Write(Utf8JsonWriter writer, SOUR sour, JsonSerializerOptions options)
    {
        var sourJsonObject = new
        {
            Id = sour.XrefSour,
            Publisher = sour.PUBL,
            Rin = sour.RIN,
            Title = sour.TITL
        };

        JsonSerializer.Serialize(writer, sourJsonObject, options);
    }
}

#region The Gedcom Standard SOURCE_RECORD (SOUR)
/*
https://gedcom.io/specifications/ged551.pdf
The Gedcom Standard 
Release 5.5.1
p. 27

SOURCE_RECORD:=

n @<XREF:SOUR>@ SOUR {1:1}
    +1 DATA {0:1}
        +2 EVEN <EVENTS_RECORDED> {0:M} p.50
            +3 DATE <DATE_PERIOD> {0:1} p.46
            +3 PLAC <SOURCE_JURISDICTION_PLACE> {0:1} p.62
        +2 AGNC <RESPONSIBLE_AGENCY> {0:1} p.60
        +2 <<NOTE_STRUCTURE>> {0:M} p.37
    +1 AUTH <SOURCE_ORIGINATOR> {0:1} p.62
        +2 [CONC|CONT] <SOURCE_ORIGINATOR> {0:M} p.62
    +1 TITL <SOURCE_DESCRIPTIVE_TITLE> {0:1} p.62
        +2 [CONC|CONT] <SOURCE_DESCRIPTIVE_TITLE> {0:M} p.62
    +1 ABBR <SOURCE_FILED_BY_ENTRY> {0:1} p.62
    +1 PUBL <SOURCE_PUBLICATION_FACTS> {0:1} p.62
        +2 [CONC|CONT] <SOURCE_PUBLICATION_FACTS> {0:M} p.62
    +1 TEXT <TEXT_FROM_SOURCE> {0:1} p.63
        +2 [CONC|CONT] <TEXT_FROM_SOURCE> {0:M} p.63
    +1 <<SOURCE_REPOSITORY_CITATION>> {0:M} p.40
    +1 REFN <USER_REFERENCE_NUMBER> {0:M} p.63, 64
        +2 TYPE <USER_REFERENCE_TYPE> {0:1} p.64
    +1 RIN <AUTOMATED_RECORD_ID> {0:1} p.43
    +1 <<CHANGE_DATE>> {0:1} p.31
    +1 <<NOTE_STRUCTURE>> {0:M} p.37
    +1 <<MULTIMEDIA_LINK>> {0:M} p.37, 26

Source records are used to provide a bibliographic description of the source cited. (See the
<<SOURCE_CITATION>> structure, page 39, which contains the pointer to this source record.)
*/
#endregion