using System.Text.Json;
using System.Text.Json.Serialization;

namespace Gedcom.RecordStructures;

// The Gedcom Standard 5.5.1 documentation is at the end of this file.
[JsonConverter(typeof(FamilyPartnerJsonConverter))]
public class FamilyPartner : RecordStructureBase
{
    public FamilyPartner() { }
    public FamilyPartner(Record record) { }

    private string? _ageAtEvent = null;
    public string AgeAtEvent => _ageAtEvent ??= Record.Records.FirstOrDefault(r => r.Tag.Equals(Tag.Age))?.Value ?? "";
   
    public string Name => Record.Value;

    public override string ToString() => $"{Record.Value}, {Name}";
}

internal sealed class FamilyPartnerJsonConverter : JsonConverter<FamilyPartner>
{
    public override FamilyPartner? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException();

    public override void Write(Utf8JsonWriter writer, FamilyPartner value, JsonSerializerOptions options)
    {
        ArgumentNullException.ThrowIfNull(value);
        JsonSerializer.Serialize(writer, new FamilyPartnerDto(value), GedcomDto.SerializationOptions);
    }
}

public class FamilyPartnerDto(FamilyPartner familyPartner) : GedcomDto
{
    public string? AgeAtEvent { get; set; } = GetString(familyPartner.AgeAtEvent);
    public string? Name { get; set; } = GetString(familyPartner.Name);
    public override string ToString() => $"{Name}";
}