using Gedcom.RecordStructures;
using System.Text;

namespace Gedcom.CLI;

public class HtmlWriter(HeaderTree headerTree) : IGedcomWriter
{
    private HeaderTree HeaderTree { get; set; } = headerTree;

    public byte[] GetIndividuals(List<IndividualListItem> individualListItems)
    {
        var htmlTemplate = Encoding.UTF8.GetString(Properties.Resources.IndividualsHtmlTemplate);
        var individualLis = CreateIndividualList(individualListItems, HeaderTree.AutomatedRecordId);
        var finalHtml = htmlTemplate.Replace("{{INDIVIDUAL_LIST_ITEMS}}", string.Join(Environment.NewLine, individualLis));

        return Encoding.UTF8.GetBytes(finalHtml);
    }

    public static List<string> CreateIndividualList(List<IndividualListItem> individualListItems, string treeId)
    {
        individualListItems.Sort();
        var liList = new List<string>();
        foreach (var li in individualListItems)
        {
            liList.Add(
$@"<li class='individual-card'>
    <a href='{GenerateAncestryProfileLink(treeId, li.XrefId)}' target='_blank'>
        <h3>
            {li.Surname}, {li.Given}
        </h3>
        <div class='vitals'>
            BIRTH {li.Birth.DayMonthYear} • {li.BirthPlace}
        </div>
        <div class='vitals'>
            DEATH {li.Death.DayMonthYear} • {li.DeathPlace}
        </div>
    </a>
</li>");
        }

        return liList;
    }

    public static string GenerateAncestryProfileLink(string treeId, string xref) =>
        $"https://www.ancestry.com/family-tree/person/tree/{treeId}/person/{xref}/facts";
}