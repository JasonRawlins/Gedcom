namespace Gedcom.GedcomWriters;

public interface IGedcomWriter
{
    public byte[] GetIndividual(string xref);
    public byte[] GetIndividuals(string query = "");

    public string GetFamily(string xref);
    public string GetFamilies(string query = "");

    public string GetRepository(string xref);
    public string GetRepositories(string query = "");

    public string GetSource(string xref);
    public string GetSources(string query = "");
}
