using Gedcom.Core;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Gedcom.RecordStructures;

// The Gedcom Standard 5.5.1 documentation is at the end of this file.
[JsonConverter(typeof(NameVariationJsonConverter))]
public class NameVariation : RecordStructureBase, IPersonalNamePieces
{
    public NameVariation() { }
    public NameVariation(Record record) : base(record) { }

    public string FullName => $"{Given} {Surname}";
    public string Given => GetValue(Tag.GivenName);
    public string NamePrefix => GetValue(Tag.NamePrefix);
    public string NameSuffix => GetValue(Tag.NameSuffix);
    public string Nickname => GetValue(Tag.Nickname);
    public string Surname => GetValue(Tag.Surname);
    public string SurnamePrefix => GetValue(Tag.SurnamePrefix);
    public string Type => GetValue(Tag.Type);

    public override string ToString() => $"{Record.Value}, {Type}, {FullName}";
}

internal sealed class NameVariationJsonConverter : JsonConverter<NameVariation>
{
    public override NameVariation? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException();

    public override void Write(Utf8JsonWriter writer, NameVariation value, JsonSerializerOptions options)
    {
        ArgumentNullException.ThrowIfNull(value);
        JsonSerializer.Serialize(writer, new NameVariationJson(value), options);
    }
}

public class NameVariationJson(NameVariation nameVariation) : GedcomJson
{
    public string? Given { get; set; } = JsonString(nameVariation.Type);
    public string? Nickname { get; set; } = JsonString(nameVariation.Type);
    public string? Prefix { get; set; } = JsonString(nameVariation.Type);
    public string? Suffix { get; set; } = JsonString(nameVariation.Type);
    public string? Surname { get; set; } = JsonString(nameVariation.Type);
    public string? SurnamePrefix { get; set; } = JsonString(nameVariation.Type);
    public string? Type { get; set; } = JsonString(nameVariation.Type);

    public override string ToString() => $"{Given} {Surname}";
}

#region NAME_PHONETIC_VARIATION (FONE) and NAME_ROMANIZED_VARIATION (ROMN) p. 38
/* 

n NAME
    +1 FONE <NAME_PHONETIC_VARIATION> {0:M} p.55
        +2 TYPE <PHONETIC_TYPE> {1:1} p.57
        +2 <<PERSONAL_NAME_PIECES>> {0:1} p.37
    +1 ROMN <NAME_ROMANIZED_VARIATION> {0:M} p.56
        +2 TYPE <ROMANIZED_TYPE> {1:1} p.61
        +2 <<PERSONAL_NAME_PIECES>> {0:1} p.37

*/
#endregion