using Gedcom.RecordStructures;
using System.Text;

namespace Gedcom.CLI;

public class HtmlGedcomWriter(Gedcom gedcom) : IGedcomWriter
{
    private Gedcom Gedcom { get; set; } = gedcom;

    public string GetIndividual(string xref)
    {
        var individualRecord = Gedcom.GetIndividualRecord(xref);

        if (individualRecord.IsEmpty) return "";

        return CreateIndividualListItem(individualRecord);
    }

    public string GetIndividuals(string query = "")
    {
        var individualRecords = Gedcom.GetIndividualRecords(query);

        if (individualRecords.Count == 0) return "";

        var ul = new StringBuilder();
        ul.AppendLine("<ul>");
       
        foreach (var individualRecord in individualRecords)
        {
            var individualListItem = CreateIndividualListItem(individualRecord);
            ul.AppendLine(individualListItem);
        }

        ul.AppendLine("</ul>");

        return ul.ToString();
    }

    public string GetFamily(string xref)
    {
        var familyRecord = Gedcom.GetFamilyRecord(xref);

        if (familyRecord.IsEmpty) return "";

        return CreateFamilyListItem(familyRecord);
    }

    public string GetFamilies(string query = "")
    {
        var familyRecords = Gedcom.GetFamilyRecords(query);

        if (familyRecords.Count == 0) return "";

        var ul = new StringBuilder();
        ul.AppendLine("<ul>");

        foreach (var familyRecord in familyRecords)
        {
            var familyListItem = CreateFamilyListItem(familyRecord);
            ul.AppendLine(familyListItem);
        }

        ul.AppendLine("</ul>");

        return ul.ToString();
    }

    public string GetRepository(string xref)
    {
        var repositoryRecord = Gedcom.GetRepositoryRecord(xref);

        if (repositoryRecord.IsEmpty) return "";

        return CreateRepositoryListItem(repositoryRecord);
    }

    public string GetRepositories(string query = "")
    {
        var repositoryRecords = Gedcom.GetRepositoryRecords(query);

        if (repositoryRecords.Count == 0) return "";

        var ul = new StringBuilder();
        ul.AppendLine("<ul>");

        foreach (var repositoryRecord in repositoryRecords)
        {
            var repositoryListItem = CreateRepositoryListItem(repositoryRecord);
            ul.AppendLine(repositoryListItem);
        }

        ul.AppendLine("</ul>");

        return ul.ToString();
    }

    public string GetSource(string xref)
    {
        var sourceRecord = Gedcom.GetSourceRecord(xref);

        if (sourceRecord.IsEmpty) return "";

        return CreateSourceListItem(sourceRecord);
    }

    public string GetSources(string query = "")
    {
        var sourceRecords = Gedcom.GetSourceRecords(query);

        if (sourceRecords.Count == 0) return "";

        var ul = new StringBuilder();
        ul.AppendLine("<ul>");

        foreach (var sourceRecord in sourceRecords)
        {
            var sourceListItem = CreateSourceListItem(sourceRecord);
            ul.AppendLine(sourceListItem);
        }

        ul.AppendLine("</ul>");

        return ul.ToString();
    }

    private string CreateIndividualListItem(IndividualRecord individualRecord)
    {
        var individualListItem = new IndividualListItem(individualRecord);
        var ancestryLink = GenerateAncestryProfileLink(Gedcom.Header.Source.Tree.AutomatedRecordId, individualListItem.XrefId);

        return $@"<li class='individual-card'>
                    <a href='{ancestryLink}' target='_blank'>
                        <h3>
                            {individualListItem.Surname}, {individualListItem.Given}
                        </h3>
                        <div class='vitals'>
                            BIRTH {individualListItem.Birth.DayMonthYear} • {individualListItem.BirthPlace}
                        </div>
                        <div class='vitals'>
                            DEATH {individualListItem.Death.DayMonthYear} • {individualListItem.DeathPlace}
                        </div>
                    </a>
                </li>";
    }

    private string CreateFamilyListItem(FamilyRecord familyRecord)
    {
        return $"<li>{familyRecord.Xref}</li>";
    }

    private string CreateRepositoryListItem(RepositoryRecord repositoryRecord)
    {
        return $"<li>({repositoryRecord.Xref}) {repositoryRecord.Name}</li>";
    }

    private string CreateSourceListItem(SourceRecord sourceRecord)
    {
        return $"<li>({sourceRecord.Xref}) {sourceRecord.TextFromSource}</li>";
    }

    public static string GenerateAncestryProfileLink(string treeId, string xref) => $"https://www.ancestry.com/family-tree/person/tree/{treeId}/person/{xref}/facts";

    public byte[] GetAsByteArray(string query = "")
    {
        var htmlTemplate = Encoding.UTF8.GetString(Properties.Resources.IndividualsHtmlTemplate);
        var finalHtml = htmlTemplate.Replace("{{INDIVIDUAL_LIST_ITEMS}}", GetIndividuals());

        return Encoding.UTF8.GetBytes(finalHtml);
    }
}

