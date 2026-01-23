import { DateModel } from '../view-models/DateModel';
import { NoteModel } from './NoteModel';
import { PlaceModel } from '../view-models/PlaceModel';

export interface EventModel {
  //address: Address;
  ageAtEvent: string;
  causeOfEvent: string;
  eventOrFactClassification: string;
  gedcomDate: DateModel;
  name: string;
  noteStructures: NoteModel[];
  //multimediaLinks: MultimediaLinkModel[];
  placeStructure: PlaceModel;
  religiousAffiliation: string;
  responsibleAgency: string;
  restrictionNotice: string;
  //sourceCitations: SourceCitation[];
  type: EventModelType;
}

export enum EventModelType {
  Family,
  Individual
}

