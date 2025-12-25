import { AddressStructure } from "./AddressStructure";
import { GedcomDate } from "./GedcomDate";
import { MultimediaLink } from "./MultimediaLink";
import { NoteStructure } from "./NoteStructure";
import { PlaceStructure } from "./PlaceStructure";
import { SourceCitation } from "./SourceCitation";

export interface EventDetail {
  addressStructure: AddressStructure;
  causeOfEvent: string;
  dateValuev: string;
  eventOrFactClassification: string;
  gedcomDate: GedcomDate;
  multimediaLinks: MultimediaLink[];
  noteStructures: NoteStructure[];
  placeStructure: PlaceStructure;
  religiousAffiliation: string;
  responsibleAgency: string;
  restrictionNotice: string;
  sourceCitations: SourceCitation[];
}
