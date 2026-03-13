using Gedcom.RecordStructures;

namespace Gedcom.DTOs;

public class LdsIndividualOrdinanceDto(LdsIndividualOrdinance ldsIndividualOrdinance) : GedcomDto
{
    public string? DateLdsOrdinance { get; set; } = GetString(ldsIndividualOrdinance.DateLdsOrdinance);
    public LdsOrdinanceStatusDto? LdsBaptismDateStatus { get; set; } = GetRecord(new LdsOrdinanceStatusDto(ldsIndividualOrdinance.LdsBaptismDateStatus));
    public List<NoteDto>? Notes { get; set; } = GedcomDto.GetList<NoteDto>(ldsIndividualOrdinance.NoteStructures.Select(ns => new NoteDto(ns)).ToList());
    public string? PlaceLivingOrdinance { get; set; } = GetString(ldsIndividualOrdinance.PlaceLivingOrdinance);
    public List<SourceCitationDto>? SourceCitations { get; set; } = GetList(ldsIndividualOrdinance.SourceCitations.Select(sc => new SourceCitationDto(sc)).ToList());
    public string? TempleCode { get; set; } = GetString(ldsIndividualOrdinance.TempleCode);
    public override string ToString() => $"{TempleCode} {DateLdsOrdinance}";
}