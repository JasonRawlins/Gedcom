using Gedcom.Core;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Gedcom.RecordStructures;

[JsonConverter(typeof(NoteStructureJsonConverter))]
public class NoteStructure : RecordStructureBase
{
    public NoteStructure() : base() { }
    public NoteStructure(Record record) : base(record) { }

    public string Text
    {
        get
        {
            var text = new StringBuilder();
            text.Append(Record.Value);
            var contAndConc = Record.Records.Where(r => r.Tag.Equals(C.CONT) || r.Tag.Equals(C.CONC)).ToList();
            contAndConc.ForEach(r => text.Append(r.Value));

            return text.ToString();
        }
    }
}

internal class NoteStructureJsonConverter : JsonConverter<NoteStructure>
{
    public override NoteStructure? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException();
    public override void Write(Utf8JsonWriter writer, NoteStructure noteStructure, JsonSerializerOptions options)
    {
        var noteStructureJson = new NoteStructureJson(noteStructure);
        JsonSerializer.Serialize(writer, noteStructureJson, noteStructureJson.GetType(), options);
    }
}

internal class NoteStructureJson
{
    public NoteStructureJson(NoteStructure noteStructure)
    {
        Text = noteStructure.Text;
    }

    public string Text { get; set; }
}

#region NOTE_STRUCTURE p. 37
/* 

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