using Newtonsoft.Json;

namespace Gedcom.RecordStructures;

// The Gedcom Standard 5.5.1 documentation is at the end of this file.
[JsonConverter(typeof(FamilyPartnerJsonConverter))]
public class FamilyPartner : RecordStructureBase
{
    public FamilyPartner() { }
    public FamilyPartner(Record record) { }

    public string AgeAtEvent => Record.Records.FirstOrDefault(r => r.Tag.Equals(Tag.Age))?.Value ?? "";
    public string Name => Record.Value;

    public override string ToString() => $"{Record.Value}, {Name}";
}

internal class FamilyPartnerJsonConverter : JsonConverter<FamilyPartner>
{
    public override FamilyPartner? ReadJson(JsonReader reader, Type objectType, FamilyPartner? existingValue, bool hasExistingValue, JsonSerializer serializer) => throw new NotImplementedException();

    public override void WriteJson(JsonWriter writer, FamilyPartner? familyPartner, JsonSerializer serializer)
    {
        if (familyPartner == null) throw new ArgumentNullException(nameof(familyPartner));

        serializer.Serialize(writer, new FamilyPartnerJson(familyPartner));
    }
}

public class FamilyPartnerJson(FamilyPartner familyPartner) : GedcomJson
{
    public string? AgeAtEvent { get; set; } = JsonString(familyPartner.AgeAtEvent);
    public string? Name { get; set; } = JsonString(familyPartner.Name);
}