using Gedcom.Tags;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Gedcom;

[JsonConverter(typeof(NAMEJsonConverter))]
public class NAME : TagBase
{
    public NAME(Record record) : base(record) { }

    public string Name => Value(T.NAME);
}

public class NAMEJsonConverter: JsonConverter<NAME>
{
    public override NAME? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException();
    public override void Write(Utf8JsonWriter writer, NAME value, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }
}

#region PERSONAL_NAME_STRUCTURE (NAME) p. 38
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