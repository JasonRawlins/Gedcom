namespace Gedcom.Core;

internal class GedcomJson
{
    protected T? JsonRecord<T>(T recordStructureBase) where T : RecordStructureBase
    {
        if (recordStructureBase.IsEmpty)
        {
            return null;
        }

        return recordStructureBase;
    }

    protected string? JsonString(string value) => string.IsNullOrEmpty(value) ? null : value;

    protected List<string>? JsonList(List<string> stringList)
    {
        if (stringList.Count == 0)
        {
            return null;
        }

        return stringList;
    }

    protected List<T>? JsonList<T>(List<T> recordStructureBaseList) where T : RecordStructureBase
    {
        if (recordStructureBaseList.Count == 0)
        {
            return null;
        }

        return recordStructureBaseList;
    }
}

