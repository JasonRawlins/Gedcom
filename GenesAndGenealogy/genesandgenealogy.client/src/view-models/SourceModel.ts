import { DateModel } from '../view-models/DateModel';

export interface SourceModel {
  automatedRecordId: string;
  callNumber: string;
  changeDate: DateModel;
  //multimediaLinks: MultimediaLink[];
  notes: string[];
  title: string;
  filedByEntry: string;
  originator: string;
  publicationFacts: string;
  //recordData: SourceRecordData;
  //sourceRepositoryCitations: SourceRepositoryCitation[];
  textFromSource: string;
  //userReferenceNumbers: UserReferenceNumber[];
}
