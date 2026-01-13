namespace GenesAndGenealogy.Server.ViewModels;

public class ProfileModel
{
    public ProfileModel(TreeModel treeModel, IndividualModel individualModule, FamilyModel parents, List<FamilyModel> familyModels, List<RepositoryModel> repositories, List<SourceModel> sources)
    {
        Families = familyModels;
        Individual = individualModule;
        Parents = parents;
        Repositories = repositories;
        Tree = treeModel;
        Sources = sources;
    }

    public List<FamilyModel> Families { get; set; }
    public IndividualModel Individual { get; set; }
    public FamilyModel Parents { get; set; }
    public List<RepositoryModel> Repositories { get; set; }
    public List<SourceModel> Sources { get; set; }
    public TreeModel Tree { get; set; }

    public override string ToString() => $"{Individual.Given} {Individual.Surname}";
}
