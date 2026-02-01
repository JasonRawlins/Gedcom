using Gedcom.RecordStructures;

namespace GenesAndGenealogy.Server.ViewModels;

public class ProfileModel(
    HeaderTreeJson treeModel, 
    IndividualJson individualJson, 
    List<FamilyModel> familyModels, 
    List<RepositoryJson> repositories, 
    List<SourceJson> sources, 
    List<MultimediaJson> multimediaItems)
{
    public List<FamilyModel> Families { get; set; } = familyModels;
    public IndividualJson Individual { get; set; } = individualJson;
    public List<MultimediaJson> MultimediaItems { get; set; } = multimediaItems;
    public FamilyModel? Parents { get; set; }
    public MultimediaJson? PortraitMultiMedia { get; set; }
    public string PortraitUrl
    {
        get
        {
            if (PortraitMultiMedia != null)
            {
                var sourceType = PortraitMultiMedia!.File!.Form!.SourceType ?? "";
                if (sourceType.Equals("jpeg"))
                    sourceType = "jpg";
                return $"{PortraitMultiMedia.ObjectId}.{sourceType}";
            }

            return Individual.Sex! switch
            {
                "M" => "silhouette-male.jpg",
                "F" => "silhouette-female.jpg",
                _ => "silhouette-unknown.jpg"
            };
        }
    }
    public List<RepositoryJson> Repositories { get; set; } = repositories;
    public List<SourceJson> Sources { get; set; } = sources;
    public HeaderTreeJson Tree { get; set; } = treeModel;

    public override string ToString() => $"{Individual.Given} {Individual.Surname}";
}
