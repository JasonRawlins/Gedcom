using System.Text.Json;
using System.Text.Json.Serialization;

namespace Gedcom.RecordStructure;

//[JsonConverter(typeof(SpouseToFamilyLinkJsonConverter))]
public class SpouseToFamilyLink : RecordStructureBase
{
    public SpouseToFamilyLink() : base() { }
    public SpouseToFamilyLink(Record record) : base(record) { }
    
    public List<NoteStructure>? NoteStructure
    {
        get
        {
            var noteStructures = List(C.FAMS);
            if (noteStructures != null)
            {
                return noteStructures.Select(r => new NoteStructure(r)).ToList();
            }

            return null;
        }
    }
}

//public class SpouseToFamilyLinkJsonConverter : JsonConverter<SpouseToFamilyLink>
//{
//    public override SpouseToFamilyLink? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException();

//    public override void Write(Utf8JsonWriter writer, SpouseToFamilyLink SpouseToFamilyLink, JsonSerializerOptions options)
//    {
//        var jsonObject = new
//        {
//        };

//        JsonSerializer.Serialize(writer, jsonObject, options);
//    }
//}

#region SPOUSE_TO_FAMILY_LINK p. 40
/* 
https://gedcom.io/specifications/ged551.pdf

SPOUSE_TO_FAMILY_LINK:=

n FAMS @<XREF:FAM>@ {1:1} p.24
    +1 <<NOTE_STRUCTURE>> {0:M} p.37

*/
#endregion