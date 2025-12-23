using Gedcom.Entities;

namespace Gedcom;

public class FamilyManager(Gedcom gedcom)
{
    private readonly Gedcom Gedcom = gedcom;
    private readonly Dictionary<string, Individual> IndividualsCache = [];

    private Individual? GetOrCreateIndividual(string individualXref)
    {
        var individualRecord = Gedcom.GetIndividualRecord(individualXref);
        if (individualRecord.IsEmpty) 
            return null;

        if (IndividualsCache.TryGetValue(individualXref, out var exisitingIndividual))
            return exisitingIndividual;

        var newIndividual = new Individual(individualRecord);
        IndividualsCache[individualXref] = newIndividual;

        return newIndividual;
    }

    public Family CreateFamily(string familyXref, int generationsOfAncestors, int generationsOfDescendants)
    {
        var family = CreateFamilyWithHusbandAndWife(familyXref);

        if (family.Husband != null)
        { 
            LoadAncestors(family.Husband, generationsOfAncestors);
        }

        if (family.Wife != null)
        {
            LoadAncestors(family.Wife, generationsOfAncestors);
        }

        LoadDescendants(family, generationsOfDescendants);

        return family;
    }

    private Family CreateFamilyWithHusbandAndWife(string familyXref)
    {
        var familyRecord = Gedcom.GetFamilyRecord(familyXref);
        var family = new Family(familyRecord);

        var husbandIndividualRecord = Gedcom.GetIndividualRecord(familyRecord.Husband);
        if (!husbandIndividualRecord.IsEmpty)
        {
            family.Husband = GetOrCreateIndividual(husbandIndividualRecord.Xref);
        }

        var wifeIndividualRecord = Gedcom.GetIndividualRecord(familyRecord.Wife);
        if (!wifeIndividualRecord.IsEmpty)
        {
            family.Wife = GetOrCreateIndividual(wifeIndividualRecord.Xref);
        }

        return family;
    }

    private void LoadAncestors(Individual individual, int generationsOfAncestors)
    {
        if (generationsOfAncestors == 0)
            return;

        var parentsFamilyRecord = Gedcom.GetFamilyRecordOfParents(individual.Xref);

        if (!parentsFamilyRecord.IsEmpty)
        {
            var parentsFamily = CreateFamilyWithHusbandAndWife(parentsFamilyRecord.Xref);
            individual.Parents = parentsFamily;

            if (parentsFamily.Husband != null)
            {
                LoadAncestors(parentsFamily.Husband, generationsOfAncestors - 1);
            }

            if (parentsFamily.Wife != null)
            {
                LoadAncestors(parentsFamily.Wife, generationsOfAncestors - 1);
            }

        }
    }

    private void LoadDescendants(Family family, int generationsOfDescendants)
    {
        if (generationsOfDescendants == 0)
            return;

        var familyChildrenXrefs = Gedcom.GetFamilyRecord(family.Xref).Children;

        foreach (var childXref in familyChildrenXrefs)
        {
            var child = GetOrCreateIndividual(childXref);

            if (child == null)
                continue;

            family.Husband?.Children.Add(child);
            family.Wife?.Children.Add(child);
            family.Children.Add(child);
            child.Parents = family;

            var childAsParentFamilyRecord = Gedcom.GetFamilyRecordWhereTheIndividualIsAParent(child.Xref);
            if (!childAsParentFamilyRecord.IsEmpty)
            {
                var childAsParentFamily = CreateFamilyWithHusbandAndWife(childAsParentFamilyRecord.Xref);
                LoadDescendants(childAsParentFamily, generationsOfDescendants - 1);
            }
        }
    }
}