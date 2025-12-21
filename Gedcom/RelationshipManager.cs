using Gedcom.Entities;

namespace Gedcom;

public class ReltionshipManager(Gedcom gedcom)
{
    private readonly Gedcom Gedcom = gedcom;

    public Family GetFamily(string familyXref)
    {
        return GetFamily(familyXref, 1, 1);
    }

    public Family GetFamily(string familyXref, int generationOfAncestors, int generationOfDescendants)
    {
        var familyRecord = Gedcom.GetFamilyRecord(familyXref);
        var husbandRecord = Gedcom.GetIndividualRecord(familyRecord.Husband);
        var wifeRecord = Gedcom.GetIndividualRecord(familyRecord.Wife);

        var family = new Family(familyRecord);

        if (!husbandRecord.IsEmpty)
        {
            family.Husband = new Individual(husbandRecord);         
        }

        if (!wifeRecord.IsEmpty)
        {
            family.Wife = new Individual(wifeRecord);

            var wifeParentsFamilyRecord = Gedcom.GetChildParentsFamilyRecord(family.Wife.Xref);

            var fatherRecord = Gedcom.GetIndividualRecord(wifeParentsFamilyRecord.Husband);
            var motherRecord = Gedcom.GetIndividualRecord(wifeParentsFamilyRecord.Wife);

            family.Wife.Parents = new Family(wifeParentsFamilyRecord)
            {
                Husband = new Individual(fatherRecord),
                Wife = new Individual(motherRecord)
            };
        }

        foreach (var childXref in familyRecord.Children)
        {
            var childRecord = Gedcom.GetIndividualRecord(childXref);

            if (!childRecord.IsEmpty)
            {
                family.Children.Add(new Individual(childRecord));
            }
        }

        return family;
    }
}