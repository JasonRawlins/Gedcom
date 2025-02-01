using System.Text.Json;
using System.Text.Json.Serialization;

namespace Gedcom.Tags;

[JsonConverter(typeof(NOTEJsonConverter))]
public class NOTE : TagBase
{
    public NOTE(Record record) : base(record) { }

    public CHAN? CHAN
    {
        get
        {
            var chanRecord = Record.Records.First(r => r.Tag.Equals(C.CHAN));

            if (chanRecord != null)
            {
                return new CHAN(chanRecord);
            }

            return null;
        }
    }

    public string Text
    {
        get
        {
            var text = Record.Value;

            foreach (var cont in Record.Records.Where(r => r.Tag.Equals(C.CONT) || r.Tag.Equals(C.CONC)))
            {
                text += cont;
            }

            return text;
        }

    }

    public string XRef => Record.Value;
}

public class NOTEJsonConverter : JsonConverter<NOTE>
{
    public override NOTE? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException();
    
    public override void Write(Utf8JsonWriter writer, NOTE NOTE, JsonSerializerOptions options)
    {
        var jsonObject = new
        {
        };

        JsonSerializer.Serialize(writer, jsonObject, options);
    }
}

#region STRUCTURE_NAME (NOTE) p. 27
/* 
https://gedcom.io/specifications/ged551.pdf

NOTE_RECORD:=

n @<XREF:NOTE>@ NOTE <SUBMITTER_TEXT> {1:1} p.63
    +1 [CONC|CONT] <SUBMITTER_TEXT> {0:M}
    +1 REFN <USER_REFERENCE_NUMBER> {0:M} p.63, 64
        +2 TYPE <USER_REFERENCE_TYPE> {0:1} p.64
    +1 RIN <AUTOMATED_RECORD_ID> {0:1} p.43
    +1 <<SOURCE_CITATION>> {0:M} p.39
    +1 <<CHANGE_DATE>> {0:1} p.31


*/
#endregion