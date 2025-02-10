namespace Gedcom.RecordStructures;

public class ChildToFamilyLink : RecordStructureBase
{
    public ChildToFamilyLink() : base() { }
    public ChildToFamilyLink(Record record) : base(record) { }

    public string PedigreeLinkageType => V(C.PEDI);
    public string ChildLinkageStatus => V(C.STAT);
    public List<NoteStructure> NoteStructures => List<NoteStructure>(C.NOTE);
    public string AdoptedByWhichParent => First(C.ADOP).Value;
}

#region CHILD_TO_FAMILY_LINK p. 31-32
/* 

CHILD_TO_FAMILY_LINK:=

n FAMC @<XREF:FAM>@ {1:1} p.24
    +1 PEDI <PEDIGREE_LINKAGE_TYPE> {0:1} p.57
    +1 STAT <CHILD_LINKAGE_STATUS> {0:1} p.44
    +1 <<NOTE_STRUCTURE>> {0:M} p.37

[Editor] Possible errata in The Gedcom Standard 5.1.1 The ADOP tag on page 34 
has an structure that looks like this: 

    n ADOP {1:1}
    +1 <<INDIVIDUAL_EVENT_DETAIL>> {0:1}* p.34
    +1 FAMC @<XREF:FAM>@ {0:1} p.24
        +2 ADOP<ADOPTED_BY_WHICH_PARENT> {0:1}

However,the ADOP line is missing from FAMC structure on page 32. I'm guessing 
that may have been a minor ommission. I'm adding ADOP to this class.

*/
#endregion