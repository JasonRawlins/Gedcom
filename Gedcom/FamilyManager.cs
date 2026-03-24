using Gedcom.Entities;
using Gedcom.RecordStructures;

namespace Gedcom;

public class FamilyManager(GedcomDocument gedcomDocument)
{
    private readonly GedcomDocument GedcomDocument = gedcomDocument;
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

    public static Family CreateNullFamily()
    {
        return new Family(RecordStructureBase.Empty<FamilyRecord>());
    }

    private Individual GetOrCreateIndividual(string individualXref)
    {
        var individualRecord = GedcomDocument.GetIndividualRecords().Single(r => r.Xref == individualXref);

        if (IndividualsCache.TryGetValue(individualXref, out var existingIndividual))
            return existingIndividual;

        var newIndividual = new Individual(individualRecord);

        foreach (var multimediaLink in individualRecord.MultimediaLinks)
        {
            var objectRecord = GedcomDocument.GetObjectRecord(multimediaLink.Xref);
            newIndividual.MultimediaRecords.Add(objectRecord);
        }
        
        IndividualsCache[individualXref] = newIndividual;

        return newIndividual;
    }

    private Family GetOrCreateFamily(string familyXref)
    {
        if (FamilyCache.TryGetValue(familyXref, out var exisitingFamily))
            return exisitingFamily;

        var newFamilyRecord = GedcomDocument.GetFamilyRecord(familyXref);
        var newFamily = new Family(newFamilyRecord);

        var husbandIndividualRecord = GedcomDocument.GetIndividualRecords().Single(r => r.Xref == newFamilyRecord.Husband);
        if (!husbandIndividualRecord.IsEmpty)
        {
            newFamily.Husband = GetOrCreateIndividual(husbandIndividualRecord.Xref);
        }

        var wifeIndividualRecord = GedcomDocument.GetIndividualRecords().Single(r => r.Xref == newFamilyRecord.Wife);
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

        var parentsFamilyRecord = GedcomDocument.GetFamilyRecordOfParents(individual.Xref);

        if (parentsFamilyRecord.IsEmpty)
            return;

        var parentsFamily = GetOrCreateFamily(parentsFamilyRecord.Xref);

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

    public void LoadDescendants(Individual individual, int generationsOfDescendants)
    {
        if (generationsOfDescendants == 0)
            return;

        var familyRecord = GedcomDocument.GetFamilyRecordWhereTheIndividualIsAParent(individual.Xref);
        var family = GetOrCreateFamily(familyRecord.Xref);

        LoadDescendants(family, 1);
    }

    public void LoadDescendants(Family family, int generationsOfDescendants)
    {
        if (generationsOfDescendants == 0)
            return;

        var familyChildrenXrefs = GedcomDocument.GetFamilyRecord(family.Xref).Children;

        foreach (var childXref in familyChildrenXrefs)
        {
            var child = GetOrCreateIndividual(childXref);

            if (child == null)
                continue;

            family.Husband?.AddChild(child);
            family.Wife?.AddChild(child);
            family.AddChild(child);
            child.Parents = family;

            var childAsParentFamilyRecord = GedcomDocument.GetFamilyRecordWhereTheIndividualIsAParent(child.Xref);
            if (!childAsParentFamilyRecord.IsEmpty)
            {
                var childAsParentFamily = GetOrCreateFamily(childAsParentFamilyRecord.Xref);
                LoadDescendants(childAsParentFamily, generationsOfDescendants - 1);
            }
        }
    }

    public void LoadSiblings(Individual individual)
    {
        var parentsFamilyRecord = GedcomDocument.GetFamilyRecordOfParents(individual.Xref);

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