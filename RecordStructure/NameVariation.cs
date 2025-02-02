using System.Text.Json;
using System.Text.Json.Serialization;

namespace Gedcom.RecordStructure;

[JsonConverter(typeof(NameVariationJsonConverter))]
public class NameVariation : RecordStructureBase, IPersonalNamePieces
{
    public NameVariation(Record record) : base(record) { }

    public string Type => V(C.TYPE);

    #region IPersonalNamePieces

    public string Given => V(C.GIVN);
    public string Nickname => V(C.NICK);
    public string NamePrefix => V(C.NPFX);
    public string NameSuffix => V(C.NSFX);
    public string SurnamePrefix => V(C.SPFX);
    public string Surname => V(C.SURN);

    #endregion
}

public class NameVariationJsonConverter : JsonConverter<NameVariation>
{
    public override NameVariation? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException();
    
    public override void Write(Utf8JsonWriter writer, NameVariation fone_romn, JsonSerializerOptions options)
    {
        var jsonObject = new
        {
            fone_romn.Value,
            fone_romn.Given,
            fone_romn.Nickname,
            fone_romn.NamePrefix,
            fone_romn.NameSuffix,
            fone_romn.SurnamePrefix,
            fone_romn.Surname,
            fone_romn.Type
        };

        JsonSerializer.Serialize(writer, jsonObject, options);
    }
}

#region NAME_PHONETIC_VARIATION (FONE) and NAME_ROMANIZED_VARIATION (ROMN) p. 38
/* 
https://gedcom.io/specifications/ged551.pdf

n NAME
    +1 FONE <NAME_PHONETIC_VARIATION> {0:M} p.55
        +2 TYPE <PHONETIC_TYPE> {1:1} p.57
        +2 <<PERSONAL_NAME_PIECES>> {0:1} p.37
    +1 ROMN <NAME_ROMANIZED_VARIATION> {0:M} p.56
        +2 TYPE <ROMANIZED_TYPE> {1:1} p.61
        +2 <<PERSONAL_NAME_PIECES>> {0:1} p.37

*/
#endregion