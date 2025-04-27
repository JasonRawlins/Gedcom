using System.Text.Json;
using System.Text.Json.Serialization;

namespace Gedcom.RecordStructures;

// The Gedcom Standard 5.1.1 documentation is at the end of this file.
[JsonConverter(typeof(PersonalNameStructureJsonConverter))]
public class PersonalNameStructure : RecordStructureBase, IPersonalNamePieces
{
    public PersonalNameStructure() : base() { }
    public PersonalNameStructure(Record record) : base(record) { }

    public string Given => _(C.GIVN);
    public string NamePersonal => Record.Value;
    public NameVariation NamePhoneticVariation => First<NameVariation>(C.FONE);
    public string NamePrefix => _(C.NPFX);
    public string NameSuffix => _(C.NSFX);
    public NameVariation NameRomanizedVariation => First<NameVariation>(C.ROMN);
    public string NameType => _(C.TYPE);
    public string Nickname => _(C.NICK);
    public string Surname => _(C.SURN);
    public string SurnamePrefix => _(C.SPFX);

    public override string ToString() => $"{Record.Value}, {NamePersonal}";
}

internal class PersonalNameStructureJsonConverter : JsonConverter<PersonalNameStructure>
{
    public override PersonalNameStructure? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException();
    public override void Write(Utf8JsonWriter writer, PersonalNameStructure personalNameStructure, JsonSerializerOptions options)
    {
        var personalNameStructureJson = new PersonalNameStructureJson(personalNameStructure);
        JsonSerializer.Serialize(writer, personalNameStructureJson, personalNameStructureJson.GetType(), options);
    }
}

internal class PersonalNameStructureJson : GedcomJson
{
    public PersonalNameStructureJson(PersonalNameStructure personalNameStructure)
    {
        Given = JsonString(personalNameStructure.Given);
        NamePersonal = JsonString(personalNameStructure.NamePersonal);
        NamePhoneticVariation = JsonRecord(personalNameStructure.NamePhoneticVariation);
        NamePrefix = JsonString(personalNameStructure.NamePrefix);
        NameRomanizedVariation = JsonRecord(personalNameStructure.NameRomanizedVariation);
        NameSuffix = JsonString(personalNameStructure.NameSuffix);
        NameType = JsonString(personalNameStructure.NameType);
        Nickname = JsonString(personalNameStructure.Nickname);
        Surname = JsonString(personalNameStructure.Surname);
        SurnamePrefix = JsonString(personalNameStructure.SurnamePrefix);
    }

    public string? Given { get; set; }
    public string? NamePersonal { get; set; }
    public NameVariation? NamePhoneticVariation { get; set; }
    public string? NamePrefix { get; set; }
    public NameVariation? NameRomanizedVariation { get; set; }
    public string? NameSuffix { get; set; }
    public string? NameType { get; set; }
    public string? Nickname { get; set; }
    public string? Surname { get; set; }
    public string? SurnamePrefix { get; set; }

}

#region PERSONAL_NAME_STRUCTURE p. 38
/*

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