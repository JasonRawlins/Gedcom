using Gedcom.RecordStructures;

namespace Gedcom.CLI;

public static class HtmlWriter
{
   public static List<string> CreateIndividualLis(List<IndividualListItem> individualListItems, HeaderTree tree)
    {
        individualListItems.Sort();
        var liList = new List<string>();
        foreach (var li in individualListItems)
        {
            liList.Add(
$@"<li class='individual-card'>
    <a href='{GenerateAncestryProfileLink(tree.AutomatedRecordId, li.XrefId)}' target='_blank'>
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