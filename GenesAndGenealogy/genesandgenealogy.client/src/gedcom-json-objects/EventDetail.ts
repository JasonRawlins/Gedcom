import { Address } from "./Address";
import { GedcomDate } from "./GedcomDate";
import { MultimediaLink } from "./MultimediaLink";
import { Note } from "./Note";
import { Place } from "./Place";
import { SourceCitation } from "./SourceCitation";

export interface EventDetail {
  address: Address;
  causeOfEvent: string;
  dateValue: string;
  eventOrFactClassification: string;
  gedcomDate: GedcomDate;
  multimediaLinks: MultimediaLink[];
  noteStructures: Note[];
  placeStructure: Place;
  religiousAffiliation: string;
  responsibleAgency: string;
  restrictionNotice: string;
  sourceCitations: SourceCitation[];
}
