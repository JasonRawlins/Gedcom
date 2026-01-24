using Newtonsoft.Json;

namespace Gedcom.RecordStructures;

// The Gedcom Standard 5.5.1 documentation is at the end of this file.
[JsonConverter(typeof(HeaderGedcomJsonConverter))]
public class HeaderGedcom : RecordStructureBase
{
    public HeaderGedcom() : base() { }
    public HeaderGedcom(Record record) : base(record) { }

    public string GedcomForm => GetValue(Tag.Format);
    public string VersionNumber => GetValue(Tag.Version);

    public override string ToString() => $"{Record.Value}, {VersionNumber}";
}

internal class HeaderGedcomJsonConverter : JsonConverter<HeaderGedcom>
{
    public override HeaderGedcom? ReadJson(JsonReader reader, Type objectType, HeaderGedcom? existingValue, bool hasExistingValue, JsonSerializer serializer) => throw new NotImplementedException();

    public override void WriteJson(JsonWriter writer, HeaderGedcom? headerGedcom, JsonSerializer serializer)
    {
        if (headerGedcom == null) throw new ArgumentNullException(nameof(headerGedcom));

        serializer.Serialize(writer, new HeaderGedcomJson(headerGedcom));
    }
}

public class HeaderGedcomJson : GedcomJson
{
    public HeaderGedcomJson(HeaderGedcom gedc)
    {
        GedcomForm = JsonString(gedc.GedcomForm);
        VersionNumber = JsonString(gedc.VersionNumber);
    }

    public string? GedcomForm { get; set; }
    public string? VersionNumber { get; set; }
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