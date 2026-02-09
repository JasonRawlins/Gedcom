using Gedcom.RecordStructures;

namespace GenesAndGenealogy.Server.ViewModels;

public class FamilyModel(IndividualDto? husband, IndividualDto? wife)
{
    public List<IndividualDto> Children { get; set; } = [];
    public List<EventDto> Events { get; set; } = [];
    public IndividualDto? Husband { get; set; } = husband;
    public FamilyModel? Parents { get; set; }
    public IndividualDto? Wife { get; set; } = wife;

    public override string ToString() => $"{Husband?.FullName ?? "Unknown father"}, {Wife?.FullName ?? "Unknown mother"} with {Children.Count} children)";
}
