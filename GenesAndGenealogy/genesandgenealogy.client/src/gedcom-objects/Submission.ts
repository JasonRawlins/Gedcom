import { ChangeDate } from "./ChangeDate";
import { Note } from "./Note";

export interface Submission {
  automatedRecordId: string;
  changeDate: ChangeDate;
  generationsOfAncestors: string;
  generationsOfDescendants: string;
  nameOfFamilyFile: string;
  notes: Note[];
  ordinanceProcessFlag: string;
  submitter: string;
  templeCode: string;
  xref: string;
}
