using CommandLine;
using System.Text;

namespace Gedcom.CLI;

public static class Html
{
   public static List<string> CreateIndividualLis(List<IndividualListItem> individualListItems, string TreeId)
    {
        var liList = new List<string>();
        foreach (var li in individualListItems)
        {
            liList.Add(
$@"<li>
      <a href='{GenerateProfileLink(TreeId, li.Xref)}' target='_blank'>
          {li.Surname}, {li.Given} ({li.Birth.DayMonthYear} — {li.Death.DayMonthYear}). {li.Xref}
      </a>
</li>");
        }

        return liList;
    }

    public static string GenerateProfileLink(string treeId, string xref) =>
        "https://www.ancestry.com/family-tree/person/tree/{treeId}/person/{xref}/facts";
}