using Gedcom;
using Gedcom.RecordStructures;

namespace GenesAndGenealogy.Server.ViewModels;

public class PlaceModel
{
    public PlaceModel(PlaceStructure placeStructure)
    {
        Name = placeStructure.PlaceName;
        Hierarchy = placeStructure.PlaceHierarchy;
        Map = new MapModel(placeStructure.Map);
        Notes = placeStructure.NoteStructures.Select(ns => ns.Text).ToList();
    }

    public string Name { get; set; }
    public string Hierarchy { get; set; }
    public MapModel Map { get; set; }
    public List<string> Notes { get; set; }

    public override string ToString() => Name;
}