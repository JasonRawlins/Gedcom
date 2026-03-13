// TODO: 2025-03-12 This code is probably redundant with NoteStructure. I cannot determine this right now.
// But there is a vary strong likelihood that it will be used in the future. I'm leaving it in so that
// I don't forget about it.

//using Gedcom.RecordStructures;

//namespace Gedcom.DTOs;

//public class NoteDto(NoteRecord noteRecord) : GedcomJson
//{
//    public string? AutomatedRecordId { get; set; } = JsonString(noteRecord.AutomatedRecordId);
//    public ChangeDateJson? ChangeDate { get; set; } = JsonRecord(new ChangeDateJson(noteRecord.ChangeDate));
//    public UserReferenceNumberJson? UserReferenceNumber { get; set; } = JsonRecord(new UserReferenceNumberJson(noteRecord.UserReferenceNumber));

//    public override string ToString() => $"{AutomatedRecordId}";
//}