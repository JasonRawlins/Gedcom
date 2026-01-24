using Gedcom.RecordStructures;

namespace GenesAndGenealogy.Server.ViewModels;

public class ProfileModel(HeaderTreeJson treeModel, IndividualJson individualJson, List<FamilyModel> familyModels, List<RepositoryJson> repositories, List<SourceJson> sources)
{
    public List<FamilyModel> Families { get; set; } = familyModels;
    public IndividualJson Individual { get; set; } = individualJson;
    public FamilyModel? Parents { get; set; }
    public List<RepositoryJson> Repositories { get; set; } = repositories;
    public List<SourceJson> Sources { get; set; } = sources;
    public HeaderTreeJson Tree { get; set; } = treeModel;

    public override string ToString() => $"{Individual.Given} {Individual.Surname}";
}
