using Newtonsoft.Json;

namespace Gedcom.RecordStructures;

// The Gedcom Standard 5.1.1 documentation is at the end of this file.
[JsonConverter(typeof(MapJsonConverter))]
public class Map : RecordStructureBase
{
    public Map() : base() { }
    public Map(Record record) : base(record) { }
    public string PlaceLatitude => _(Tag.LATI);
    public string PlaceLongitude => _(Tag.LONG);

    public override string ToString() => $"{Record.Value}, {PlaceLatitude}, {PlaceLongitude}";
}

internal class MapJsonConverter : JsonConverter<Map>
{
    public override Map? ReadJson(JsonReader reader, Type objectType, Map? existingValue, bool hasExistingValue, JsonSerializer serializer) => throw new NotImplementedException();

    public override void WriteJson(JsonWriter writer, Map? map, JsonSerializer serializer)
    {
        if (map == null) throw new ArgumentNullException(nameof(map));

        serializer.Serialize(writer, new MapJson(map));
    }
}

internal class MapJson : GedcomJson
{
    public MapJson(Map map)
    {
        PlaceLatitude = JsonString(map.PlaceLatitude);
        PlaceLongitude = JsonString(map.PlaceLongitude);
    }

    public string? PlaceLatitude { get; set; }
    public string? PlaceLongitude { get; set; }
}

#region MAP p. 39
/* 

+1 MAP {0:1}
    +2 LATI <PLACE_LATITUDE> {1:1} p.58
    +2 LONG <PLACE_LONGITUDE> {1:1} p.58

MAP {MAP}:=
Pertains to a representation of measurements usually presented in a graphical form.

PLACE_LATITUDE:= {Size=5:8}
The value specifying the latitudinal coordinate of the place name. The latitude coordinate is the
direction North or South from the equator in degrees and fraction of degrees carried out to give the
desired accuracy. For example: 18 degrees, 9 minutes, and 3.4 seconds North would be formatted as
N18.150944. Minutes and seconds are converted by dividing the minutes value by 60 and the seconds
value by 3600 and adding the results together. This sum becomes the fractional part of the degree’s
value.

PLACE_LONGITUDE:= {Size=5:8}
The value specifying the longitudinal coordinate of the place name. The longitude coordinate is
Degrees and fraction of degrees east or west of the zero or base meridian coordinate. For example:
168 degrees, 9 minutes, and 3.4 seconds East would be formatted as E168.150944.

*/
#endregion