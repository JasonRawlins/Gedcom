using System.Text.Json;
using System.Text.Json.Serialization;

namespace Gedcom.RecordStructure;

//[JsonConverter(typeof(PersonalNameStructureJsonConverter))]
public class PersonalNameStructure : RecordStructureBase, IPersonalNamePieces
{
    public PersonalNameStructure(Record record) : base(record) { }

    public NameVariation? Phonetic
    {
        get
        {
            var foneRecord = FirstOrDefault(C.FONE);
            if (foneRecord != null)
            {
                return new NameVariation(foneRecord);
            }

            return null;
        }
    }

    public NameVariation? Romanized
    {
        get
        {
            var romnRecord = FirstOrDefault(C.ROMN);
            if (romnRecord != null)
            {
                return new NameVariation(romnRecord);
            }

            return null;
        }
    }

    public string Name => Record.Value;
    public string Type => V(C.TYPE);

    #region IPersonalNamePeices

    public string Given => V(C.GIVN);

    public string Nickname => V(C.NICK);

    public string NamePrefix => V(C.NPFX);

    public string NameSuffix => V(C.NSFX);

    public string SurnamePrefix => V(C.SPFX);

    public string Surname => V(C.SURN);

    #endregion
}

//public class PersonalNameStructureJsonConverter: JsonConverter<PersonalNameStructure>
//{
//    public override PersonalNameStructure? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException();
//    public override void Write(Utf8JsonWriter writer, PersonalNameStructure name, JsonSerializerOptions options)
//    {
//        var jsonObject = new
//        {
//            name.Given,
//            name.Name,
//            name.Nickname,
//            name.NamePrefix,
//            NameSufix = name.NameSuffix,
//            name.SurnamePrefix,
//            name.Surname,
//            name.Type
//        };

//        JsonSerializer.Serialize(writer, jsonObject, options);
//    }
//}

#region PERSONAL_NAME_STRUCTURE p. 38
/*
https://gedcom.io/specifications/ged551.pdf

PERSONAL_NAME_STRUCTURE:=

n NAME <NAME_PERSONAL> {1:1} p.54
    +1 TYPE <NAME_TYPE> {0:1} p.56
    +1 <<PERSONAL_NAME_PIECES>> {0:1} p.37
    +1 FONE <NAME_PHONETIC_VARIATION> {0:M} p.55
        +2 TYPE <PHONETIC_TYPE> {1:1} p.57
        +2 <<PERSONAL_NAME_PIECES>> {0:1} p.37
    +1 ROMN <NAME_ROMANIZED_VARIATION> {0:M} p.56
        +2 TYPE <ROMANIZED_TYPE> {1:1} p.61
        +2 <<PERSONAL_NAME_PIECES>> {0:1} p.37

The name value is formed in the manner the name is normally spoken, with the given name and family
name (surname) separated by slashes (/). (See <NAME_PERSONAL>, page 54.) Based on the
dynamic nature or unknown compositions of naming conventions, it is difficult to provide more
detailed name piece structure to handle every case. The NPFX, GIVN, NICK, SPFX, SURN, and
NSFX tags are provided optionally for systems that cannot operate effectively with less structured
information. For current future compatibility, all systems must construct their names based on the
<NAME_PERSONAL> structure. Those using the optional name pieces should assume that few
systems will process them, and most will not provide the name pieces.

A <NAME_TYPE> is used to specify the particular variation that this name is. For example; if the
name type is subordinate to the <NAME_PERSONAL> it could indicate that this name is a name
taken at immigration or that it could be an ‘also known as’ name (see page 56.)

Future GEDCOM releases (6.0 or later) will likely apply a very different strategy to resolve this
problem, possibly using a sophisticated parser and a name-knowledge database.

*/
#endregion