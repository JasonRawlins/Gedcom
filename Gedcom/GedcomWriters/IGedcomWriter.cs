namespace Gedcom.GedcomWriters;

public interface IGedcomWriter
{
    public GedcomDocument GedcomDocument { get; set; }

    public byte[] GetIndividuals(string xref = "");

    public byte[] GetFamilies(string xref = "");

    public byte[] GetRepositories(string xref = "");

    public byte[] GetSources(string xref = "");
}
