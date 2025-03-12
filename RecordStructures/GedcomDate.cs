using Gedcom.Core;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Gedcom.RecordStructures;

[JsonConverter(typeof(GedcomDateJsonConverter))]
public class GedcomDate : RecordStructureBase
{
    public GedcomDate() : base() { }
    public GedcomDate(Record record) : base(record) { }

    public string DateValue => Record.Value;
    public string TimeValue => _(C.TIME);
}

internal class GedcomDateJsonConverter : JsonConverter<GedcomDate>
{
    public override GedcomDate? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException();
    public override void Write(Utf8JsonWriter writer, GedcomDate gedcomDate, JsonSerializerOptions options)
    {
        var gedcomDateJson = new GedcomDateJson(gedcomDate);
        JsonSerializer.Serialize(writer, gedcomDateJson, gedcomDateJson.GetType(), options);
    }
}

internal class GedcomDateJson : GedcomJson
{
    public GedcomDateJson(GedcomDate gedcomDate)
    {
        DateValue = JsonString(gedcomDate.DateValue);
        TimeValue = JsonString(gedcomDate.TimeValue);
    }

    public string? DateValue { get; set; }
    public string? TimeValue { get; set; }
}

#region DATE p. 45
/* 

+1 DATE <TRANSMISSION_DATE> {0:1} p.63
    +2 TIME <TIME_VALUE> {0:1} p.63

*/
#endregion