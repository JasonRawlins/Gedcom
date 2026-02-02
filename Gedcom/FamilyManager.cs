using Gedcom.Entities;
using Gedcom.RecordStructures;

namespace Gedcom;

public class FamilyManager(Core.GedcomDocument gedcom)
{
    private readonly Core.GedcomDocument Gedcom = gedcom;
    private readonly Dictionary<string, Individual> IndividualsCache = [];
    private readonly Dictionary<string, Family> FamilyCache = [];

    public Family CreateFamily(string familyXref, int generationsOfAncestors, int generationsOfDescendants)
    {
        var family = GetOrCreateFamily(familyXref);

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

    public Family CreateNullFamily()
    {
        return new Family(RecordStructureBase.Empty<FamilyRecord>());
    }

    private Individual GetOrCreateIndividual(string individualXref)
    {
        var individualRecord = Gedcom.GetIndividualRecord(individualXref);

        if (IndividualsCache.TryGetValue(individualXref, out var exisitingIndividual))
            return exisitingIndividual;

        var newIndividual = new Individual(individualRecord);

        foreach (var multimediaLink in individualRecord.MultimediaLinks)
        {
            var objectRecord = Gedcom.GetObjectRecord(multimediaLink.Xref);
            newIndividual.MultimediaRecords.Add(objectRecord);
        }
        
        IndividualsCache[individualXref] = newIndividual;

        return newIndividual;
    }

    private Family GetOrCreateFamily(string familyXref)
    {
        if (FamilyCache.TryGetValue(familyXref, out var exisitingFamily))
            return exisitingFamily;

        var newFamilyRecord = Gedcom.GetFamilyRecord(familyXref);
        var newFamily = new Family(newFamilyRecord);

        var husbandIndividualRecord = Gedcom.GetIndividualRecord(newFamilyRecord.Husband);
        if (!husbandIndividualRecord.IsEmpty)
        {
            newFamily.Husband = GetOrCreateIndividual(husbandIndividualRecord.Xref);
        }

        var wifeIndividualRecord = Gedcom.GetIndividualRecord(newFamilyRecord.Wife);
        if (!wifeIndividualRecord.IsEmpty)
        {
            newFamily.Wife = GetOrCreateIndividual(wifeIndividualRecord.Xref);
        }

        FamilyCache[familyXref] = newFamily;

        return newFamily;
    }

    private void LoadAncestors(Individual individual, int generationsOfAncestors)
    {
        if (generationsOfAncestors == 0)
            return;

        var parentsFamilyRecord = Gedcom.GetFamilyRecordOfParents(individual.Xref);

        if (parentsFamilyRecord.IsEmpty)
            return;

        var parentsFamily = GetOrCreateFamily(parentsFamilyRecord.Xref);

        if (parentsFamily.Husband != null)
        {
            LoadAncestors(parentsFamily.Husband, generationsOfAncestors - 1);
        }

        if (parentsFamily.Wife != null)
        {
            LoadAncestors(parentsFamily.Wife, generationsOfAncestors - 1);
        }
    }

    public void LoadDescendants(Individual individual, int generationsOfDescendants)
    {
        if (generationsOfDescendants == 0)
            return;

        var familyRecord = Gedcom.GetFamilyRecordWhereTheIndividualIsAParent(individual.Xref);
        var family = GetOrCreateFamily(familyRecord.Xref);

        LoadDescendants(family, 1);
    }

    public void LoadDescendants(Family family, int generationsOfDescendants)
    {
        if (generationsOfDescendants == 0)
            return;

        var familyChildrenXrefs = Gedcom.GetFamilyRecord(family.Xref).Children;

        foreach (var childXref in familyChildrenXrefs)
        {
            var child = GetOrCreateIndividual(childXref);

            if (child == null)
                continue;

            family.Husband?.AddChild(child);
            family.Wife?.AddChild(child);
            family.AddChild(child);
            child.Parents = family;

            var childAsParentFamilyRecord = Gedcom.GetFamilyRecordWhereTheIndividualIsAParent(child.Xref);
            if (!childAsParentFamilyRecord.IsEmpty)
            {
                var childAsParentFamily = GetOrCreateFamily(childAsParentFamilyRecord.Xref);
                LoadDescendants(childAsParentFamily, generationsOfDescendants - 1);
            }
        }
    }

    public void LoadSiblings(Individual individual)
    {
        var parentsFamilyRecord = Gedcom.GetFamilyRecordOfParents(individual.Xref);

        if (parentsFamilyRecord.IsEmpty)
            return;

        var parentsFamily = GetOrCreateFamily(parentsFamilyRecord.Xref);
        LoadDescendants(parentsFamily, 1);

        foreach (var sibling in parentsFamily.Children.Where(c => c.Xref != individual.Xref))
        {
            individual.Siblings.Add(sibling);
        }
    }
}