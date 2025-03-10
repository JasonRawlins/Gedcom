namespace Gedcom.RecordStructures;

public class LdsIndividualOrdinance : RecordStructureBase
{
    public LdsIndividualOrdinance() : base() { }
    public LdsIndividualOrdinance(Record record) : base(record) { }

    public string DateLdsOrdinance => _(C.DATE);
    public string TempleCode => _(C.TEMP);
    public string PlaceLivingOrdinance => _(C.PLAC);
    public LdsOrdinanceStatus LdsBaptismDateStatus => First<LdsOrdinanceStatus>(C.STAT);
    public List<NoteStructure> NoteStructures => List<NoteStructure>(C.NOTE);
    public List<SourceCitation> SourceCitations => List<SourceCitation>(C.SOUR);
}

#region LDS_INDIVIDUAL_ORDINANCE p. 
/* 

LDS_INDIVIDUAL_ORDINANCE:=
[
n [ BAPL | CONL ] {1:1}
    +1 DATE <DATE_LDS_ORD> {0:1} p.46
    +1 TEMP <TEMPLE_CODE> {0:1} p.63
    +1 PLAC <PLACE_LIVING_ORDINANCE> {0:1} p.58
    +1 STAT <LDS_BAPTISM_DATE_STATUS> {0:1} p.51
        +2 DATE <CHANGE_DATE> {1:1} p.44
    +1 <<NOTE_STRUCTURE>> {0:M} p.37
    +1 <<SOURCE_CITATION>> {0:M} p.39
|
n ENDL {1:1}
    +1 DATE <DATE_LDS_ORD> {0:1} p.46
    +1 TEMP <TEMPLE_CODE> {0:1} p.63
    +1 PLAC <PLACE_LIVING_ORDINANCE> {0:1} p.58
    +1 STAT <LDS_ENDOWMENT_DATE_STATUS> {0:1} p.52
        +2 DATE <CHANGE_DATE> {1:1} p.44
    +1 <<NOTE_STRUCTURE>> {0:M} p.37
    +1 <<SOURCE_CITATION>> {0:M} p.39
|
n SLGC {1:1}
    +1 DATE <DATE_LDS_ORD> {0:1} p.46
    +1 TEMP <TEMPLE_CODE> {0:1} p.63
    +1 PLAC <PLACE_LIVING_ORDINANCE> {0:1} p.58
    +1 FAMC @<XREF:FAM>@ {1:1} p.24
    +1 STAT <LDS_CHILD_SEALING_DATE_STATUS> {0:1} p.51
        +2 DATE <CHANGE_DATE> {1:1} p.44
    +1 <<NOTE_STRUCTURE>> {0:M} p.37
    +1 <<SOURCE_CITATION>> {0:M} p.39
]

*/
#endregion