using Gedcom.RecordStructures;

namespace Gedcom.Entities;

public class Family(FamilyRecord familyRecord)
{
    internal FamilyRecord FamilyRecord { get; } = familyRecord;

    public List<Individual> Children { get; set; } = [];
    public Individual? Husband { get; set; }
    public Individual? Wife { get; set; }
    public string Xref => FamilyRecord.Xref;

    public override string ToString()
    {
        var husbandName = "Unknown father";
        var wifeName = "Unknown mother";

        if (Husband != null)
        {
            husbandName = $"{Husband.Given} {Husband.Surname}";
        }

        if (Wife != null)
        {
            wifeName = $"{Wife.Given} {Wife.Surname}";
        }

        return $"{husbandName} and {wifeName} ({Xref}) with {string.Join(", ", Children)}";
    }
}
