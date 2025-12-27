import { ChangeDate } from "./ChangeDate";
import { MultimediaFormat } from "./MultimediaFormat";
import { Note } from "./Note";
import { SourceCitation } from "./SourceCitation";
import { UserReferenceNumber } from "./UserReferenceNumber";

export interface Multimedia {
  automatedRecordId: string;
  changeDate: ChangeDate;
  descriptiveTitle: string;
  multimediaFileReferenceNumbers: string[];
  multimediaFormat: MultimediaFormat;
  notes: Note[];
  sourceCitations: SourceCitation[];
  userReferenceNumber: UserReferenceNumber;
}
