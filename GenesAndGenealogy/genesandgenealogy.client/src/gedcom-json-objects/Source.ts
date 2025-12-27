import { ChangeDate } from "./ChangeDate";
import { MultimediaLink } from "./MultimediaLink";
import { Note } from "./Note";
import { SourceRecordData } from "./SourceRecordData";
import { SourceRepositoryCitation } from "./SourceRepositoryCitation";
import { UserReferenceNumber } from "./UserReferenceNumber";

export interface Source {
  automatedRecordId: string;
  callNumber: string;
  changeDate: ChangeDate;
  multimediaLinks: MultimediaLink[];
  notes: Note[];
  sourceDescriptiveTitle: Note[];
  filedByEntry: Note[];
  originator: Note[];
  publicationFacts: Note[];
  recordData: SourceRecordData;
  repositoryCitations: SourceRepositoryCitation;
  textFromSource: Note;
  userReferenceNumbers: UserReferenceNumber[];
  xref: string;
}
