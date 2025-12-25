import { ChangeDate } from "./ChangeDate";
import { MultimediaLink } from "./MultimediaLink";
import { NoteStructure } from "./NoteStructure";
import { SourceRecordData } from "./SourceRecordData";
import { SourceRepositoryCitation } from "./SourceRepositoryCitation";
import { UserReferenceNumber } from "./UserReferenceNumber";

export interface SourceRecord {
  automatedRecordId: string;
  callNumber: string;
  changeDate: ChangeDate;
  multimediaLinks: MultimediaLink[];
  noteStructures: NoteStructure[];
  sourceDescriptiveTitle: NoteStructure[];
  sourceFiledByEntry: NoteStructure[];
  sourceOriginator: NoteStructure[];
  sourcePublicationFacts: NoteStructure[];
  sourceRecordData: SourceRecordData;
  sourceRepositoryCitations: SourceRepositoryCitation;
  textFromSource: NoteStructure;
  userReferenceNumbers: UserReferenceNumber[];
  xref: string;
}
