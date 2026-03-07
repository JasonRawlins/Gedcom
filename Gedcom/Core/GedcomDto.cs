using System.Text.Json;
using System.Text.Json.Serialization;

namespace Gedcom;

/*
 * All RecordStructure classes are serialized as GedcomDto objects. This allows 
 * some properites to be marked as null when a certain condition is met. For example,
 * when a list has no items in it, we don't always want to return the empty array, we 
 * may want to make it null. That way the property can be ignored later during json 
 * serialization. The reason I did this is because most of the gedcom object graph 
 * doesn't contain any data. And by most, I mean 90%. The front end will have to
 * handle these missing properties. 
 */
public class GedcomDto
{
    [JsonIgnore]
    public bool IsEmpty { get; set; }
    protected static T? GetRecord<T>(T gedcomJson) where T : GedcomDto
    {
        if (gedcomJson.IsEmpty) 
            return null;

        return gedcomJson;
    }

    protected static string? GetString(string value) => string.IsNullOrEmpty(value) ? null : value;    
    protected static List<string>? GetList(List<string> stringList) => stringList.Count == 0 ? null : stringList;
    protected static List<GedcomDto>? GetList(List<GedcomDto> gedcomJson) => gedcomJson.Count == 0 ? null : gedcomJson;
    protected static List<T>? GetList<T>(List<T>? gedcomBaseList) where T : GedcomDto => 
        (gedcomBaseList == null || (gedcomBaseList != null && gedcomBaseList.Count == 0)) 
        ? null : gedcomBaseList;

    public static JsonSerializerOptions SerializationOptions
    {
        get
        {
            return new JsonSerializerOptions()
            {
                
                WriteIndented = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            };
        }
    }    
}