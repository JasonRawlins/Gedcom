using Gedcom.CLI;

namespace Gedcom.CLI;

public interface IGedcomWriter
{
    byte[] GetIndividuals(List<IndividualListItem> individualListItems);
}
