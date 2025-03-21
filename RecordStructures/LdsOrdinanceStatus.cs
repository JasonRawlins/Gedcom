using System.Text.Json;
using System.Text.Json.Serialization;

namespace Gedcom.RecordStructures;

[JsonConverter(typeof(LdsOrdinanceStatusJsonConverter))]
public class LdsOrdinanceStatus : RecordStructureBase
{
    public string Status => Record.Value;
    public string ChangeDate => _(C.DATE);
}

internal class LdsOrdinanceStatusJsonConverter : JsonConverter<LdsOrdinanceStatus>
{
    public override LdsOrdinanceStatus? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException();
    public override void Write(Utf8JsonWriter writer, LdsOrdinanceStatus ldsOrdinanceStatus, JsonSerializerOptions options)
    {
        var ldsOrdinanceStatusJson = new LdsOrdinanceStatusJson(ldsOrdinanceStatus);
        JsonSerializer.Serialize(writer, ldsOrdinanceStatusJson, ldsOrdinanceStatusJson.GetType(), options);
    }
}

internal class LdsOrdinanceStatusJson : GedcomJson
{
    public LdsOrdinanceStatusJson(LdsOrdinanceStatus ldsOrdinanceStatus)
    {
        Status = JsonString(ldsOrdinanceStatus.Status);
        ChangeDate = JsonString(ldsOrdinanceStatus.ChangeDate);
    }

    public string? Status { get; set; }
    public string? ChangeDate { get; set; }
}

#region STRUCTURE_NAME p. 
/* 

n SLGS {1:1}
    +1 STAT <LDS_SPOUSE_SEALING_DATE_STATUS> {0:1} p.52
        +2 DATE <CHANGE_DATE> {1:1}

*/
#endregion

