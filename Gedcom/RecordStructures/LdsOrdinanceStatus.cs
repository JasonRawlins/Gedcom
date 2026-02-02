using System.Text.Json;
using System.Text.Json.Serialization;

namespace Gedcom.RecordStructures;

// The Gedcom Standard 5.5.1 documentation is at the end of this file.
[JsonConverter(typeof(LdsOrdinanceStatusJsonConverter))]
public class LdsOrdinanceStatus : RecordStructureBase
{
    private string? _changeDate = null;
    public string ChangeDate => _changeDate ??= GetValue(Tag.Date);

    private string? _status = null;
    public string Status => _status ??= Record.Value;

    public override string ToString() => $"{Record.Value}, {Status}, {ChangeDate}";
}

internal sealed class LdsOrdinanceStatusJsonConverter : JsonConverter<LdsOrdinanceStatus>
{
    public override LdsOrdinanceStatus? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException();

    public override void Write(Utf8JsonWriter writer, LdsOrdinanceStatus value, JsonSerializerOptions options)
    {
        ArgumentNullException.ThrowIfNull(value);
        JsonSerializer.Serialize(writer, new LdsOrdinanceStatusJson(value), options);
    }
}

public class LdsOrdinanceStatusJson(LdsOrdinanceStatus ldsOrdinanceStatus) : GedcomJson
{
    public string? ChangeDate { get; set; } = JsonString(ldsOrdinanceStatus.ChangeDate);
    public string? Status { get; set; } = JsonString(ldsOrdinanceStatus.Status);
    public override string ToString() => $"{Status}";
}

#region STRUCTURE_NAME p. 
/* 

n SLGS {1:1}
    +1 STAT <LDS_SPOUSE_SEALING_DATE_STATUS> {0:1} p.52
        +2 DATE <CHANGE_DATE> {1:1}

*/
#endregion

