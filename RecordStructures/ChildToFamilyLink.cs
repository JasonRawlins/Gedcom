namespace Gedcom.RecordStructures;

public class ChildToFamilyLink : RecordStructureBase
{
    public ChildToFamilyLink() : base() { }
    public ChildToFamilyLink(Record record) : base(record) { }

    public string ChildLinkageStatus => V(C.STAT);
    public string PedigreeLinkageType => V(C.PEDI);
    public List<NoteStructure>? NoteStructure => List<NoteStructure>(C.NOTE);
}

#region CHILD_TO_FAMILY_LINK p. 31-32
/* 

CHILD_TO_FAMILY_LINK:=

n FAMC @<XREF:FAM>@ {1:1} p.24
    +1 PEDI <PEDIGREE_LINKAGE_TYPE> {0:1} p.57
    +1 STAT <CHILD_LINKAGE_STATUS> {0:1} p.44
    +1 <<NOTE_STRUCTURE>> {0:M} p.37

*/
#endregion