using System.Text.Json;
using System.Text.Json.Serialization;

namespace Gedcom.RecordStructures;

[JsonConverter(typeof(FamilyPartnerJsonConverter))]
public class FamilyPartner : RecordStructureBase
{
    public FamilyPartner() { }
    public FamilyPartner(Record record) { }

    public string AgeAtEvent => Record.Records.FirstOrDefault(r => r.Tag.Equals(C.AGE))?.Value ?? "";
    public string Name => Record.Value;

    public override string ToString() => $"{Record.Value}, {Name}";
}

internal class FamilyPartnerJsonConverter : JsonConverter<FamilyPartner>
{
    public override FamilyPartner? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException();
    public override void Write(Utf8JsonWriter writer, FamilyPartner familyPartner, JsonSerializerOptions options)
    {
        var familyPartnerJson = new FamilyPartnerJson(familyPartner);
        JsonSerializer.Serialize(writer, familyPartnerJson, familyPartnerJson.GetType(), options);
    }
}

internal class FamilyPartnerJson : GedcomJson
{
    public FamilyPartnerJson(FamilyPartner familyPartner)
    {
        AgeAtEvent = JsonString(familyPartner.AgeAtEvent);
        Name = JsonString(familyPartner.Name);
    }

    public string? AgeAtEvent { get; set; }
    public string? Name { get; set; }
}