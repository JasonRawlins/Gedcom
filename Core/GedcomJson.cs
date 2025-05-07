namespace Gedcom;

/*
 * All RecordStructure classes are serialized as a GedcomJson object.
 * This allows some properites to be marked as null when a certain condition is met. For example,
 * when a list has no items in it we don't want to return the empty array, we want to mark it
 * as null. That way the property can be ignored later during json serialization. 
 */
internal class GedcomJson
{
    protected T? JsonRecord<T>(T recordStructureBase) where T : RecordStructureBase
    {
        if (recordStructureBase.IsEmpty) 
            return null;

        return recordStructureBase;
    }

    protected string? JsonString(string value) => string.IsNullOrEmpty(value) ? null : value;    
    protected List<string>? JsonList(List<string> stringList) => stringList.Count == 0 ? null : stringList;
    protected List<T>? JsonList<T>(List<T>? recordStructureBaseList) where T : RecordStructureBase => 
        (recordStructureBaseList == null || (recordStructureBaseList != null && recordStructureBaseList.Count == 0)) 
        ? null : recordStructureBaseList;
}

