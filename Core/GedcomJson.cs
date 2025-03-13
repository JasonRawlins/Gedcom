namespace Gedcom.Core;

internal class GedcomJson
{
    public static bool ReturnAllValues { get; set; }

    protected T? JsonRecord<T>(T recordStructureBase) where T : RecordStructureBase
    {
        if (ReturnAllValues) 
            return recordStructureBase;

        if (recordStructureBase.IsEmpty) 
            return null;

        return recordStructureBase;
    }

    // JsonString() is probably not necessary. I've put it in enough places that I don't want to remove
    // it until I'm sure. For now it will just return the passed in string. 
    protected string? JsonString(string value) => value; // Empty is probably a valid value. string.IsNullOrEmpty(value) ? null : value; // 
    
    protected List<string>? JsonList(List<string> stringList)
    {
        if (ReturnAllValues)
            return stringList;

        if (stringList.Count == 0)
            return null;
        
        return stringList;
    }

    protected List<T>? JsonList<T>(List<T> recordStructureBaseList) where T : RecordStructureBase
    {
        if (ReturnAllValues)
            return recordStructureBaseList;

        if (recordStructureBaseList.Count == 0)
            return null;
        
        return recordStructureBaseList;
    }
}

