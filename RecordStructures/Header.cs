namespace Gedcom.RecordStructures;

public class Header : RecordStructureBase
{
    public Header() : base() { }
    public Header(Record record) : base(record) { }
    
    public HeaderSOUR HeaderSOUR => FirstOrDefault<HeaderSOUR>(C.SOUR);
    public string ReceivingSystemName => _(C.DEST);
    public Date TransmissionDate => FirstOrDefault<Date>(C.DATE);
    public string Submitter => _(C.SUBM);
    public SubmissionRecord SubmissionRecord => FirstOrDefault<SubmissionRecord>(C.SUBN);
    public string FileName => _(C.FILE);
    public string CopyrightGedcomFile => _(C.COPR);
    public GEDC Gedcom => FirstOrDefault<GEDC>(C.GEDC);
    public CharacterSet CharacterSet => FirstOrDefault<CharacterSet>(C.CHAR);
    public string LanguageOfText => _(C.LANG);
    public string PlaceHierarchy => Record.Records.FirstOrDefault(r => r.Tag.Equals(C.PLAC))?.Records.First(r => r.Tag.Equals(C.FORM)).Value ?? "";
    public NoteStructure GedcomContentDescription => FirstOrDefault<NoteStructure>(C.NOTE);
}

#region HEADER p. 23
/* 

HEADER:=

n HEAD {1:1}
    +1 SOUR <APPROVED_SYSTEM_ID> {1:1} p.42
        +2 VERS <VERSION_NUMBER> {0:1} p.64
        +2 NAME <NAME_OF_PRODUCT> {0:1} p.54
        +2 CORP <NAME_OF_BUSINESS> {0:1} p.54
            +3 <<ADDRESS_STRUCTURE>> {0:1} p.31
        +2 DATA <NAME_OF_SOURCE_DATA> {0:1} p.54
            +3 DATE <PUBLICATION_DATE> {0:1) p.59
            +3 COPR <COPYRIGHT_SOURCE_DATA> {0:1) p.44
                +4 [CONT|CONC]<COPYRIGHT_SOURCE_DATA> {0:M} p.44
    +1 DEST <RECEIVING_SYSTEM_NAME> {0:1* p.59
    +1 DATE <TRANSMISSION_DATE> {0:1} p.63
        +2 TIME <TIME_VALUE> {0:1} p.63
    +1 SUBM @<XREF:SUBM>@ {1:1} p.28
    +1 SUBN @<XREF:SUBN>@ {0:1} p.28
    +1 FILE <FILE_NAME> {0:1} p.50
    +1 COPR <COPYRIGHT_GEDCOM_FILE> {0:1} p.44
    +1 GEDC {1:1}
        +2 VERS <VERSION_NUMBER> {1:1} p.64
        +2 FORM <GEDCOM_FORM> {1:1} p.50
    +1 CHAR <CHARACTER_SET> {1:1} p.44
        +2 VERS <VERSION_NUMBER> {0:1} p.64
    +1 LANG <LANGUAGE_OF_TEXT> {0:1} p.51
    +1 PLAC {0:1}
        +2 FORM <PLACE_HIERARCHY> {1:1} p.58
    +1 NOTE <GEDCOM_CONTENT_DESCRIPTION> {0:1} p.50
        +2 [CONC|CONT] <GEDCOM_CONTENT_DESCRIPTION> {0:M}

* NOTE:
Submissions to the Family History Department for Ancestral File submission or for clearing temple ordinances must use a
DESTination of ANSTFILE or TempleReady, respectively.

The header structure provides information about the entire transmission. The SOURce system name
identifies which system sent the data. The DESTination system name identifies the intended receiving
system.

Additional GEDCOM standards will be produced in the future to reflect GEDCOM expansion and
maturity. This requires the reading program to make sure it can read the GEDC.VERS and the
GEDC.FORM values to insure proper readability. The CHAR tag is required. All character codes
greater than 0x7F must be converted to ANSEL. (See Chapter 3, starting on page 77.)

*/
#endregion