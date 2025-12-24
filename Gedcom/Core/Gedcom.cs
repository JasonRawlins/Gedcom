using Newtonsoft.Json;
using Gedcom.RecordStructures;

namespace Gedcom;

// The Gedcom Standard 5.5.1 documentation is at the end of this file.
[JsonConverter(typeof(GedcomJsonConverter))]
public class Gedcom : RecordStructureBase
{
    public Gedcom(List<GedcomLine> gedcomLines)
    {
        // Level 0 records are the top-level records:
        // FAM (Family)
        // HEAD (Head)
        // INDI (Individual)
        // NOTE (Note)
        // OBJE (Multimedia)
        // REPO (Repository)
        // SOUR (Source)
        // SUBM (Submitter)
        // TRLR (Trailer)
        foreach (var level0Record in GetGedcomLinesForLevel(0, gedcomLines))
        {
            Record.Records.Add(new Record(level0Record));
        }
    }

    public Header Header => First<Header>(Tag.Header);

    // Family (FAM)
    public FamilyRecord GetFamilyRecord(string xref) => GetRecord<FamilyRecord>(xref);

    // Given a child, find his or her parents.
    public FamilyRecord GetFamilyRecordOfParents(string childXref)
    {
        var parentsFamily = GetFamilyRecords().FirstOrDefault(fr => fr.Children.Contains(childXref));

        if (parentsFamily == null)
            return Empty<FamilyRecord>();

        return parentsFamily;
    }

    public FamilyRecord GetFamilyRecordByHusbandAndWife(string husbandXref, string wifeXref) => GetFamilyRecords().SingleOrDefault(f => f.Husband == husbandXref && f.Wife == wifeXref) ?? Empty<FamilyRecord>();

    public FamilyRecord GetFamilyRecordWhereTheIndividualIsAParent(string individualXref)
    {
        var family = GetFamilyRecords().FirstOrDefault(fr => fr.Husband == individualXref || fr.Wife == individualXref);

        if (family == null)
            return Empty<FamilyRecord>();

        return family;
    }

    public List<FamilyRecord> GetFamilyRecords(string query = "") => GetRecords<FamilyRecord>(Tag.Family, query);

    // Individual (INDI)
    public IndividualRecord GetIndividualRecord(string xref) => GetRecord<IndividualRecord>(xref);
    public List<IndividualRecord> GetIndividualRecords(string query = "") => GetRecords<IndividualRecord>(Tag.Individual, query);

    // Multimedia (OBJE)
    public MultimediaRecord GetMultimediaRecord(string xref) => GetRecord<MultimediaRecord>(xref);
    public List<MultimediaRecord> GetMultimediaRecords() => GetRecords<MultimediaRecord>(Tag.Object);

    // Note (NOTE)
    public NoteRecord GetNoteRecord(string xref) => GetRecord<NoteRecord>(xref);
    public List<NoteRecord> GetNoteRecords() => GetRecords<NoteRecord>(Tag.Note);

    // Repository (REPO)
    public RepositoryRecord GetRepositoryRecord(string xref) => GetRecord<RepositoryRecord>(xref);
    public List<RepositoryRecord> GetRepositoryRecords(string query = "") => GetRecords<RepositoryRecord>(Tag.Repository, query);

    // Source (SOUR)
    public SourceRecord GetSourceRecord(string xref) => GetRecord<SourceRecord>(xref);
    public List<SourceRecord> GetSourceRecords(string query = "") => GetRecords<SourceRecord>(Tag.Source, query);

    // Submitter (SUBM) TODO:
    public SubmitterRecord GetSubmitterRecord(string xref) => GetRecord<SubmitterRecord>(xref);

    private T GetRecord<T>(string xref) where T : RecordStructureBase, new() => 
        CreateRecord<T>(Single(r => r.Value.Equals(xref) && r.Level == 0 && r.Tag != Tag.Header && r.Tag != Tag.Trailer));

    private List<T> GetRecords<T>(string tag, string query = "") where T : RecordStructureBase, new()
    {
        var records = Record.Records.Where(r =>
            r.Tag.Equals(tag)
            && r.IsQueryMatch(query));

        return records.Select(CreateRecord<T>).ToList();
    }

    private T CreateRecord<T>(Record record) where T : RecordStructureBase, new()
    {
        var dynamic = new T();
        dynamic.SetRecord(record);
        return dynamic;
    }


    // The explanation of this function is at the end of the file. 
    public static List<List<GedcomLine>> GetGedcomLinesForLevel(int level, List<GedcomLine> gedcomLines)
    {
        var gedcomLinesAtThisLevel = new List<List<GedcomLine>>();
        var currentGedcomLines = new List<GedcomLine>();

        foreach (var gedcomLine in gedcomLines)
        {
            if (gedcomLine.Level == level)
            {
                gedcomLinesAtThisLevel.Add(currentGedcomLines);
                currentGedcomLines = [gedcomLine];
            }
            else
            {
                currentGedcomLines.Add(gedcomLine);
            }
        }

        gedcomLinesAtThisLevel.Add(currentGedcomLines);

        return gedcomLinesAtThisLevel.Skip(1).ToList();
    }

    public override string ToString() => $"{Header.Source.Tree.Name} ({Header.Source.Tree.AutomatedRecordId})";
}

internal class GedcomJsonConverter : JsonConverter<Gedcom>
{
    public override Gedcom? ReadJson(JsonReader reader, Type objectType, Gedcom? existingValue, bool hasExistingValue, JsonSerializer serializer) => throw new NotImplementedException();

    public override void WriteJson(JsonWriter writer, Gedcom? gedcom, JsonSerializer serializer)
    {
        if (gedcom == null) throw new ArgumentNullException(nameof(gedcom));

        var gedcomObject = new
        {
            Families = gedcom.GetFamilyRecords(),
            Individuals = gedcom.GetIndividualRecords(),
            Repositories = gedcom.GetRepositoryRecords(),
            Sources = gedcom.GetSourceRecords()
        };

        serializer.Serialize(writer, gedcomObject);
    }
}

#region LINEAGE_LINKED_GEDCOM p. 23
/*

LINEAGE_LINKED_GEDCOM:=

0 <<HEADER>> {1:1} p.23
0 <<SUBMISSION_RECORD>> {0:1} p.28
0 <<RECORD>> {1:M} p.24
0 TRLR {1:1}

This is a model of the lineage-linked GEDCOM structure for submitting data to other lineage-linked
GEDCOM processing systems. A header and a trailer record are required, and they can enclose any
number of data records. Tags from Appendix A (see page 83) must be used in the same context as
shown in the following form. User defined tags (see <NEW_TAG> on page 56) are discouraged but
when used must begin with an under-score. Tags that are required within a desired context have been
bolded. Note that some contexts are not required but if they are used then the bolded tags are
required.

*/
#endregion


#region Explanation of GetGedcomLinesForLevel()

/*
Explanation of List<List<GedcomLine>> GetGedcomLinesForLevel(int level, List<GedcomLine> gedcomLines)

Note: I will be indenting the lines by level in this example for readability, but no leading whitespace 
is allowed in a valid ged file.

A Gedcom file is made up of a series of lines that each have a level. Here is a small ged file
representing two individuals (INDI):

// gedcom lines
0 HEAD
0 @I0987654321@ INDI
	1 NAME John /Doe/
		2 GIVN John
		2 SURN Doe
	1 SEX M
0 @I1234567890@ INDI
	1 NAME Mary /Jane/
		2 GIVN Mary
		2 SURN Jane
	1 SEX F
0 TRLR

This function groups gedcom lines together logically into a tree structure of nested lists. For
example, calling GetGedcomLinesForLevel(0, <gedcom lines>) with the above file will create four lists 
(HEAD, INDI, INDI, TRLR). If I call GetGedcomLinesForLevel(1, <gedcom lines>) and pass one of the INDI 
records, I'll get two lists (NAME, SEX). If I call GetGedcomLinesForLevel(2, <gedcom lines>) and pass 
one of the NAME records, I'll get two lists (GIVN, SURN). And so on. The upper bound for level is 99.

*/

#endregion