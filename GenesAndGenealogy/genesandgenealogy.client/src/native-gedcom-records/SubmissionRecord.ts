import { ChangeDate } from "./ChangeDate";
import { NoteStructure } from "./NoteStructure";

export interface SubmissionRecord {
  submitter: string;
  nameOfFamilyFile: string;
  templeCode: string;
  generationsOfAncestors: string;
  generationsOfDescendants: string;
  ordinanceProcessFlag: string;
  automatedRecordId: string;
  noteStructures: NoteStructure[];
  changeDate: ChangeDate;
  xref: string;
}
