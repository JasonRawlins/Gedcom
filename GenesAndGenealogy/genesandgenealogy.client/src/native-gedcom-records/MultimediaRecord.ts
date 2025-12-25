import { ChangeDate } from "./ChangeDate";
import { MultimediaFormat } from "./MultimediaFormat";
import { NoteStructure } from "./NoteStructure";
import { SourceCitation } from "./SourceCitation";
import { UserReferenceNumber } from "./UserReferenceNumber";

export interface MultimediaRecord {
  automatedRecordId: string;
  changeDate: ChangeDate;
  descriptiveTitle: string;
  multimediaFileReferenceNumbers: string[];
  multimediaFormat: MultimediaFormat;
  noteStructures: NoteStructure[];
  sourceCitations: SourceCitation[];
  userReferenceNumber: UserReferenceNumber;
}
