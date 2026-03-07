namespace Gedcom.GedcomWriters;

public interface IGedcomWriter
{
    public byte[] GetIndividual(string xref);
    public byte[] GetIndividuals(string query = "");

    public byte[] GetFamily(string xref);
    public byte[] GetFamilies(string query = "");

    public string GetRepository(string xref);
    public string GetRepositories(string query = "");

    public string GetSource(string xref);
    public string GetSources(string query = "");
}
