using Newtonsoft.Json;

namespace Gedcom.RecordStructures;

// The Gedcom Standard 5.5.1 documentation is at the end of this file.
[JsonConverter(typeof(PersonalNameStructureJsonConverter))]
public class PersonalNameStructure : RecordStructureBase, IPersonalNamePieces
{
    public PersonalNameStructure() : base() { }
    public PersonalNameStructure(Record record) : base(record) { }

    public string Given => _(Tag.GivenName);
    public string NamePersonal => Record.Value; // The complete name, (e.g. John /Doe/)
    public NameVariation NamePhoneticVariation => First<NameVariation>(Tag.Phonetic);
    public string NamePrefix => _(Tag.NamePrefix);
    public string NameSuffix => _(Tag.NameSuffix);
    public NameVariation NameRomanizedVariation => First<NameVariation>(Tag.Romanized);
    public string NameType => _(Tag.Type);
    public string Nickname => _(Tag.Nickname);
    public string Surname => _(Tag.Surname);
    public string SurnamePrefix => _(Tag.SurnamePrefix);

    public override string ToString() => $"{Record.Value}, {NamePersonal}";
}

internal class PersonalNameStructureJsonConverter : JsonConverter<PersonalNameStructure>
{
    public override PersonalNameStructure? ReadJson(JsonReader reader, Type objectType, PersonalNameStructure? existingValue, bool hasExistingValue, JsonSerializer serializer) => throw new NotImplementedException();

    public override void WriteJson(JsonWriter writer, PersonalNameStructure? personalNameStructure, JsonSerializer serializer)
    {
        if (personalNameStructure == null) throw new ArgumentNullException(nameof(personalNameStructure));

        serializer.Serialize(writer, new PersonalNameStructureJson(personalNameStructure));
    }
}

internal class PersonalNameStructureJson : GedcomJson
{
    public PersonalNameStructureJson(PersonalNameStructure personalNameStructure)
    {
        Given = JsonString(personalNameStructure.Given);
        Name = JsonString(personalNameStructure.NamePersonal);
        PhoneticVariation = JsonRecord(personalNameStructure.NamePhoneticVariation);
        Prefix = JsonString(personalNameStructure.NamePrefix);
        RomanizedVariation = JsonRecord(personalNameStructure.NameRomanizedVariation);
        Suffix = JsonString(personalNameStructure.NameSuffix);
        Type = JsonString(personalNameStructure.NameType);
        Nickname = JsonString(personalNameStructure.Nickname);
        Surname = JsonString(personalNameStructure.Surname);
        SurnamePrefix = JsonString(personalNameStructure.SurnamePrefix);
    }

    public string? Given { get; set; }
    public string? Name { get; set; }
    public NameVariation? PhoneticVariation { get; set; }
    public string? Prefix { get; set; }
    public NameVariation? RomanizedVariation { get; set; }
    public string? Suffix { get; set; }
    public string? Type { get; set; }
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