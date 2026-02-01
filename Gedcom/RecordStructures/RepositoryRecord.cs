using Gedcom.Core;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Gedcom.RecordStructures;

// The Gedcom Standard 5.5.1 documentation is at the end of this file.
[JsonConverter(typeof(RepositoryRecordJsonConverter))]
public class RepositoryRecord : RecordStructureBase, IAddressStructure
{
    public RepositoryRecord() : base() { }
    public RepositoryRecord(Record record) : base(record) { }

    private List<string>? addressEmails = null;
    public List<string> AddressEmails => addressEmails ??= ListValues(Tag.Email);

    private List<string>? addressFaxNumbers = null;
    public List<string> AddressFaxNumbers => addressFaxNumbers ??= ListValues(Tag.Facimilie);

    private AddressStructure? addressStructure = null;
    public AddressStructure AddressStructure => addressStructure ??= First<AddressStructure>(Tag.Address);

    private List<string>? addressWebPages = null;
    public List<string> AddressWebPages => addressWebPages ??= ListValues(Tag.Web);

    private string? automatedRecordId = null;
    public string AutomatedRecordId => automatedRecordId ??= GetValue(Tag.RecordIdNumber);

    private CallNumber? callNumber = null;
    public CallNumber CallNumber => callNumber ??= First<CallNumber>(Tag.CallNumber);

    private ChangeDate? changeDate = null;
    public ChangeDate ChangeDate => changeDate ??= First<ChangeDate>(Tag.Change);

    private string? name = null;
    public string Name => name ??= GetValue(Tag.Name);

    private List<NoteStructure>? noteStructures = null;
    public List<NoteStructure> NoteStructures => noteStructures ??= List<NoteStructure>(Tag.Note);

    private List<string>? phoneNumbers = null;
    public List<string> PhoneNumbers => phoneNumbers ??= ListValues(Tag.Phone);

    private UserReferenceNumber? userReferenceNumber = null;
    public UserReferenceNumber UserReferenceNumber => userReferenceNumber ?? First<UserReferenceNumber>(Tag.Reference);
    
    public string Xref => Record.Value;

    public override string ToString() => $"{Record.Value}, {Name}";
}

internal sealed class RepositoryRecordJsonConverter : JsonConverter<RepositoryRecord>
{
    public override RepositoryRecord? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException();

    public override void Write(Utf8JsonWriter writer, RepositoryRecord value, JsonSerializerOptions options)
    {
        ArgumentNullException.ThrowIfNull(value);
        JsonSerializer.Serialize(writer, new RepositoryJson(value), options);
    }
}

public class RepositoryJson : GedcomJson
{
    public RepositoryJson(RepositoryRecord repositoryRecord)
    {
        Address = JsonRecord(new AddressJson(repositoryRecord.AddressStructure));
        AutomatedRecordId = JsonString(repositoryRecord.AutomatedRecordId);
        CallNumber = JsonRecord(new CallNumberJson(repositoryRecord.CallNumber));
        ChangeDate = JsonRecord(new ChangeDateJson(repositoryRecord.ChangeDate));
        Emails = JsonList(repositoryRecord.AddressEmails);
        FaxNumbers = JsonList(repositoryRecord.AddressFaxNumbers);
        IsEmpty = repositoryRecord.IsEmpty;
        Name = JsonString(repositoryRecord.Name);
        Note = JsonString(repositoryRecord.NoteStructures.FirstOrDefault()?.Text ?? "");
        PhoneNumbers = JsonList(repositoryRecord.PhoneNumbers);
        UserReferenceNumber = JsonRecord(new UserReferenceNumberJson(repositoryRecord.UserReferenceNumber));
        WebPages = JsonList(repositoryRecord.AddressWebPages);
        Xref = repositoryRecord.Xref;
    }

    public AddressJson? Address { get; set; }
    public string? AutomatedRecordId { get; set; }
    public CallNumberJson? CallNumber { get; set; }
    public ChangeDateJson? ChangeDate { get; set; }
    public List<string>? Emails { get; set; }
    public List<string>? FaxNumbers { get; set; }
    public string? Name { get; set; }
    public string? Note { get; set; }
    public List<string>? PhoneNumbers { get; set; }
    public UserReferenceNumberJson? UserReferenceNumber { get; set; }
    public List<string>? WebPages { get; set; }
    public string? Xref { get; set; }
    public override string ToString() => $"{Name}";
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