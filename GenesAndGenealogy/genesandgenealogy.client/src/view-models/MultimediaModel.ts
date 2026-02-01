import { FileModel } from './FileModel';
import { NoteModel } from './NoteModel';
import { PlaceModel } from './PlaceModel';

export interface MultimediaModel {
  automatedRecordId: string;
  //ChangeDate: ChangeDateJson;
  date: string;
  description: string;
  descriptiveTitle: string;
  file: FileModel;
  multimediaFileReferenceNumbers: string[];
  //MultimediaFormat: MultimediaFormatJson;
  notes: NoteModel[];
  objectId: string;
  place: PlaceModel;
  //sourceCitations: SourceCitationJson[];
  //userReferenceNumber: UserReferenceNumberJson;
}

