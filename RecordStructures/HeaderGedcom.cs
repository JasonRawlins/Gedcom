using System.Text.Json;
using System.Text.Json.Serialization;

namespace Gedcom.RecordStructures;

[JsonConverter(typeof(GEDCJsonConverter))]
public class HeaderGedcom : RecordStructureBase
{
    public HeaderGedcom() : base() { }
    public HeaderGedcom(Record record) : base(record) { }

    public string VersionNumber => _(C.VERS);
    public string GedcomForm => _(C.FORM);

    public override string ToString() => $"{Record.Value}, {VersionNumber}";
}

internal class GEDCJsonConverter : JsonConverter<HeaderGedcom>
{
    public override HeaderGedcom? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException();
    public override void Write(Utf8JsonWriter writer, HeaderGedcom gedc, JsonSerializerOptions options)
    {
        var gedcJson = new GEDCJson(gedc);
        JsonSerializer.Serialize(writer, gedcJson, gedcJson.GetType(), options);
    }
}

internal class GEDCJson : GedcomJson
{
    public GEDCJson(HeaderGedcom gedc)
    {
        VersionNumber = JsonString(gedc.VersionNumber);
        GedcomForm = JsonString(gedc.GedcomForm);
    }

    public string? VersionNumber { get; set; }
    public string? GedcomForm { get; set; }
}

#region STRUCTURE_NAME p. 23
/* 

GEDC {GEDCOM}:=

1 GEDC {1:1}
    2 VERS <VERSION_NUMBER> {1:1} p.64
    2 FORM <GEDCOM_FORM> {1:1} p.50

Information about the use of GEDCOM in a transmission.

*/
#endregion