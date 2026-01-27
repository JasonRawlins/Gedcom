using Newtonsoft.Json;

namespace Gedcom.RecordStructures;

// The Gedcom Standard 5.5.1 documentation is at the end of this file.
[JsonConverter(typeof(LdsOrdinanceStatusJsonConverter))]
public class LdsOrdinanceStatus : RecordStructureBase
{
    public string ChangeDate => GetValue(Tag.Date);
    public string Status => Record.Value;

    public override string ToString() => $"{Record.Value}, {Status}, {ChangeDate}";
}

internal class LdsOrdinanceStatusJsonConverter : JsonConverter<LdsOrdinanceStatus>
{
    public override LdsOrdinanceStatus? ReadJson(JsonReader reader, Type objectType, LdsOrdinanceStatus? existingValue, bool hasExistingValue, JsonSerializer serializer) => throw new NotImplementedException();

    public override void WriteJson(JsonWriter writer, LdsOrdinanceStatus? ldsOrdinanceStatus, JsonSerializer serializer)
    {
        if (ldsOrdinanceStatus == null) throw new ArgumentNullException(nameof(ldsOrdinanceStatus));

        serializer.Serialize(writer, new LdsOrdinanceStatusJson(ldsOrdinanceStatus));
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

