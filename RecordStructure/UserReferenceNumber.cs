using System.Text.Json;
using System.Text.Json.Serialization;

namespace Gedcom.RecordStructure;

//[JsonConverter(typeof(ReferenceJsonConverter))]
public class UserReferenceNumber : RecordStructureBase
{
    public UserReferenceNumber(Record record) : base(record) { }

    public string Type => V(C.TYPE);
}

//public class ReferenceJsonConverter : JsonConverter<UserReferenceNumber>
//{
//    public override UserReferenceNumber? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException();
    
//    public override void Write(Utf8JsonWriter writer, UserReferenceNumber reference, JsonSerializerOptions options)
//    {
//        var jsonObject = new
//        {
//            Id = reference.Value,
//            reference.Type
//        };

//        JsonSerializer.Serialize(writer, jsonObject, options);
//    }
//}

#region USER_REFERENCE_TYPE (REFN) p. 27
/* 
https://gedcom.io/specifications/ged551.pdf

n @
    +1 REFN <USER_REFERENCE_NUMBER> {0:M} p.63, 64
        +2 TYPE <USER_REFERENCE_TYPE> {0:1} p.64

*/
#endregion