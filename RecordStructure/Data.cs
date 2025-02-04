namespace Gedcom.RecordStructure;

//[JsonConverter(typeof(DATAJsonConverter))]
public class Data : RecordStructureBase
{
    public Data() : base() { }
    public Data(Record record) : base(record) { }

    public string DATE => V(C.DATE);
}

//public class DATAJsonConverter : JsonConverter<Data>
//{
//    public override Data? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException();
    
//    public override void Write(Utf8JsonWriter writer, Data data, JsonSerializerOptions options)
//    {
//        var jsonObject = new
//        {
//            Date = data.DATE,
//            data.Text()
//        };

//        JsonSerializer.Serialize(writer, jsonObject, options);
//    }
//}

#region STRUCTURE_NAME (DATA) p. 39
/* 
https://gedcom.io/specifications/ged551.pdf

n SOUR @<XREF:SOUR>@ {1:1} p.27
    +1 DATA {0:1}
        +2 DATE <ENTRY_RECORDING_DATE> {0:1} p.48
        +2 TEXT <TEXT_FROM_SOURCE> {0:M} p.63
            +3 [CONC|CONT] <TEXT_FROM_SOURCE> {0:M}

*/
#endregion