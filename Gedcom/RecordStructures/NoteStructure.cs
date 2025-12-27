using System.Text;
using Newtonsoft.Json;

namespace Gedcom.RecordStructures;

// The Gedcom Standard 5.5.1 documentation is at the end of this file.
[JsonConverter(typeof(NoteJsonConverter))]
public class NoteStructure : RecordStructureBase
{
    public NoteStructure() : base() { }
    public NoteStructure(Record record) : base(record) { }

    // A NoteStructure might only have a value. However, the max length of that value is
    // 255 characters. If the value is greater than 255 characters, the record may also have
    // the sub records CONC (Concatenate) or CONT (Continue). The Value, CONC, and CONT
    // values must be merged together to get the full text for a NoteStructure.
    public string Text
    {
        get
        {
            var text = new StringBuilder();
            text.Append(Record.Value);
            var continueOrConcatenate = Record.Records.Where(r => r.Tag.Equals(Tag.Continue) || r.Tag.Equals(Tag.Concatenation)).ToList();
            continueOrConcatenate.ForEach(r => {
                if (r.Tag.Equals(Tag.Continue))
                {
                    text.AppendLine(r.Value);
                }

                if (r.Tag.Equals(Tag.Concatenation))
                {
                    text.Append(r.Value);
                }
            });

            return text.ToString();
        }
    }

    public override string ToString()
    {
        var lengthOfSubstring = Text.Length < 64 ? Text.Length : 64;
        return Text.Substring(0, lengthOfSubstring);
    }
}

internal class NoteJsonConverter : JsonConverter<NoteStructure>
{
    public override NoteStructure? ReadJson(JsonReader reader, Type objectType, NoteStructure? existingValue, bool hasExistingValue, JsonSerializer serializer) => throw new NotImplementedException();

    public override void WriteJson(JsonWriter writer, NoteStructure? noteStructure, JsonSerializer serializer)
    {
        if (noteStructure == null) throw new ArgumentNullException(nameof(noteStructure));

        serializer.Serialize(writer, new NoteJson(noteStructure));
    }
}

internal class NoteJson : GedcomJson
{
    public NoteJson(NoteStructure noteStructure)
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