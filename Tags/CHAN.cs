using System.Text.Json;
using System.Text.Json.Serialization;

namespace Gedcom.Tags;

[JsonConverter(typeof(CHANJsonConverter))]
public class CHAN : TagBase
{
    public CHAN() { }
    public CHAN(Record record) : base(record) { }

    public string DATE => V(C.DATE);
    public string TIME => V(C.TIME);

    public NOTE_STRUCTURE? NOTE
    {
        get
        {
            var timeRecord = Record.Records.FirstOrDefault(r => r.Tag.Equals(C.NOTE));

            if (timeRecord != null) 
            {
                return new NOTE_STRUCTURE(timeRecord);
            }

            return null;
        }
    }
}

public class CHANJsonConverter : JsonConverter<CHAN>
{
    public override CHAN? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException();
    
    public override void Write(Utf8JsonWriter writer, CHAN chan, JsonSerializerOptions options)
    {
        var jsonObject = new
        {
        };

        JsonSerializer.Serialize(writer, jsonObject, options);
    }
}

#region CHANGE_DATE (CHAN) p. 
/* 
https://gedcom.io/specifications/ged551.pdf

CHANGE_DATE:=

n CHAN {1:1}
    +1 DATE <CHANGE_DATE> {1:1} p.44
        +2 TIME <TIME_VALUE> {0:1} p.63
    +1 <<NOTE_STRUCTURE>> {0:M} p.37
    
    The change date is intended to only record the last change to a record. Some systems may want to
    manage the change process with more detail, but it is sufficient for GEDCOM purposes to indicate
    the last time that a record was modified.
*/
#endregion