using System.Text.Json;
using System.Text.Json.Serialization;

namespace Gedcom.Tags;

[JsonConverter(typeof(FONE_ROMNJsonConverter))]
public class FONE_ROMN : TagBase, IPersonalNamePieces
{
    public FONE_ROMN(Record record) : base(record) { }

    public string TYPE => V(C.TYPE);

    #region IPersonalNamePieces

    public string GIVN => V(C.GIVN);
    public string NICK => V(C.NICK);
    public string NPFX => V(C.NPFX);
    public string NSFX => V(C.NSFX);
    public string SPFX => V(C.SPFX);
    public string SURN => V(C.SURN);

    #endregion
}

public class FONE_ROMNJsonConverter : JsonConverter<FONE_ROMN>
{
    public override FONE_ROMN? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException();
    
    public override void Write(Utf8JsonWriter writer, FONE_ROMN fone_romn, JsonSerializerOptions options)
    {
        var jsonObject = new
        {
            fone_romn.Value,
            Given = fone_romn.GIVN,
            Nickname = fone_romn.NICK,
            NamePrefix = fone_romn.NPFX,
            NameSuffix = fone_romn.NSFX,
            SurnamePrefix = fone_romn.SPFX,
            Surname = fone_romn.SURN,
            Type = fone_romn.TYPE
        };

        JsonSerializer.Serialize(writer, jsonObject, options);
    }
}

#region NAME_PHONETIC_VARIATION (FONE) p. 38
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