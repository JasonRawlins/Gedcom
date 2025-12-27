import { Address } from "./Address";
import { CallNumber } from "./CallNumber";
import { ChangeDate } from "./ChangeDate";
import { Note } from "./Note";
import { UserReferenceNumber } from "./UserReferenceNumber";

export interface Repository {
  automatedRecordId: string;
  callNumber: CallNumber;
  changeDate: ChangeDate;
  emails: string[];
  faxNumbers: string[];
  notes: Note;
  phoneNumbers: string[];
  structure: Address;
  userReferenceNumber: UserReferenceNumber;
  webPages: string[];
  xref: string;
}
