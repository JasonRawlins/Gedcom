using Gedcom.Core;
using Newtonsoft.Json;

namespace Gedcom.RecordStructures;

// The Gedcom Standard 5.5.1 documentation is at the end of this file.
[JsonConverter(typeof(CharacterSetJsonConverter))]
public class CharacterSet : RecordStructureBase
{
    public CharacterSet() : base() { }
    public CharacterSet(Record record) : base(record) { }

    public string VersionNumber => GetValue(Tag.Version);

    public override string ToString() => $"{Record.Value}, {VersionNumber}";
}

internal class CharacterSetJsonConverter : JsonConverter<CharacterSet>
{
    public override CharacterSet? ReadJson(JsonReader reader, Type objectType, CharacterSet? existingValue, bool hasExistingValue, JsonSerializer serializer) => throw new NotImplementedException();

    public override void WriteJson(JsonWriter writer, CharacterSet? characterSet, JsonSerializer serializer)
    {
        ArgumentNullException.ThrowIfNull(characterSet);
        serializer.Serialize(writer, new CharacterSetJson(characterSet));
    }
}

public class CharacterSetJson(CharacterSet characterSet) : GedcomJson
{
    public string? VersionNumber { get; set; } = JsonString(characterSet.VersionNumber);
    public override string ToString() => $"{VersionNumber}";
}

#region CHARACTER_SET p. 23
/* 

CHARACTER_SET:= 

[ ANSEL |UTF-8 | UNICODE | ASCII ]

A code value that represents the character set to be used to interpret this data. Currently, the
preferred character set is ANSEL, which includes ASCII as a subset. UNICODE is not widely
supported by most operating systems; therefore, GEDCOM produced using the UNICODE character
set will be limited in its interchangeability for a while but should eventually provide the international
flexibility that is desired. See Chapter 3, starting on page 77.

Note:The IBMPC character set is not allowed. This character set cannot be interpreted properly
without knowing which code page the sender was using.

n HEAD
    +1 CHAR <CHARACTER_SET> {1:1} p.44
        +2 VERS <VERSION_NUMBER> {0:1} p.64

*/
#endregion