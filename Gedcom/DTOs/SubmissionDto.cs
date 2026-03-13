using Gedcom.RecordStructures;

namespace Gedcom.DTOs;

public class SubmissionDto(SubmissionRecord submissionRecord) : GedcomDto
{
    public string? AutomatedRecordId { get; set; } = GetString(submissionRecord.AutomatedRecordId);
    public ChangeDateDto? ChangeDate { get; set; } = GetRecord(new ChangeDateDto(submissionRecord.ChangeDate));
    public string? GenerationsOfAncestors { get; set; } = GetString(submissionRecord.GenerationsOfAncestors);
    public string? GenerationsOfDescendants { get; set; } = GetString(submissionRecord.GenerationsOfDescendants);
    public string? NameOfFamilyFile { get; set; } = GetString(submissionRecord.NameOfFamilyFile);
    public List<NoteDto>? Notes { get; set; } = GedcomDto.GetList<NoteDto>(submissionRecord.NoteStructures.Select(ns => new NoteDto(ns)).ToList());
    public string? OrdinanceProcessFlag { get; set; } = GetString(submissionRecord.OrdinanceProcessFlag);
    public string? Submitter { get; set; } = GetString(submissionRecord.Submitter);
    public string? TempleCode { get; set; } = GetString(submissionRecord.TempleCode);
    public string? Xref { get; set; } = submissionRecord.Xref;

    public override string ToString() => $"{Submitter}";
}
