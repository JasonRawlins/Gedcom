import { ChangeDate } from "./ChangeDate";
import { FamilyEvent } from "./FamilyEvent";
import { MultimediaLink } from "./MultimediaLink";
import { Note } from "./Note";
import { SourceCitation } from "./SourceCitation";
import { UserReferenceNumber } from "./UserReferenceNumber";

export interface Family {
  adoptedByWhichParent: string;
  automatedRecordNumber: string;
  changeDate: ChangeDate;
  children: string[];
  countOfChildren: string;
  divorce: FamilyEvent;
  familyEventStructures: FamilyEvent[];
  husband: string;
  // +1 <<LDS_SPOUSE_SEALING>> {0:M} p.36
  marriage: FamilyEvent;
  multimediaLinks: MultimediaLink[];
  notes: Note[];
  restrictionNotice: string;
  sourceCitations: SourceCitation[];
  submitter: string;
  userReferenceNumbers: UserReferenceNumber[];
  wife: string;
  xref: string;
}
