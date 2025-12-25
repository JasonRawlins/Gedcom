import { ChangeDate } from "./ChangeDate";
import { UserReferenceNumber } from "./UserReferenceNumber";

export interface NoteRecord {
  automatedRecordId: string;
  changeDate: ChangeDate;
  userReferenceNumber: UserReferenceNumber;
}
