namespace Gedcom.RecordStructures;

public class FamilyEventStructure : RecordStructureBase
{
    public FamilyEventStructure() { }
    public FamilyEventStructure(Record record) : base(record) { }

    public FamilyEventDetail FamilyEventDetail => FirstOrDefault<FamilyEventDetail>(Record.Tag);
}

#region FAMILY_EVENT_STRUCTURE p. 32
/* 

FAMILY_EVENT_STRUCTURE:=

[
n [ ANUL | CENS | DIV | DIVF ] {1:1}
    +1 <<FAMILY_EVENT_DETAIL>> {0:1} p.32
|
n [ ENGA | MARB | MARC ] {1:1}
    +1 <<FAMILY_EVENT_DETAIL>> {0:1} p.32
|
n MARR [Y|<NULL>] {1:1}
    +1 <<FAMILY_EVENT_DETAIL>> {0:1} p.32
|
n [ MARL | MARS ] {1:1}
    +1 <<FAMILY_EVENT_DETAIL>> {0:1} p.32
|
n RESI
    +1 <<FAMILY_EVENT_DETAIL>> {0:1} p.32
|
n EVEN [<EVENT_DESCRIPTOR> | <NULL>] {1:1} p.48
    +1 <<FAMILY_EVENT_DETAIL>> {0:1} p.32
]

*/
#endregion