using Gedcom.Entities;

namespace Gedcom.RecordStructures;

// The Gedcom Standard 5.5.1 documentation is at the end of this file.
public class FamilyEventStructure : EventStructure
{
    public FamilyEventStructure() { }
    public FamilyEventStructure(Record record) : base(record) { }

    public override EventType EventType => EventType.Family;

    public static bool IsFamilyEventStructure(Record record)
    {
        var familyEventTags = new string[]
        {
                Tag.Annulment,
                Tag.Census,
                Tag.Divorce,
                Tag.DivorceFiled,
                Tag.Engagement,
                Tag.MarriageBann,
                Tag.MarriageContract,
                Tag.Marriage,
                Tag.MarriageLicense,
                Tag.MarriageSettlement,
                Tag.Residence
        };

        return familyEventTags.Contains(record.Tag);
    }
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