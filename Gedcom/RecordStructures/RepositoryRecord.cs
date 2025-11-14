using Newtonsoft.Json;

namespace Gedcom.RecordStructures;

// The Gedcom Standard 5.5.1 documentation is at the end of this file.
[JsonConverter(typeof(RepositoryRecordJsonConverter))]
public class RepositoryRecord : RecordStructureBase, IAddressStructure
{
    public RepositoryRecord() : base() { }
    public RepositoryRecord(Record record) : base(record) { }

    public List<string> AddressEmails => ListValues(C.EMAIL);
    public List<string> AddressFaxNumbers => ListValues(C.FAX);
    public AddressStructure AddressStructure => First<AddressStructure>(C.ADDR);
    public List<string> AddressWebPages => ListValues(C.WWW);
    public string AutomatedRecordId => _(C.RIN);
    public CallNumber CallNumber => First<CallNumber>(C.CALN);
    public ChangeDate ChangeDate => First<ChangeDate>(C.CHAN);
    public string Name => _(C.NAME);
    public List<NoteStructure> NoteStructures => List<NoteStructure>(C.NOTE);
    public List<string> PhoneNumbers => ListValues(C.PHON);
    public UserReferenceNumber UserReferenceNumber => First<UserReferenceNumber>(C.REFN);
    public string Xref => Record.Value;

    public override string ToString() => $"{Record.Value}, {Name}";
}

internal class RepositoryRecordJsonConverter : JsonConverter<RepositoryRecord>
{
    public override RepositoryRecord? ReadJson(JsonReader reader, Type objectType, RepositoryRecord? existingValue, bool hasExistingValue, JsonSerializer serializer) => throw new NotImplementedException();

    public override void WriteJson(JsonWriter writer, RepositoryRecord? repositoryRecord, JsonSerializer serializer)
    {
        if (repositoryRecord == null) throw new ArgumentNullException(nameof(repositoryRecord));

        serializer.Serialize(writer, new RepositoryRecordJson(repositoryRecord));
    }
}

internal class RepositoryRecordJson : GedcomJson
{
    public RepositoryRecordJson(RepositoryRecord repositoryRecord)
    {
        Emails = JsonList(repositoryRecord.AddressEmails);
        FaxNumbers = JsonList(repositoryRecord.AddressFaxNumbers);
        Address = JsonRecord(repositoryRecord.AddressStructure);
        WebPages = JsonList(repositoryRecord.AddressWebPages);
        AutomatedRecordId = JsonString(repositoryRecord.AutomatedRecordId);
        CallNumber = JsonRecord(repositoryRecord.CallNumber);
        ChangeDate = JsonRecord(repositoryRecord.ChangeDate);
        Name = JsonString(repositoryRecord.Name);
        Notes = JsonList(repositoryRecord.NoteStructures);
        PhoneNumbers = JsonList(repositoryRecord.PhoneNumbers);
        UserReferenceNumber = JsonRecord(repositoryRecord.UserReferenceNumber);
        Xref = repositoryRecord.Xref;
    }

    public List<string>? Emails { get; set; }
    public List<string>? FaxNumbers { get; set; }
    public AddressStructure? Address { get; set; }
    public List<string>? WebPages { get; set; }
    public string? AutomatedRecordId { get; set; }
    public CallNumber? CallNumber { get; set; }
    public ChangeDate? ChangeDate { get; set; }
    public string? Name { get; set; }
    public List<NoteStructure>? Notes { get; set; }
    public List<string>? PhoneNumbers { get; set; }
    public UserReferenceNumber? UserReferenceNumber { get; set; }
    public string? Xref { get; set; }
}

#region REPOSITORY_RECORD p. 27
/* 

REPOSITORY_RECORD:=

n @<XREF:REPO>@ REPO {1:1}
    +1 NAME <NAME_OF_REPOSITORY> {1:1} p.54
    +1 <<ADDRESS_STRUCTURE>> {0:1} p.31
    +1 <<NOTE_STRUCTURE>> {0:M} p.37
    +1 REFN <USER_REFERENCE_NUMBER> {0:M} p.63, 64
        +2 TYPE <USER_REFERENCE_TYPE> {0:1} p.64
    +1 RIN <AUTOMATED_RECORD_ID> {0:1} p.43
    +1 <<CHANGE_DATE>> {0:1} p.31
    +1 CALN <SOURCE_CALL_NUMBER> {0:M} p.61
        +2 MEDI <SOURCE_MEDIA_TYPE> {0:1}

*/
#endregion