using Gedcom.RecordStructures;

namespace Gedcom.DTOs;

public class RepositoryDto : GedcomDto
{
    public RepositoryDto(RepositoryRecord repositoryRecord)
    {
        Address = GetRecord(new AddressDto(repositoryRecord.AddressStructure));
        AutomatedRecordId = GetString(repositoryRecord.AutomatedRecordId);
        CallNumber = GetRecord(new CallNumberDto(repositoryRecord.CallNumber));
        ChangeDate = GetRecord(new ChangeDateDto(repositoryRecord.ChangeDate));
        Emails = GetList(repositoryRecord.AddressEmails);
        FaxNumbers = GetList(repositoryRecord.AddressFaxNumbers);
        IsEmpty = repositoryRecord.IsEmpty;
        Name = GetString(repositoryRecord.Name);
        Note = GetString(repositoryRecord.NoteStructures.FirstOrDefault()?.Text ?? "");
        PhoneNumbers = GetList(repositoryRecord.PhoneNumbers);
        UserReferenceNumber = GetRecord(new UserReferenceNumberDto(repositoryRecord.UserReferenceNumber));
        WebPages = GetList(repositoryRecord.AddressWebPages);
        Xref = repositoryRecord.Xref;
    }

    public AddressDto? Address { get; set; }
    public string? AutomatedRecordId { get; set; }
    public CallNumberDto? CallNumber { get; set; }
    public ChangeDateDto? ChangeDate { get; set; }
    public List<string>? Emails { get; set; }
    public List<string>? FaxNumbers { get; set; }
    public string? Name { get; set; }
    public string? Note { get; set; }
    public List<string>? PhoneNumbers { get; set; }
    public UserReferenceNumberDto? UserReferenceNumber { get; set; }
    public List<string>? WebPages { get; set; }
    public string? Xref { get; set; }
    public override string ToString() => $"{Name}";
}