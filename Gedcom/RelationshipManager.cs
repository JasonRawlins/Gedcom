using Gedcom.Entities;
using Gedcom.RecordStructures;
using Microsoft.Extensions.FileSystemGlobbing;

namespace Gedcom;

public class ReltionshipManager(Gedcom gedcom)
{
    private readonly Gedcom Gedcom = gedcom;

    public Family GetFamily(string familyXref)
    {
        return GetFamily(familyXref, 0, 0);
    }

    public Family GetFamily(string familyXref, int generationsOfAncestors, int generationsOfDescendants)
    {
        var familyRecord = Gedcom.GetFamilyRecord(familyXref);
        var family = new Family(familyRecord);

        var husbandRecord = Gedcom.GetIndividualRecord(familyRecord.Husband);
        if (!husbandRecord.IsEmpty)
        {
            family.Husband = new Individual(husbandRecord);
            LoadAncestors(family.Husband, generationsOfAncestors);
        }

        var wifeRecord = Gedcom.GetIndividualRecord(familyRecord.Wife);
        if (!wifeRecord.IsEmpty)
        {
            family.Wife = new Individual(wifeRecord);
            LoadAncestors(family.Wife, generationsOfAncestors);
        }

        LoadDescendants(family, generationsOfDescendants);

        return family;
    }

    private void LoadFamily(Family family, Individual individual)
    {
        var familyRecord = family.FamilyRecord;

        individual.Parents = new Family(familyRecord);

        var husbandRecord = family.Husband!.IndividualRecord;
        if (!husbandRecord.IsEmpty)
        {
            var husband = new Individual(husbandRecord);
            individual.Parents.Husband = husband;
        }

        var wifeRecord = family.Wife!.IndividualRecord;
        if (!wifeRecord.IsEmpty)
        {
            var wife = new Individual(wifeRecord);
            individual.Parents.Wife = wife;
        }
    }

    private void LoadAncestors(Individual individual, int generationsOfAncestors)
    {
        if (generationsOfAncestors == 0)
            return;

        var familyRecord = Gedcom.GetFamilyRecordOfParents(individual.Xref);
        var family = new Family(familyRecord);

        LoadFamily(family, individual);

        if (individual.Parents!.Husband != null)
            LoadAncestors(individual.Parents.Husband, generationsOfAncestors - 1);

        if (individual.Parents!.Wife != null)
            LoadAncestors(individual.Parents.Wife, generationsOfAncestors - 1);
    }

    private void LoadDescendants(Family family, int generationsOfDescendants)
    {
        if (generationsOfDescendants == 0)
            return;

        foreach (var childXref in family.FamilyRecord.Children)
        {
            var childRecord = Gedcom.GetIndividualRecord(childXref);

            if (childRecord.IsEmpty)
                continue;

            var child = new Individual(childRecord);

            if (family.Husband != null)
                family.Husband.Children.Add(child);

            if (family.Wife != null)
                family.Wife.Children.Add(child);

            var childFamilyRecord = Gedcom.GetFamilyRecordWhereTheIndividualIsAParent(child.Xref);

            if (!childFamilyRecord.IsEmpty)
            {
                //LoadFamily(childFamily, child);
                //LoadDescendants(childFamily, generationsOfDescendants - 1);
            }
        }
    }
}