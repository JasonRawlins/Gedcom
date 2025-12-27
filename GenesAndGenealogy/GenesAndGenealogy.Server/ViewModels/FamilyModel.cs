namespace GenesAndGenealogy.Server.ViewModels;

public class FamilyModel
{
    public FamilyModel(IndividualModel partner1, IndividualModel partner2)
    {
        Children = new List<IndividualModel>();
        Partner1 = partner1;
        Partner2 = partner2;
    }

    public FamilyModel(IndividualModel partner1, IndividualModel partner2, List<IndividualModel> children) : this(partner1, partner2)
    {
        Children = children;
    }

    public List<IndividualModel> Children { get; set; }
    public IndividualModel Partner1 { get; set; }
    public IndividualModel Partner2 { get; set; }

    public override string ToString() => $"{Partner1.FullName}, {Partner2.FullName} with {Children.Count} children)";
}
