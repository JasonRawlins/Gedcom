using Newtonsoft.Json;

namespace Gedcom.RecordStructures;

// The Gedcom Standard 5.5.1 documentation is at the end of this file.
[JsonConverter(typeof(PlaceJsonConverter))]
public class PlaceStructure : RecordStructureBase
{
    public PlaceStructure() : base() { }
    public PlaceStructure(Record record) : base(record) { }

    public Map Map => First<Map>(Tag.Map);
    public List<NoteStructure> NoteStructures => List<NoteStructure>(Tag.Note);
    public string PlaceHierarchy => GetValue(Tag.Format);
    public string PlaceName => Record.Value;
    public List<NameVariation> PlacePhoneticVariations => List<NameVariation>(Tag.Phonetic);
    public List<NameVariation> PlaceRomanizedVariations => List<NameVariation>(Tag.Romanized);

    public override string ToString() => $"{Record.Value}, {PlaceName}";
}

internal class PlaceJsonConverter : JsonConverter<PlaceStructure>
{
    public override PlaceStructure? ReadJson(JsonReader reader, Type objectType, PlaceStructure? existingValue, bool hasExistingValue, JsonSerializer serializer) => throw new NotImplementedException();

    public override void WriteJson(JsonWriter writer, PlaceStructure? placeStructure, JsonSerializer serializer)
    {
        if (placeStructure == null) throw new ArgumentNullException(nameof(placeStructure));

        serializer.Serialize(writer, new PlaceJson(placeStructure));
    }
}

internal class PlaceJson : GedcomJson
{
    public PlaceJson(PlaceStructure placeStructure)
    {
        Hierarchy = JsonString(placeStructure.PlaceHierarchy);
        Map = JsonRecord(placeStructure.Map);
        Name = JsonString(placeStructure.PlaceName);
        Notes = JsonList(placeStructure.NoteStructures);
        PhoneticVariations = JsonList(placeStructure.PlacePhoneticVariations);
        RomanizedVariations = JsonList(placeStructure.PlaceRomanizedVariations);
    }

    public string? Hierarchy { get; set; }
    public Map? Map { get; set; }
    public string? Name { get; set; }
    public List<NoteStructure>? Notes { get; set; }
    public List<NameVariation>? PhoneticVariations { get; set; }
    public List<NameVariation>? RomanizedVariations { get; set; }
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