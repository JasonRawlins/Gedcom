using Gedcom.RecordStructures;
using System.Text;

namespace Gedcom.GedcomWriters;

public class HtmlGedcomWriter(GedcomDocument gedcom) : IGedcomWriter
{
    public GedcomDocument GedcomDocument { get; set; } = gedcom;

    public byte[] GetIndividual(string xref)
    {
        var individualRecord = GedcomDocument.GetIndividualRecord(xref);

        if (individualRecord.IsEmpty) return [];

        var individualRecords = new List<IndividualRecord> { individualRecord };

        var htmlTemplate = Encoding.UTF8.GetString(Properties.Resources.GedcomNetIndividualsHtmlTemplate);
        var finalHtml = htmlTemplate.Replace("{{INDIVIDUAL_LIST_ITEMS}}", GetIndividualsText(individualRecords));

        return Encoding.UTF8.GetBytes(finalHtml);
    }

    public byte[] GetIndividuals(string query = "")
    {
        var individualRecords = GedcomDocument.GetIndividualRecords();
        if (individualRecords.Count == 0) return [];

        var htmlTemplate = Encoding.UTF8.GetString(Properties.Resources.GedcomNetIndividualsHtmlTemplate);
        var finalHtml = htmlTemplate.Replace("{{INDIVIDUAL_LIST_ITEMS}}", GetIndividualsText(individualRecords));

        return Encoding.UTF8.GetBytes(finalHtml);
    }

    private string GetIndividualsText(List<IndividualRecord> individualRecords)
    {
        if (individualRecords.Count == 0)
        {
            return "";
        }

        var ulStringBuilder = new StringBuilder();
        ulStringBuilder.AppendLine("<ul>");

        foreach (var individualRecord in individualRecords)
        {
            if (individualRecord.IsEmpty) continue;

            var individualListItem = CreateIndividualListItem(individualRecord);
            ulStringBuilder.AppendLine(individualListItem);
        }

        ulStringBuilder.AppendLine("</ul>");

        return ulStringBuilder.ToString();
    }

    public byte[] GetFamily(string xref)
    {
        var familyRecord = GedcomDocument.GetFamilyRecord(xref);

        if (familyRecord.IsEmpty) return [];

        return Encoding.UTF8.GetBytes(CreateFamilyListItem(familyRecord));
    }

    public byte[] GetFamilies(string query = "")
    {
        var familyRecords = GedcomDocument.GetFamilyRecords();

        if (familyRecords.Count == 0) return [];

        var ul = new StringBuilder();
        ul.AppendLine("<ul>");

        foreach (var familyRecord in familyRecords)
        {
            var familyListItem = CreateFamilyListItem(familyRecord);
            ul.AppendLine(familyListItem);
        }

        ul.AppendLine("</ul>");

        return Encoding.UTF8.GetBytes(ul.ToString());
    }

    public string GetRepository(string xref)
    {
        var repositoryRecord = GedcomDocument.GetRepositoryRecord(xref);

        if (repositoryRecord.IsEmpty) return "";

        return CreateRepositoryListItem(repositoryRecord);
    }

    public string GetRepositories(string query = "")
    {
        var repositoryRecords = GedcomDocument.GetRepositoryRecords();

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
        var sourceRecord = GedcomDocument.GetSourceRecord(xref);

        if (sourceRecord.IsEmpty) return "";

        return CreateSourceListItem(sourceRecord);
    }

    public string GetSources(string query = "")
    {
        var sourceRecords = GedcomDocument.GetSourceRecords();

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
        var ancestryLink = GenerateAncestryProfileLink(GedcomDocument.Header.Source.Tree.AutomatedRecordId, individualListItem.XrefId);

        return $@"<li class='individual-card'>
                    <a href='{ancestryLink}' target='_blank'>
                        <h3>
                            {individualListItem.Surname}, {individualListItem.Given}
                        </h3>
                        <div class='vitals'>
                            BIRTH {individualListItem.Birthdate} • {individualListItem.BirthPlace}
                        </div>
                        <div class='vitals'>
                            DEATH {individualListItem.DeathDate} • {individualListItem.DeathPlace}
                        </div>
                    </a>
                </li>";
    }

    private static string CreateFamilyListItem(FamilyRecord familyRecord)
    {
        return $"<li>{familyRecord.Xref}</li>";
    }

    private static string CreateRepositoryListItem(RepositoryRecord repositoryRecord)
    {
        return $"<li>({repositoryRecord.Xref}) {repositoryRecord.Name}</li>";
    }

    private static string CreateSourceListItem(SourceRecord sourceRecord)
    {
        return $"<li>({sourceRecord.Xref}) {sourceRecord.TextFromSource}</li>";
    }

    public static string GenerateAncestryProfileLink(string treeId, string xref) => $"https://www.ancestry.com/family-tree/person/tree/{treeId}/person/{xref}/facts";
}

