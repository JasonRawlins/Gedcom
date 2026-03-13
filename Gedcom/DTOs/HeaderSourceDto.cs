using Gedcom.RecordStructures;

namespace Gedcom.DTOs;

public class HeaderSourceDto(HeaderSource headerSource) : GedcomDto
{
    public HeaderCorporationDto? Corporation { get; set; } = GetRecord(new HeaderCorporationDto(headerSource.Corporation));
    public HeaderDataDto? Data { get; set; } = GetRecord(new HeaderDataDto(headerSource.Data));
    public string? NameOfProduct { get; set; } = GetString(headerSource.NameOfProduct);
    public HeaderTreeDto? Tree { get; set; } = GetRecord(new HeaderTreeDto(headerSource.Tree));
    public string? Version { get; set; } = GetString(headerSource.Version);
    public string? Xref { get; set; } = GetString(headerSource.Xref);
    public override string ToString() => $"{Tree?.Name}";
}