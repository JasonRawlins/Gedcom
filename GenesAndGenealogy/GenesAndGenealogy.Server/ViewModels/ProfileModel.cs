using Gedcom.RecordStructures;

namespace GenesAndGenealogy.Server.ViewModels;

public class ProfileModel
{
    public ProfileModel(TreeModel treeModel, IndividualJson individualJson, List<FamilyModel> familyModels, List<RepositoryModel> repositories, List<SourceJson> sources)
    {
        Families = familyModels;
        Individual = individualJson;
        Repositories = repositories;
        Tree = treeModel;
        Sources = sources;
    }

    public List<FamilyModel> Families { get; set; }
    public IndividualJson Individual { get; set; }
    public FamilyModel? Parents { get; set; }
    public List<RepositoryModel> Repositories { get; set; }
    public List<SourceJson> Sources { get; set; }
    public TreeModel Tree { get; set; }

    public override string ToString() => $"{Individual.Given} {Individual.Surname}";
}
