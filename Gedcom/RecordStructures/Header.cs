using Newtonsoft.Json;

namespace Gedcom.RecordStructures;

// The Gedcom Standard 5.5.1 documentation is at the end of this file.
[JsonConverter(typeof(HeaderJsonConverter))]
public class Header : RecordStructureBase
{
    public Header() : base() { }
    public Header(Record record) : base(record) { }
    
    public CharacterSet CharacterSet => First<CharacterSet>(Tag.Character);
    public string CopyrightGedcomFile => GetValue(Tag.Copyright);
    public string FileName => GetValue(Tag.File);
    public HeaderGedcom Gedcom => First<HeaderGedcom>(Tag.Gedcom);
    public NoteStructure GedcomContentDescription => First<NoteStructure>(Tag.Note);
    public string LanguageOfText => GetValue(Tag.Language);
    public string PlaceHierarchy => Record.Records.FirstOrDefault(r => r.Tag.Equals(Tag.Place))?.Records.First(r => r.Tag.Equals(Tag.Format)).Value ?? "";
    public string ReceivingSystemName => GetValue(Tag.Destination);
    public HeaderSource Source => First<HeaderSource>(Tag.Source);
    public SubmissionRecord SubmissionRecord => First<SubmissionRecord>(Tag.Submission);
    public string Submitter => GetValue(Tag.Submitter);
    public GedcomDate TransmissionDate => First<GedcomDate>(Tag.Date);

    public override string ToString() => $"{Record.Value}, {Submitter}";
}

internal class HeaderJsonConverter : JsonConverter<Header>
{
    public override Header? ReadJson(JsonReader reader, Type objectType, Header? existingValue, bool hasExistingValue, JsonSerializer serializer) => throw new NotImplementedException();

    public override void WriteJson(JsonWriter writer, Header? header, JsonSerializer serializer)
    {
        if (header == null) throw new ArgumentNullException(nameof(header));

        serializer.Serialize(writer, new HeaderJson(header));
    }
}

internal class HeaderJson : GedcomJson
{
    public HeaderJson(Header header)
    {
        CharacterSet = JsonRecord(header.CharacterSet);
        CopyrightGedcomFile = JsonString(header.CopyrightGedcomFile);
        FileName = JsonString(header.FileName);
        Gedcom = JsonRecord(header.Gedcom);
        GedcomContentDescription = JsonRecord(header.GedcomContentDescription);
        LanguageOfText = JsonString(header.LanguageOfText);
        PlaceHierarchy = JsonString(header.PlaceHierarchy);
        ReceivingSystemName = JsonString(header.ReceivingSystemName);
        Source = JsonRecord(header.Source);
        SubmissionRecord = JsonRecord(header.SubmissionRecord);
        Submitter = JsonString(header.Submitter);
        TransmissionDate = JsonRecord(header.TransmissionDate);
    }

    public CharacterSet? CharacterSet { get; set; }
    public string? CopyrightGedcomFile { get; set; }
    public string? FileName { get; set; }
    public HeaderGedcom? Gedcom { get; set; }
    public NoteStructure? GedcomContentDescription { get; set; }
    public string? LanguageOfText { get; set; }
    public string? PlaceHierarchy { get; set; }
    public string? ReceivingSystemName { get; set; }
    public HeaderSource? Source { get; set; }
    public SubmissionRecord? SubmissionRecord { get; set; }
    public string? Submitter { get; set; }
    public GedcomDate? TransmissionDate { get; set; }
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