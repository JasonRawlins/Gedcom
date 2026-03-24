using Gedcom.DTOs;
using Gedcom.RecordStructures;
using System.Text;

namespace Gedcom.GedcomWriters;

public class HtmlGedcomWriter : GedcomWriter
{
    public HtmlGedcomWriter(GedcomDocument gedcomDocument) : base(gedcomDocument)
    {
        GedcomDocument = gedcomDocument;
    }

    public override byte[] GetIndividuals(string xref)
    {
        var individualDtos = GetIndividualDtos(xref);

        var htmlTemplate = Encoding.UTF8.GetString(Properties.Resources.GedcomNetIndividualsHtmlTemplate);
        var finalHtml = htmlTemplate.Replace("{{INDIVIDUAL_LIST_ITEMS}}", GetIndividualsText(individualDtos));

        return Encoding.UTF8.GetBytes(finalHtml);
    }

    public override byte[] GetFamilies(string xref = "")
    {
        var familyDtos = GetFamilyDtos(xref);

        var htmlTemplate = Encoding.UTF8.GetString(Properties.Resources.GedcomNetFamiliesHtmlTemplate);
        var finalHtml = htmlTemplate.Replace("{{FAMILY_LIST_ITEMS}}", GetFamiliesText(familyDtos));

        return Encoding.UTF8.GetBytes(finalHtml);
    }

    public override byte[] GetRepositories(string xref = "")
    {
        return [];
        // TODO: Return a html formatted repositories. 
        //var repositoryRecords = GedcomDocument.GetRepositoryRecords();

        //if (repositoryRecords.Count == 0) return "";

        //var ul = new StringBuilder();
        //ul.AppendLine("<ul>");

        //foreach (var repositoryRecord in repositoryRecords)
        //{
        //    var repositoryListItem = CreateRepositoryListItem(repositoryRecord);
        //    ul.AppendLine(repositoryListItem);
        //}

        //ul.AppendLine("</ul>");

        //return Encoding.UTF8.GetBytes(ul.ToString());
    }

    public override byte[] GetSources(string xref = "")
    {
        return [];
        // TODO: Return a html formatted sources
        //var sourceRecords = GedcomDocument.GetSourceRecords();

        //if (sourceRecords.Count == 0) return "";

        //var ul = new StringBuilder();
        //ul.AppendLine("<ul>");

        //foreach (var sourceRecord in sourceRecords)
        //{
        //    var sourceListItem = CreateSourceListItem(sourceRecord);
        //    ul.AppendLine(sourceListItem);
        //}

        //ul.AppendLine("</ul>");

        //return ul.ToString();
    }

    private string GetIndividualsText(List<IndividualDto> individualDtos)
    {
        if (individualDtos.Count == 0)
        {
            return "";
        }

        var ulStringBuilder = new StringBuilder();
        ulStringBuilder.AppendLine("<ul class='individuals'>");

        foreach (var individualDto in individualDtos)
        {
            if (individualDto.IsEmpty) continue;

            var individualListItem = CreateIndividualListItem(individualDto);
            ulStringBuilder.AppendLine(individualListItem);
        }

        ulStringBuilder.AppendLine("</ul>");

        return ulStringBuilder.ToString();
    }

    private string CreateIndividualListItem(IndividualDto individualDto)
    {
        var individualListItem = new IndividualListItem(individualDto);
        var ancestryLink = GenerateAncestryProfileLink(GedcomDocument.Header.Source.Tree.AutomatedRecordId, individualListItem.XrefId);

        return $@"<li class='individual-card'>
                    <a href='{ancestryLink}' target='_blank'>
                        <h3>
                            {individualListItem.Surname}, {individualListItem.Given}
                        </h3>
                        <div>{individualListItem.Xref}</div>
                        <div class='vitals'>
                            BIRTH {individualListItem.Birthdate} • {individualListItem.BirthPlace}
                        </div>
                        <div class='vitals'>
                            DEATH {individualListItem.DeathDate} • {individualListItem.DeathPlace}
                        </div>
                    </a>
                </li>";
    }

    private static string GetFamiliesText(List<FamilyDto> familyDtos)
    {
        if (familyDtos.Count == 0)
        {
            return "";
        }

        var ulStringBuilder = new StringBuilder();
        ulStringBuilder.AppendLine("<ul class='families'>");

        foreach (var familyDto in familyDtos)
        {
            if (familyDto.IsEmpty) continue;

            var familyListItem = CreateFamilyListItem(familyDto);
            ulStringBuilder.AppendLine(familyListItem);
        }

        ulStringBuilder.AppendLine("</ul>");

        return ulStringBuilder.ToString();
    }

    private static string CreateFamilyListItem(FamilyDto familyDto)
    {
        var familyListItem = new FamilyListItem(familyDto);
        var familyStringBuilder = new StringBuilder();

        familyStringBuilder.Append($@"<li class='family-card'>
                    <h3>
                        {familyListItem.Husband} and {familyListItem.Wife}
                    </h3>
                    <div>{familyListItem.Xref}</div>");

        if (familyDto.Children?.Count == 0)
        {
            familyStringBuilder.AppendLine("With no children");
        }

        if (familyDto.Children?.Count > 0)
        {
            familyStringBuilder.AppendLine($"with {familyDto.Children.Count} child(ren)");

            familyStringBuilder.AppendLine("<ul class='children'>");

            foreach (var child in familyDto.Children)
            {
                familyStringBuilder.AppendLine($"<li>{child}</li>");
            }

            familyStringBuilder.AppendLine("</ul>");
        }

        familyStringBuilder.AppendLine("</li>");

        return familyStringBuilder.ToString();
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

