using Gedcom.Core;

namespace Gedcom.RecordStructures;

// I named this class after the tag name because there is already a class named Gedcom.cs. Find a more appropriate name.
public class GEDC : RecordStructureBase
{
    public GEDC() : base() { }
    public GEDC(Record record) : base(record) { }

    public string VersionNumber => _(C.VERS);
    public string GedcomForm => _(C.FORM);
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