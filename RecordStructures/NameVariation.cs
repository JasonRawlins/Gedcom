using Gedcom.Core;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Gedcom.RecordStructures;

[JsonConverter(typeof(NameVariationJsonConverter))]
public class NameVariation : RecordStructureBase, IPersonalNamePieces
{
    public NameVariation() { }
    public NameVariation(Record record) : base(record) { }

    public string Type => _(C.TYPE); // PHONETIC_TYPE or ROMANIZED_TYPE

    #region IPersonalNamePieces

    public string Given => _(C.GIVN);
    public string NamePrefix => _(C.NPFX);
    public string NameSuffix => _(C.NSFX);
    public string Nickname => _(C.NICK);
    public string Surname => _(C.SURN);
    public string SurnamePrefix => _(C.SPFX);

    #endregion
}

internal class NameVariationJsonConverter : JsonConverter<NameVariation>
{
    public override NameVariation? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException();
    public override void Write(Utf8JsonWriter writer, NameVariation nameVariation, JsonSerializerOptions options)
    {
        var nameVariationJson = new NameVariationJson(nameVariation);
        JsonSerializer.Serialize(writer, nameVariationJson, nameVariationJson.GetType(), options);
    }
}

internal class NameVariationJson
{
    public NameVariationJson(NameVariation nameVariation)
    {
        Type = string.IsNullOrEmpty(nameVariation.Type) ? null : nameVariation.Type;
        Given = string.IsNullOrEmpty(nameVariation.Given) ? null : nameVariation.Type;
        NamePrefix = string.IsNullOrEmpty(nameVariation.NamePrefix) ? null : nameVariation.Type;
        NameSuffix = string.IsNullOrEmpty(nameVariation.NameSuffix) ? null : nameVariation.Type;
        Nickname = string.IsNullOrEmpty(nameVariation.Nickname) ? null : nameVariation.Type;
        Surname = string.IsNullOrEmpty(nameVariation.Surname) ? null : nameVariation.Type;
        SurnamePrefix = string.IsNullOrEmpty(nameVariation.SurnamePrefix) ? null : nameVariation.Type;
    }

    public string? Type { get; set; }

    #region IPersonalNamePieces

    public string? Given { get; set; }
    public string? NamePrefix { get; set; }
    public string? NameSuffix { get; set; }
    public string? Nickname { get; set; }
    public string? Surname { get; set; }
    public string? SurnamePrefix { get; set; }

    #endregion
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