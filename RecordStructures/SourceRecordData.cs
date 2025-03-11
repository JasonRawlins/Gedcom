using Gedcom.Core;

namespace Gedcom.RecordStructures;

public class SourceRecordData : RecordStructureBase
{
    public SourceRecordData() : base() { }
    public SourceRecordData(Record record) : base(record) { }

    public List<SourceRecordEvent> EventsRecorded => List<SourceRecordEvent>(C.EVEN);
    public string ResponsibleAgency => _(C.AGNC);
    public List<NoteStructure> NoteStructures => List<NoteStructure>(C.NOTE);
}

public class SourceRecordEvent : RecordStructureBase
{
    public SourceRecordEvent() : base() { }
    public SourceRecordEvent(Record record) : base(record) { }

    public string DatePeriod => _(C.DATE);
    public string SourceJurisdictionPlace => _(C.PLAC);

}

#region SOURCE_RECORD (DATA) p. 27
/* 

n @<XREF:SOUR>@ SOUR {1:1}
    1 DATA {0:1}
        2 EVEN <EVENTS_RECORDED> {0:M} p.50
            3 DATE <DATE_PERIOD> {0:1} p.46
            3 PLAC <SOURCE_JURISDICTION_PLACE> {0:1} p.62
        2 AGNC <RESPONSIBLE_AGENCY> {0:1} p.60
        2 <<NOTE_STRUCTURE>> {0:M} p.37

*/
#endregion