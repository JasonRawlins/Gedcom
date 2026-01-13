namespace GenesAndGenealogy.Server.ViewModels;

public class FamilyModel(IndividualModel husband, IndividualModel wife)
{
    public List<IndividualModel> Children { get; set; } = [];
    public List<EventModel> Events { get; set; } = [];
    public IndividualModel Husband { get; set; } = husband;
    public FamilyModel? Parents { get; set; }
    public IndividualModel Wife { get; set; } = wife;

    public override string ToString() => $"{Husband.FullName}, {Wife.FullName} with {Children.Count} children)";
}
