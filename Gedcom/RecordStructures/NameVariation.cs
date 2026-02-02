using System.Text.Json;
using System.Text.Json.Serialization;

namespace Gedcom.RecordStructures;

// The Gedcom Standard 5.5.1 documentation is at the end of this file.
[JsonConverter(typeof(NameVariationJsonConverter))]
public class NameVariation : RecordStructureBase, IPersonalNamePieces
{
    public NameVariation() { }
    public NameVariation(Record record) : base(record) { }

    private string? _fullName = null;
    public string FullName => _fullName ??= $"{Given} {Surname}";

    private string? _given = null;
    public string Given => _given ??= GetValue(Tag.GivenName);

    private string? _namePrefix = null;
    public string NamePrefix => _namePrefix ??= GetValue(Tag.NamePrefix);

    private string? _nameSuffix = null;
    public string NameSuffix => _nameSuffix ??= GetValue(Tag.NameSuffix);

    private string? _nickname = null;
    public string Nickname => _nickname ??= GetValue(Tag.Nickname);

    private string? _surname = null;
    public string Surname => _surname ??= GetValue(Tag.Surname);

    private string? _surnamePrefix = null;
    public string SurnamePrefix => _surnamePrefix ??= GetValue(Tag.SurnamePrefix);

    private string? _type = null;
    public string Type => _type ??= GetValue(Tag.Type);

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