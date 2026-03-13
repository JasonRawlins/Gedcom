using Gedcom.RecordStructures;

namespace Gedcom.DTOs;

public class UserReferenceNumberDto(UserReferenceNumber userReferenceNumber) : GedcomDto
{
    public string? UserReferenceType { get; set; } = GetString(userReferenceNumber.UserReferenceType);

    public override string ToString() => $"{UserReferenceType}";
}