using Gedcom.RecordStructures;

namespace Gedcom.DTOs;

public class SourceDataDto(SourceRecordData sourceRecordData) : GedcomDto
{
    public List<SourceRecordEventDto>? EventsRecorded { get; set; } = GetList(sourceRecordData.RecordEvents.Select(re => new SourceRecordEventDto(re)).ToList());
    public List<NoteDto>? Notes { get; set; } = GedcomDto.GetList<NoteDto>(sourceRecordData.NoteStructures.Select(ns => new NoteDto(ns)).ToList());
    public string? ResponsibleAgency { get; set; } = GetString(sourceRecordData.ResponsibleAgency);

    public override string ToString() => $"{ResponsibleAgency}";
}