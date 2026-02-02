using System.Text.Json;
using System.Text.Json.Serialization;

namespace Gedcom.RecordStructures;

// The Gedcom Standard 5.5.1 documentation is at the end of this file.
[JsonConverter(typeof(PlaceStructureJsonConverter))]
public class PlaceStructure : RecordStructureBase
{
    public PlaceStructure() : base() { }
    public PlaceStructure(Record record) : base(record) { }

    private Map? _map = null;
    public Map Map => _map ??= First<Map>(Tag.Map);

    private List<NoteStructure>? _noteStructures = null;
    public List<NoteStructure> NoteStructures => _noteStructures ??= List<NoteStructure>(Tag.Note);

    private string? _placeHierarchy = null;
    public string PlaceHierarchy => _placeHierarchy ??= GetValue(Tag.Format);
    
    public string PlaceName => Record.Value;

    private List<NameVariation>? _placePhoneticVariations = null;
    public List<NameVariation> PlacePhoneticVariations => _placePhoneticVariations ??= List<NameVariation>(Tag.Phonetic);

    private List<NameVariation>? _placeRomanizedVariations = null;
    public List<NameVariation> PlaceRomanizedVariations => _placeRomanizedVariations ??= List<NameVariation>(Tag.Romanized);

    public override string ToString() => $"{Record.Value}, {PlaceName}";
}

internal sealed class PlaceStructureJsonConverter : JsonConverter<PlaceStructure>
{
    public override PlaceStructure? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException();

    public override void Write(Utf8JsonWriter writer, PlaceStructure value, JsonSerializerOptions options)
    {
        ArgumentNullException.ThrowIfNull(value);
        JsonSerializer.Serialize(writer, new PlaceJson(value), options);
    }
}

public class PlaceJson(PlaceStructure placeStructure) : GedcomJson
{
    public string? Hierarchy { get; set; } = JsonString(placeStructure.PlaceHierarchy);
    public MapJson? Map { get; set; } = JsonRecord(new MapJson(placeStructure.Map));
    public string? Name { get; set; } = JsonString(placeStructure.PlaceName);
    public List<NoteJson>? Notes { get; set; } = GedcomJson.JsonList<NoteJson>(placeStructure.NoteStructures.Select(ns => new NoteJson(ns)).ToList());
    public List<NameVariationJson>? PhoneticVariations { get; set; } = JsonList(placeStructure.PlacePhoneticVariations.Select(ppv => new NameVariationJson(ppv)).ToList());
    public List<NameVariationJson>? RomanizedVariations { get; set; } = JsonList(placeStructure.PlaceRomanizedVariations.Select(prv => new NameVariationJson(prv)).ToList());

    public override string ToString() => $"{Name}";
}

#region PLACE_STRUCTURE p. 38-39
/* 

PLACE_STRUCTURE:=

n PLAC <PLACE_NAME> {1:1} p.58
    +1 FORM <PLACE_HIERARCHY> {0:1} p.58
    +1 FONE <PLACE_PHONETIC_VARIATION> {0:M} p.59
        +2 TYPE <PHONETIC_TYPE> {1:1} p.57
    +1 ROMN <PLACE_ROMANIZED_VARIATION> {0:M} p.59
        +2 TYPE <ROMANIZED_TYPE> {1:1} p.61
    +1 MAP {0:1}
        +2 LATI <PLACE_LATITUDE> {1:1} p.58
        +2 LONG <PLACE_LONGITUDE> {1:1} p.58
    +1 <<NOTE_STRUCTURE>> {0:M} p.37

PLACE_HIERARCHY:=

This shows the jurisdictional entities that are named in a sequence from the lowest to the highest
jurisdiction. The jurisdictions are separated by commas, and any jurisdiction's name that is missing is
still accounted for by a comma. When a PLAC.FORM structure is included in the HEADER of a
GEDCOM transmission, it implies that all place names follow this jurisdictional format and each
jurisdiction is accounted for by a comma, whether the name is known or not. When the PLAC.FORM
is subordinate to an event, it temporarily overrides the implications made by the PLAC.FORM
structure stated in the HEADER. This usage is not common and, therefore, not encouraged. It should
only be used when a system has over-structured its place-names.

*/
#endregion