using Gedcom.RecordStructures;

namespace Gedcom.DTOs;

public class EventTypeCitedFromDto(EventTypeCitedFrom eventTypeCitedFrom) : GedcomDto
{
    public string? RoleInEvent { get; set; } = GetString(eventTypeCitedFrom.RoleInEvent);
    public override string ToString() => $"{RoleInEvent}";
}