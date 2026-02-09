using Gedcom.RecordStructures;

namespace GenesAndGenealogy.Server.ViewModels;

public class ProfileModel(
    HeaderTreeDto treeModel, 
    IndividualDto individualJson, 
    List<FamilyModel> familyModels, 
    List<RepositoryDto> repositories, 
    List<SourceDto> sources, 
    List<MultimediaDto> multimediaItems)
{
    public List<FamilyModel> Families { get; set; } = familyModels;
    public IndividualDto Individual { get; set; } = individualJson;
    public List<MultimediaDto> MultimediaItems { get; set; } = multimediaItems;
    public FamilyModel? Parents { get; set; }
    public MultimediaDto? PortraitMultiMedia { get; set; }
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
    public List<RepositoryDto> Repositories { get; set; } = repositories;
    public List<SourceDto> Sources { get; set; } = sources;
    public HeaderTreeDto Tree { get; set; } = treeModel;

    public override string ToString() => $"{Individual.Given} {Individual.Surname}";
}
