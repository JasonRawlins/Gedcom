using Gedcom.RecordStructures;

namespace Gedcom.Entities;

public class Family(FamilyRecord familyRecord)
{
    private FamilyRecord FamilyRecord { get; } = familyRecord;

    public List<Individual> Children { get; set; } = [];
    public Individual? Husband { get; set; }
    public Individual? Wife { get; set; }
    public string Xref => FamilyRecord.Xref;

    public void AddChild(Individual child)
    {
        if (!Children.Contains(child))
        {
            Children.Add(child);
        }
    }

    public override string ToString()
    {
        var husbandName = "Unknown father";
        var wifeName = "Unknown mother";
        var childrenList = " with no children";

        if (Husband != null)
        {
            husbandName = $"{Husband.Given} {Husband.Surname}";
        }

        if (Wife != null)
        {
            wifeName = $"{Wife.Given} {Wife.Surname}";
        }

        if (Children.Count > 0)
        {
            childrenList = $" with children {string.Join(", ", Children)}";
        }

        return $"{husbandName} and {wifeName} ({Xref}){childrenList}";
    }
}
