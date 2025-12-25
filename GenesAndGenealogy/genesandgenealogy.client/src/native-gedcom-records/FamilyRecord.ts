import { ChangeDate } from "./ChangeDate";
import { FamilyEventStructure } from "./FamilyEventStructure";
import { MultimediaLink } from "./MultimediaLink";
import { NoteStructure } from "./NoteStructure";
import { SourceCitation } from "./SourceCitation";
import { UserReferenceNumber } from "./UserReferenceNumber";

export interface FamilyRecord {
  adoptedByWhichParent: string;
  automatedRecordNumber: string;
  changeDate: ChangeDate;
  children: string[];
  countOfChildren: string;
  familyEventStructures: FamilyEventStructure[];
  husband: string;
  // +1 <<LDS_SPOUSE_SEALING>> {0:M} p.36
  multimediaLinks: MultimediaLink[];
  noteStructures: NoteStructure[];
  restrictionNotice: string;
  sourceCitations: SourceCitation[];
  submitter: string;
  userReferenceNumbers: UserReferenceNumber[];
  wife: string;
  xref: string;
}
