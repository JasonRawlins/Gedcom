using Gedcom.RecordStructures;

namespace GenesAndGenealogy.Server.ViewModels;

public class FamilyModel(IndividualJson? husband, IndividualJson? wife)
{
    public List<IndividualJson> Children { get; set; } = [];
    public List<IEventDetail> Events { get; set; } = [];
    public IndividualJson? Husband { get; set; } = husband;
    public FamilyModel? Parents { get; set; }
    public IndividualJson? Wife { get; set; } = wife;

    public override string ToString() => $"{Husband?.FullName ?? "Unknown father"}, {Wife?.FullName ?? "Unknown mother"} with {Children.Count} children)";
}
