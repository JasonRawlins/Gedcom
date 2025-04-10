namespace Gedcom;

internal class GedcomJson
{
    protected T? JsonRecord<T>(T recordStructureBase) where T : RecordStructureBase
    {
        if (recordStructureBase.IsEmpty) 
            return null;

        return recordStructureBase;
    }

    public string? Xref { get; set; }
    protected string? JsonString(string value) => string.IsNullOrEmpty(value) ? null : value;    
    protected List<string>? JsonList(List<string> stringList) => stringList.Count == 0 ? null : stringList;
    protected List<T>? JsonList<T>(List<T>? recordStructureBaseList) where T : RecordStructureBase => 
        (recordStructureBaseList == null || (recordStructureBaseList != null && recordStructureBaseList.Count == 0)) 
        ? null : recordStructureBaseList;
}

