namespace Gedcom.RecordStructures;

// The Gedcom Standard 5.5.1 documentation is at the end of this file.
public class HeaderGedcom : RecordStructureBase
{
    public HeaderGedcom() : base() { }
    public HeaderGedcom(Record record) : base(record) { }

    private string? _gedcomForm = null;
    public string GedcomForm => _gedcomForm ??= GetValue(Tag.Format);

    private string? _versionNumber = null;
    public string VersionNumber => _versionNumber ??= GetValue(Tag.Version);

    public override string ToString() => $"{Record.Value}, {VersionNumber}";
}

#region STRUCTURE_NAME p. 23
/* 

GEDC {GEDCOM}:=

1 GEDC {1:1}
    2 VERS <VERSION_NUMBER> {1:1} p.64
    2 FORM <GEDCOM_FORM> {1:1} p.50

Information about the use of GEDCOM in a transmission.

*/
#endregion