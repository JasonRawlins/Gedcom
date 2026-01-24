using Gedcom.RecordStructures;

namespace Gedcom;

/*
 * All RecordStructure classes are serialized as GedcomJson objects. This allows 
 * some properites to be marked as null when a certain condition is met. For example,
 * when a list has no items in it we don't want to return the empty array, we want to
 * make it null. That way the property can be ignored later during json serialization. 
 * The reason I did this is because most of the gedcom object graph doesn't contain 
 * any data. And by most, I mean 90%. The front end will have to handle these missing 
 * properties. 
 */
public class GedcomJson
{
    public bool IsEmpty { get; set; }
    protected static T? JsonRecord<T>(T gedcomJson) where T : GedcomJson
    {
        if (gedcomJson.IsEmpty) 
            return null;

        return gedcomJson;
    }

    protected static string? JsonString(string value) => string.IsNullOrEmpty(value) ? null : value;    
    protected static List<string>? JsonList(List<string> stringList) => stringList.Count == 0 ? null : stringList;
    protected static List<GedcomJson>? JsonList(List<GedcomJson> gedcomJson) => gedcomJson.Count == 0 ? null : gedcomJson;
    protected static List<T>? JsonList<T>(List<T>? gedcomBaseList) where T : GedcomJson => 
        (gedcomBaseList == null || (gedcomBaseList != null && gedcomBaseList.Count == 0)) 
        ? null : gedcomBaseList;
}