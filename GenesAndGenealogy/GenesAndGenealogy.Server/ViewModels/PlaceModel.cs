using Gedcom;
using Gedcom.RecordStructures;

namespace GenesAndGenealogy.Server.ViewModels;

public class PlaceModel(PlaceStructure placeStructure)
{
    public string Name { get; set; } = placeStructure.PlaceName;
    public string Hierarchy { get; set; } = placeStructure.PlaceHierarchy;
    public MapModel Map { get; set; } = new MapModel(placeStructure.Map);
    public List<string> Notes { get; set; } = placeStructure.NoteStructures.Select(ns => ns.Text).ToList();

    public override string ToString() => Name;
}