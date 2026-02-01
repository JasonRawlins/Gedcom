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

    public List<string> AddressEmails => ListValues(Tag.Email);
    public List<string> AddressFaxNumbers => ListValues(Tag.Facimilie);
    public AddressStructure AddressStructure => First<AddressStructure>(Tag.Address);
    public List<string> AddressWebPages => ListValues(Tag.Web);
    public string AutomatedRecordId => GetValue(Tag.RecordIdNumber);
    public CallNumber CallNumber => First<CallNumber>(Tag.CallNumber);
    public ChangeDate ChangeDate => First<ChangeDate>(Tag.Change);
    public string Name => GetValue(Tag.Name);
    public List<NoteStructure> NoteStructures => List<NoteStructure>(Tag.Note);
    public List<string> PhoneNumbers => ListValues(Tag.Phone);
    public UserReferenceNumber UserReferenceNumber => First<UserReferenceNumber>(Tag.Reference);
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