namespace Gedcom.RecordStructure;

public class NoteStructure : RecordStructureBase
{
    public NoteStructure() : base() { }
    public NoteStructure(Record record) : base(record) { }
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