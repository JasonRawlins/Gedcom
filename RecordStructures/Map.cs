using System.Text.Json;
using System.Text.Json.Serialization;

namespace Gedcom.RecordStructures;

[JsonConverter(typeof(MapJsonConverter))]
public class Map : RecordStructureBase
{
    public Map() : base() { }
    public Map(Record record) : base(record) { }
    public string PlaceLatitude => _(C.LATI);
    public string PlaceLongitude => _(C.LONG);

    public override string ToString() => $"{Record.Value}, {PlaceLatitude}, {PlaceLongitude}";
}

internal class MapJsonConverter : JsonConverter<Map>
{
    public override Map? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException();
    public override void Write(Utf8JsonWriter writer, Map map, JsonSerializerOptions options)
    {
        var mapJson = new MapJson(map);
        JsonSerializer.Serialize(writer, mapJson, mapJson.GetType(), options);
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