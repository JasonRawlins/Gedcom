using Gedcom.Core;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Gedcom.RecordStructures;

[JsonConverter(typeof(SourceCitationDataJsonConverter))]
public class SourceCitationData : RecordStructureBase
{
    public SourceCitationData() : base() { }
    public SourceCitationData(Record record) : base(record) { }

    public string EntryRecordingDate => _(C.DATE);
}

internal class SourceCitationDataJsonConverter : JsonConverter<SourceCitationData>
{
    public override SourceCitationData? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException();
    public override void Write(Utf8JsonWriter writer, SourceCitationData sourceCitationData, JsonSerializerOptions options)
    {
        var sourceCitationDataJson = new SourceCitationDataJson(sourceCitationData);
        JsonSerializer.Serialize(writer, sourceCitationDataJson, sourceCitationDataJson.GetType(), options);
    }
}

internal class SourceCitationDataJson
{
    public SourceCitationDataJson(SourceCitationData sourceCitationData)
    {
        EntryRecordingDate = string.IsNullOrEmpty(sourceCitationData.EntryRecordingDate) ? null : sourceCitationData.EntryRecordingDate;
    }

    public string? EntryRecordingDate { get; set; }
}

#region SOUR.DATA p. 39
/* 

n SOUR @<XREF:SOUR>@ {1:1} p.27
    +1 DATA {0:1}
        +2 DATE <ENTRY_RECORDING_DATE> {0:1} p.48
        +2 TEXT <TEXT_FROM_SOURCE> {0:M} p.63
            +3 [CONC|CONT] <TEXT_FROM_SOURCE> {0:M}

*/
#endregion