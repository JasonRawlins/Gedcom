using Gedcom.RecordStructures;

namespace Gedcom.DTOs;

public class SourceRecordEventDto(SourceRecordEvent sourceRecordEvent) : GedcomDto
{
    public string? DatePeriod { get; set; } = GetString(sourceRecordEvent.DatePeriod);
    public string? SourceJurisdictionPlace { get; set; } = GetString(sourceRecordEvent.SourceJurisdictionPlace);

    public override string ToString() => $"{SourceJurisdictionPlace} {DatePeriod}";
}