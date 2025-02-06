namespace Gedcom.RecordStructures;

public class AssociationStructure : RecordStructureBase
{
    public AssociationStructure() : base() { }
    public AssociationStructure(Record record) : base(record) { }

    public string RelationIsDescriptor => V(C.RELA);
    public SourceCitation? SourceCitation { get; }
    public NoteStructure? NoteStructure { get; }
}

#region ASSOCIATION_STRUCTURE p. 31 
/* 

ASSOCIATION_STRUCTURE:=

n ASSO @<XREF:INDI>@ {1:1} p.25
    +1 RELA <RELATION_IS_DESCRIPTOR> {1:1} p.60
    +1 <<SOURCE_CITATION>> {0:M} p.39
    +1 <<NOTE_STRUCTURE>> {0:M} p.37

The association pointer only associates INDIvidual records to INDIvidual records.

*/
#endregion