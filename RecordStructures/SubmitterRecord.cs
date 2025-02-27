namespace Gedcom.RecordStructures;

public class SubmitterRecord : RecordStructureBase
{
    public SubmitterRecord() : base() { }
    public SubmitterRecord(Record record) : base(record) { }

    public string SubmitterName => _(C.NAME);
    public AddressStructure AddressStructure => FirstOrDefault<AddressStructure>(C.ADDR);
    public List<MultimediaLink> MultimediaLink => List<MultimediaLink>(C.MEDI);
    public List<string> LanguagePreferences => List(r => r.Tag.Equals(C.LANG)).Select(r => r.Value).ToList();
    public string SubmitterRegisteredRfn => _(C.RFN);
    public string AutomatedRecordId => _(C.RIN);
    public List<NoteStructure> NoteStructures => List<NoteStructure>(C.NOTE);
    public Date ChangeDate => FirstOrDefault<Date>(C.CHAN);
}

#region SUBMITTER_RECORD p. 28-29
/* 

SUBMITTER_RECORD:=

n @<XREF:SUBM>@ SUBM {1:1}
    +1 NAME <SUBMITTER_NAME> {1:1} p.63
    +1 <<ADDRESS_STRUCTURE>> {0:1}* p.31
    +1 <<MULTIMEDIA_LINK>> {0:M} p.37, 26
    +1 LANG <LANGUAGE_PREFERENCE> {0:3} p.51
    +1 RFN <SUBMITTER_REGISTERED_RFN> {0:1} p.63
    +1 RIN <AUTOMATED_RECORD_ID> {0:1} p.43
    +1 <<NOTE_STRUCTURE>> {0:M} p.37
    +1 <<CHANGE_DATE>> {0:1} p.31

The submitter record identifies an individual or organization that contributed information contained
in the GEDCOM transmission. All records in the transmission are assumed to be submitted by the
SUBMITTER referenced in the HEADer, unless a SUBMitter reference inside a specific record
points at a different SUBMITTER record.

* Note: submissions to the ancestral file require the name and address of the submitter.

*/
#endregion