import { DateModel } from '../view-models/DateModel';

export interface SourceModel {
  automatedRecordId: string;
  callNumber: string;
  changeDate: DateModel;
  descriptiveTitle: string;
  filedByEntry: string;
  isEmpty: boolean;
  //multimediaLinks: MultimediaLink[];
  notes: string[];
  originator: string;
  publicationFacts: string;
  //recordData: SourceRecordData;
  //repositoryCitations: SourceRepositoryCitation[];
  repositoryXref: string;
  textFromSource: string;
  //userReferenceNumbers: UserReferenceNumber[];
  xref: string;
}
