using Gedcom.RecordStructures;

namespace Gedcom.DTOs;

public class MapDto(Map map) : GedcomDto
{
    public string? Latitude { get; set; } = GetString(map.PlaceLatitude);
    public string? Longitude { get; set; } = GetString(map.PlaceLongitude);
    public override string ToString() => $"({Latitude}, {Longitude})";
}