using Gedcom.RecordStructures;

namespace GenesAndGenealogy.Server.ViewModels;

public class TreeModel
{
    public TreeModel(HeaderTree headerTree)
    {
        AutomatedRecordId = headerTree.AutomatedRecordId;
        Name = headerTree.Name;
    }

    public string AutomatedRecordId { get; set; }
    public string Name { get; set; }

    public override string ToString() => $"{Name} ({AutomatedRecordId})";
}
