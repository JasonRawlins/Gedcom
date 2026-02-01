using Gedcom.Core;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Gedcom.RecordStructures;

// The Gedcom Standard 5.5.1 documentation is at the end of this file.
[JsonConverter(typeof(PersonalNameStructureJsonConverter))]
public class PersonalNameStructure : RecordStructureBase, IPersonalNamePieces
{
    public PersonalNameStructure() : base() { }
    public PersonalNameStructure(Record record) : base(record) { }

    public string Given => GetValue(Tag.GivenName);
    public string NamePersonal => Record.Value; // The complete name, (e.g. John /Doe/)
    public NameVariation NamePhoneticVariation => First<NameVariation>(Tag.Phonetic);
    public string NamePrefix => GetValue(Tag.NamePrefix);
    public string NameSuffix => GetValue(Tag.NameSuffix);
    public NameVariation NameRomanizedVariation => First<NameVariation>(Tag.Romanized);
    public string NameType => GetValue(Tag.Type);
    public string Nickname => GetValue(Tag.Nickname);
    public string Surname => GetValue(Tag.Surname);
    public string SurnamePrefix => GetValue(Tag.SurnamePrefix);

    public override string ToString() => $"{Record.Value}, {NamePersonal}";
}

internal sealed class PersonalNameStructureJsonConverter : JsonConverter<PersonalNameStructure>
{
    public override PersonalNameStructure? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException();

    public override void Write(Utf8JsonWriter writer, PersonalNameStructure value, JsonSerializerOptions options)
    {
        ArgumentNullException.ThrowIfNull(value);
        JsonSerializer.Serialize(writer, new PersonalNameJson(value), options);
    }
}

public class PersonalNameJson(PersonalNameStructure personalNameStructure) : GedcomJson
{
    public string? Given { get; set; } = JsonString(personalNameStructure.Given);
    public string? Name { get; set; } = JsonString(personalNameStructure.NamePersonal);
    public string? Nickname { get; set; } = JsonString(personalNameStructure.Nickname);
    public NameVariationJson? PhoneticVariation { get; set; } = JsonRecord(new NameVariationJson(personalNameStructure.NamePhoneticVariation));
    public string? Prefix { get; set; } = JsonString(personalNameStructure.NamePrefix);
    public NameVariationJson? RomanizedVariation { get; set; } = JsonRecord(new NameVariationJson(personalNameStructure.NameRomanizedVariation));
    public string? Suffix { get; set; } = JsonString(personalNameStructure.NameSuffix);
    public string? Surname { get; set; } = JsonString(personalNameStructure.Surname);
    public string? SurnamePrefix { get; set; } = JsonString(personalNameStructure.SurnamePrefix);
    public string? Type { get; set; } = JsonString(personalNameStructure.NameType);
    public override string ToString() => $"{Given} {Surname}";
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