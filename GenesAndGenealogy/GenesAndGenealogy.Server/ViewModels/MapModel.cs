using Gedcom.RecordStructures;

namespace GenesAndGenealogy.Server.ViewModels;

public class MapModel
{
    public MapModel(Map map)
    {
        Latitude = map.PlaceLatitude;
        Longitude = map.PlaceLongitude;
    }

    public string Latitude { get; set; }
    public string Longitude { get; set; }

    public override string ToString() => $"({Latitude}, {Longitude})";
}