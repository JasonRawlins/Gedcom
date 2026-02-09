namespace Gedcom.GedcomWriters;

public interface IGedcomWriter
{
    public string GetIndividual(string xref);
    public string GetIndividuals(string query = "");

    public string GetFamily(string xref);
    public string GetFamilies(string query = "");

    public string GetRepository(string xref);
    public string GetRepositories(string query = "");

    public string GetSource(string xref);
    public string GetSources(string query = "");

    // HACK: Exporting as Excel does not cleanly map to IGedcomWriter. 
    // This is for temporary demonstration purposes.
    byte[] GetAsByteArray(string query = "");
}
