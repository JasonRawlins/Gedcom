import { AddressStructure } from "./AddressStructure";
import { CallNumber } from "./CallNumber";
import { ChangeDate } from "./ChangeDate";
import { NoteStructure } from "./NoteStructure";
import { UserReferenceNumber } from "./UserReferenceNumber";

export interface RepositoryRecord {
  addressEmails: string[];
  addressFaxNumbers: string[];
  addressStructure: AddressStructure;
  addressWebPages: string[];
  automatedRecordId: string;
  callNumber: CallNumber;
  changeDate: ChangeDate;
  noteStructures: NoteStructure;
  phoneNumbers: string[];
  userReferenceNumber: UserReferenceNumber;
  xref: string;
}
