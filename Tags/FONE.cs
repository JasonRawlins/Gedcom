using System.Text.Json;
using System.Text.Json.Serialization;

namespace Gedcom.Tags;

[JsonConverter(typeof(FONEJsonConverter))]
public class FONE : TagBase, IPersonalNamePieces
{
    public FONE(Record record) : base(record) { }

    public string TYPE => Val(C.TYPE);

    #region IPersonalNamePieces

    public string GIVN => Val(C.GIVN);

    public string NICK => Val(C.NICK);

    public string NPFX => Val(C.NPFX);

    public string NSFX => Val(C.NSFX);

    public string SPFX => Val(C.SPFX);

    public string SURN => Val(C.SURN);

    #endregion
}

public class FONEJsonConverter : JsonConverter<FONE>
{
    public override FONE? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException();
    
    public override void Write(Utf8JsonWriter writer, FONE fone, JsonSerializerOptions options)
    {
        var jsonObject = new
        {
            fone.Value,
            Given = fone.GIVN,
            Nickname = fone.NICK,
            NamePrefix = fone.NPFX,
            NameSuffix = fone.NSFX,
            SurnamePrefix = fone.SPFX,
            Surname = fone.SURN,
            Type = fone.TYPE
        };

        JsonSerializer.Serialize(writer, jsonObject, options);
    }
}

#region NAME_PHONETIC_VARIATION (FONE) p. 38
/* 
https://gedcom.io/specifications/ged551.pdf

+1 FONE <NAME_PHONETIC_VARIATION> {0:M} p.55
    +2 TYPE <PHONETIC_TYPE> {1:1} p.57
    +2 <<PERSONAL_NAME_PIECES>> {0:1} p.37

*/
#endregion