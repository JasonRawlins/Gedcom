using System.Diagnostics;
using System.Text;
using Gedcom.RecordStructures;
using Newtonsoft.Json;

namespace Gedcom;

// The Gedcom Standard 5.1.1 documentation is at the end of this file.
[JsonConverter(typeof(GedcomJsonConverter))]
public class Gedcom : RecordStructureBase
{
    public Gedcom(List<GedcomLine> gedcomLines)
    {
        // Level 0 records are the top-level records. They are
        // FAM (Family),
        // INDI (Individual),
        // OBJE (Multimedia),
        // NOTE (Note),
        // REPO (Repository),
        // SOUR (Source),
        // SUBM (Submitter)
        foreach (var level0Record in GetGedcomLinesForLevel(0, gedcomLines))
        {
            Record.Records.Add(new Record(level0Record));
        }
    }

    public Header Header => First<Header>(C.HEAD);
   
    public FamilyRecord GetFamilyRecord(string xrefFAM) => new(Record.Records.First(r => r.Tag.Equals(C.FAM) && r.Value.Equals(xrefFAM)));
    public List<FamilyRecord> GetFamilyRecords() => Record.Records.Where(r => r.Tag.Equals(C.FAM)).Select(r => new FamilyRecord(r)).ToList();

    public IndividualRecord GetIndividualRecord(string xref, string query)
    {
        var individualRecord = Single(r => 
            r.Tag.Equals(C.INDI) 
            && r.Value.Equals(xref)
            && r.IsQueryMatch(query));

        if (individualRecord.IsEmpty)
        {
            return Empty<IndividualRecord>();
        }

        return new IndividualRecord(individualRecord);
    }

    public List<IndividualRecord> GetIndividualRecords() => GetIndividualRecords("");
    public List<IndividualRecord> GetIndividualRecords(string query) => 
        Record.Records.Where(r => r.Tag.Equals(C.INDI) && r.IsQueryMatch(query)).Select(r => new IndividualRecord(r)).ToList();
    
    public MultimediaRecord GetMultimediaRecord(string xref) => new(Record.Records.First(r => r.Tag.Equals(C.OBJE) && r.Value.Equals(xref)));
    public List<MultimediaRecord> GetMultimediaRecords() => Record.Records.Where(r => r.Tag.Equals(C.OBJE)).Select(r => new MultimediaRecord(r)).ToList();

    public NoteRecord GetNoteRecord(string xref) => new(Record.Records.First(r => r.Tag.Equals(C.NOTE) && r.Value.Equals(xref)));
    public List<NoteRecord> GetNoteRecords() => Record.Records.Where(r => r.Tag.Equals(C.NOTE)).Select(r => new NoteRecord(r)).ToList();
    
    public RepositoryRecord GetRepositoryRecord(string xref) => new(Record.Records.First(r => r.Tag.Equals(C.REPO) && r.Value.Equals(xref)));
    public List<RepositoryRecord> GetRepositoryRecords() => Record.Records.Where(r => r.Tag.Equals(C.REPO)).Select(r => new RepositoryRecord(r)).ToList();
   
    public SourceRecord GetSourceRecord(string xref) => new(Record.Records.First(r => r.Tag.Equals(C.SOUR) && r.Value.Equals(xref)));
    public List<SourceRecord> GetSourceRecords() => Record.Records.Where(r => r.Tag.Equals(C.SOUR)).Select(r => new SourceRecord(r)).ToList();

    public SubmitterRecord GetSubmitterRecord(string xref) => new(Record.Records.First(r => r.Tag.Equals(C.SUBM) && r.Value.Equals(xref)));
    public List<SubmitterRecord> GetSubmitterRecords() => Record.Records.Where(r => r.Tag.Equals(C.SUBM)).Select(r => new SubmitterRecord(r)).ToList();

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
            //Families = gedcom.GetFamilyRecords(),
            //Individuals = gedcom.GetIndividualRecords(),
            //Repositories = gedcom.GetRepositoryRecords(),
            Sources = gedcom.GetSourceRecords()
        };

        var stopwatch = Stopwatch.StartNew();
        serializer.Serialize(writer, gedcomObject);
        stopwatch.Stop();
        var elaspsedMilliseconds = stopwatch.ElapsedMilliseconds;
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


#region 

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
one of the NAME records, I'll get two lists (GIVN, SURN). And so on. I think 99 levels is the 
official limit. 

*/

#endregion