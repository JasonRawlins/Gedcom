using Gedcom.RecordStructures;

namespace Gedcom.DTOs;

public class PlaceDto(PlaceStructure placeStructure) : GedcomDto
{
    public string? Hierarchy { get; set; } = GetString(placeStructure.PlaceHierarchy);
    public MapDto? Map { get; set; } = GetRecord(new MapDto(placeStructure.Map));
    public string? Name { get; set; } = GetString(placeStructure.PlaceName);
    public List<NoteDto>? Notes { get; set; } = GetList(placeStructure.NoteStructures.Select(ns => new NoteDto(ns)).ToList());
    public List<NameVariationDto>? PhoneticVariations { get; set; } = GetList(placeStructure.PlacePhoneticVariations.Select(ppv => new NameVariationDto(ppv)).ToList());
    public List<NameVariationDto>? RomanizedVariations { get; set; } = GetList(placeStructure.PlaceRomanizedVariations.Select(prv => new NameVariationDto(prv)).ToList());

    public override string ToString() => $"{Name}";
}