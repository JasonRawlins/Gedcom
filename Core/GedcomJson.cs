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

}

