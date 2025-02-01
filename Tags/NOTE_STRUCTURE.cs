using System.Text.Json;
using System.Text.Json.Serialization;

namespace Gedcom.Tags;

[JsonConverter(typeof(NOTEJsonConverter))]
public class NOTE_STRUCTURE : TagBase
{
    public NOTE_STRUCTURE(Record record) : base(record) { }

    public string Text
    {
        get
        {
            var text = Record.Value;

            foreach (var contText in Record.Records.Where(r => r.Tag.Equals(C.CONT) || r.Tag.Equals(C.CONC)))
            {
                text += contText;
            }

            return text;
        }
    }

    public string XRef => Record.Value;
}

public class NOTE_STRUCTUREJsonConverter : JsonConverter<NOTE_STRUCTURE>
{
    public override NOTE_STRUCTURE? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException();
    
    public override void Write(Utf8JsonWriter writer, NOTE_STRUCTURE note, JsonSerializerOptions options)
    {
        var jsonObject = new
        {
            note.Text,
            note.XRef
        };

        JsonSerializer.Serialize(writer, jsonObject, options);
    }
}

#region STRUCTURE_NAME (NOTE) p. 37
/* 
https://gedcom.io/specifications/ged551.pdf

NOTE_STRUCTURE:=
[
n NOTE @<XREF:NOTE>@ {1:1} p.27
|
n NOTE [<SUBMITTER_TEXT> | <NULL>] {1:1} p.63
+1 [CONC|CONT] <SUBMITTER_TEXT> {0:M}
]

Note: There are special considerations required when using the CONC tag. The usage is to provide a
note string that can be concatenated together so that the display program can do its own word
wrapping according to its display window size. The requirement for usage is to either break the text
line in the middle of a word, or if at the end of a word, to add a space to the first of the next CONC
line. Otherwise most operating systems will strip off the trailing space and the space is lost in the
reconstitution of the note. 
*/
#endregion