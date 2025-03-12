using Gedcom.Core;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Gedcom.RecordStructures;

[JsonConverter(typeof(RepositoryRecordJsonConverter))]
public class RepositoryRecord : RecordStructureBase, IAddressStructure
{
    internal RepositoryRecord() : base() { }
    public RepositoryRecord(Record record) : base(record) { }
    public string Name => _(C.NAME);
    public AddressStructure AddressStructure => First<AddressStructure>(C.ADDR);
    public List<NoteStructure> NoteStructures => List<NoteStructure>(C.NOTE);
    public UserReferenceNumber UserReferenceNumber => First<UserReferenceNumber>(C.REFN);
    public string AutomatedRecordId => _(C.RIN);
    public ChangeDate ChangeDate => First<ChangeDate>(C.CHAN);
    public CallNumber CallNumber => First<CallNumber>(C.CALN);

    #region IAddressStructure
    public List<string> PhoneNumbers => ListValues(C.PHON);
    public List<string> AddressEmails => ListValues(C.EMAIL);
    public List<string> AddressFaxNumbers => ListValues(C.FAX);
    public List<string> AddressWebPages => ListValues(C.WWW);
    #endregion
}

internal class RepositoryRecordJsonConverter : JsonConverter<RepositoryRecord>
{
    public override RepositoryRecord? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException();
    public override void Write(Utf8JsonWriter writer, RepositoryRecord repositoryRecord, JsonSerializerOptions options)
    {
        var repositoryRecordJson = new RepositoryRecordJson(repositoryRecord);
        JsonSerializer.Serialize(writer, repositoryRecordJson, repositoryRecordJson.GetType(), options);
    }
}

internal class RepositoryRecordJson
{
    public RepositoryRecordJson(RepositoryRecord repositoryRecord)
    {
        Name = string.IsNullOrEmpty(repositoryRecord.Name) ? null : repositoryRecord.Name;
        AddressStructure = repositoryRecord.AddressStructure.IsEmpty ? null : repositoryRecord.AddressStructure;
        NoteStructures = repositoryRecord.NoteStructures.Count == 0 ? null : repositoryRecord.NoteStructures;
        UserReferenceNumber = repositoryRecord.UserReferenceNumber.IsEmpty ? null : repositoryRecord.UserReferenceNumber;
        AutomatedRecordId = string.IsNullOrEmpty(repositoryRecord.AutomatedRecordId) ? null : repositoryRecord.AutomatedRecordId;
        ChangeDate = repositoryRecord.ChangeDate.IsEmpty ? null : repositoryRecord.ChangeDate;
        CallNumber = repositoryRecord.CallNumber.IsEmpty ? null : repositoryRecord.CallNumber;
        PhoneNumbers = repositoryRecord.PhoneNumbers.Count == 0 ? null : repositoryRecord.PhoneNumbers;
        AddressEmails = repositoryRecord.AddressEmails.Count == 0 ? null : repositoryRecord.AddressEmails;
        AddressFaxNumbers = repositoryRecord.AddressFaxNumbers.Count == 0 ? null : repositoryRecord.AddressFaxNumbers;
        AddressWebPages = repositoryRecord.AddressWebPages.Count == 0 ? null : repositoryRecord.AddressWebPages;
    }

    public string? Name { get; set; }
    public AddressStructure? AddressStructure { get; set; }
    public List<NoteStructure>? NoteStructures { get; set; }
    public UserReferenceNumber? UserReferenceNumber { get; set; }
    public string? AutomatedRecordId { get; set; }
    public ChangeDate? ChangeDate { get; set; }
    public CallNumber? CallNumber { get; set; }
    public List<string>? PhoneNumbers { get; set; }
    public List<string>? AddressEmails { get; set; }
    public List<string>? AddressFaxNumbers { get; set; }
    public List<string>? AddressWebPages { get; set; }
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