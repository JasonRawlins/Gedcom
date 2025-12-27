import { Address } from "./Address";
import { GedcomDate } from "./GedcomDate";
import { MultimediaLink } from "./MultimediaLink";
import { Note } from "./Note";

export interface Submitter {
  address: Address;
  automatedRecordId: string;
  changeDate: GedcomDate;
  languagePreferences: string[];
  multimediaLinks: MultimediaLink[];
  notes: Note[];
  submitterName: string;
  submitterRegisteredReferenceNumber: string;
}
