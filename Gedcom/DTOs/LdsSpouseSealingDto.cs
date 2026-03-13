using Gedcom.RecordStructures;

namespace Gedcom.DTOs;

public class LdsSpouseSealingDto(LdsSpouseSealing ldsSpouseSealing) : GedcomDto
{
    public string? DateLdsOrdinance { get; set; } = GetString(ldsSpouseSealing.DateLdsOrdinance);
    public LdsOrdinanceStatusDto? LdsSpouseSealingDateStatus { get; set; } = GetRecord(new LdsOrdinanceStatusDto(ldsSpouseSealing.LdsSpouseSealingDateStatus));
    public List<NoteDto>? Notes { get; set; } = GedcomDto.GetList<NoteDto>(ldsSpouseSealing.NoteStructures.Select(ns => new NoteDto(ns)).ToList());
    public string? PlaceLivingOrdinance { get; set; } = GetString(ldsSpouseSealing.PlaceLivingOrdinance);
    public List<SourceCitationDto>? SourceCitations { get; set; } = GetList(ldsSpouseSealing.SourceCitations.Select(sc => new SourceCitationDto(sc)).ToList());
    public string? TempleCode { get; set; } = GetString(ldsSpouseSealing.TempleCode);
    public override string ToString() => $"{TempleCode}";
}