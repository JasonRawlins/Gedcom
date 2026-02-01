using Gedcom.Core;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Gedcom.RecordStructures;

// The Gedcom Standard 5.5.1 documentation is at the end of this file.
[JsonConverter(typeof(HeaderJsonConverter))]
public class Header : RecordStructureBase
{
    public Header() : base() { }
    public Header(Record record) : base(record) { }

    private CharacterSet? characterSet = null;
    public CharacterSet CharacterSet => characterSet ??= First<CharacterSet>(Tag.Character);

    private string? copyrightGedcomFile = null;
    public string CopyrightGedcomFile => copyrightGedcomFile ??= GetValue(Tag.Copyright);

    private string? fileName = null;
    public string FileName => fileName ??= GetValue(Tag.File);

    private HeaderGedcom? gedcom = null;
    public HeaderGedcom Gedcom => gedcom ??= First<HeaderGedcom>(Tag.Gedcom);

    private NoteStructure? gedcomContentDescription = null;
    public NoteStructure GedcomContentDescription => gedcomContentDescription ??= First<NoteStructure>(Tag.Note);

    private string? languageOfText = null;
    public string LanguageOfText => languageOfText ??= GetValue(Tag.Language);

    private string? placeHierarchy = null;
    public string PlaceHierarchy => placeHierarchy ??= Record.Records.FirstOrDefault(r => r.Tag.Equals(Tag.Place))?.Records.First(r => r.Tag.Equals(Tag.Format)).Value ?? "";

    private string? receivingSystemName = null;
    public string ReceivingSystemName => receivingSystemName ??= GetValue(Tag.Destination);

    private HeaderSource? source = null;
    public HeaderSource Source => source ??= First<HeaderSource>(Tag.Source);

    private SubmissionRecord? submissionRecord = null;
    public SubmissionRecord SubmissionRecord => submissionRecord ??= First<SubmissionRecord>(Tag.Submission);

    private string? submitter = null;
    public string Submitter => submitter ??= GetValue(Tag.Submitter);

    private GedcomDate? transmissionDate = null;
    public GedcomDate TransmissionDate => transmissionDate ??= First<GedcomDate>(Tag.Date);

    public override string ToString() => $"{Record.Value}, {Submitter}";
}

internal sealed class HeaderJsonConverter : JsonConverter<Header>
{
    public override Header? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException();

    public override void Write(Utf8JsonWriter writer, Header value, JsonSerializerOptions options)
    {
        ArgumentNullException.ThrowIfNull(value);
        JsonSerializer.Serialize(writer, new HeaderJson(value), options);
    }
}

public class HeaderJson(Header header) : GedcomJson
{
    public CharacterSetJson? CharacterSet { get; set; } = JsonRecord(new CharacterSetJson(header.CharacterSet));
    public string? CopyrightGedcomFile { get; set; } = JsonString(header.CopyrightGedcomFile);
    public string? FileName { get; set; } = JsonString(header.FileName);
    public HeaderGedcomJson? Gedcom { get; set; } = JsonRecord(new HeaderGedcomJson(header.Gedcom));
    public NoteJson? GedcomContentDescription { get; set; } = JsonRecord(new NoteJson(header.GedcomContentDescription));
    public string? LanguageOfText { get; set; } = JsonString(header.LanguageOfText);
    public string? PlaceHierarchy { get; set; } = JsonString(header.PlaceHierarchy);
    public string? ReceivingSystemName { get; set; } = JsonString(header.ReceivingSystemName);
    public HeaderSourceJson? Source { get; set; } = JsonRecord(new HeaderSourceJson(header.Source));
    public SubmissionJson? SubmissionRecord { get; set; } = JsonRecord(new SubmissionJson(header.SubmissionRecord));
    public string? Submitter { get; set; } = JsonString(header.Submitter);
    public GedcomDateJson? TransmissionDate { get; set; } = JsonRecord(new GedcomDateJson(header.TransmissionDate));
    public override string ToString() => $"{Submitter}";
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