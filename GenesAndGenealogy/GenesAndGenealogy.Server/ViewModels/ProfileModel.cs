namespace GenesAndGenealogy.Server.ViewModels;

public class ProfileModel
{
    public ProfileModel(TreeModel treeModel, IndividualModel individualModule, List<FamilyModel> familyModels, List<EventModel> eventModels, List<RepositoryModel> repositories, List<SourceModel> sources)
    {
        Events = eventModels;
        Families = familyModels;
        Individual = individualModule;
        Repositories = repositories;
        Tree = treeModel;
        Sources = sources;
    }

    public List<EventModel> Events { get; set; }
    public List<FamilyModel> Families { get; set; }
    public IndividualModel Individual { get; set; }
    public List<RepositoryModel> Repositories { get; set; }
    public List<SourceModel> Sources { get; set; }
    public TreeModel Tree { get; set; }

    public override string ToString() => $"{Individual.Given} {Individual.Surname}";
}
