namespace Gedcom.RecordStructures;

// The Gedcom Standard 5.5.1 documentation is at the end of this file.
public class CallNumber : RecordStructureBase
{
    public CallNumber() : base() { }
    public CallNumber(Record record) : base(record) { }

    private string? _sourceMediaType = null;
    public string SourceMediaType => _sourceMediaType ??= GetValue(Tag.Media);

    public override string ToString() => $"{Record.Value}, {SourceMediaType}";
}

public class CallNumberDto(CallNumber callNumber) : GedcomDto
{
    public string? SourceMediaType { get; set; } = GetString(callNumber.SourceMediaType);
    public override string ToString() => $"{SourceMediaType}";
}

#region CALL_NUMBER p. 40
/* 

CALN {CALL_NUMBER}:=

n REPO [ @XREF:REPO@ | <NULL>] {1:1} p.27
    +1 CALN <SOURCE_CALL_NUMBER> {0:M} p.61
        +2 MEDI <SOURCE_MEDIA_TYPE> {0:1} p.62

The number used by a repository to identify the specific items in its collections.

*/
#endregion