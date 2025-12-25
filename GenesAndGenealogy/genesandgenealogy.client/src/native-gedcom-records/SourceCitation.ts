import { EventTypeCitedFrom } from "./EventTypeCitedFrom";
import { MultimediaLink } from "./MultimediaLink";
import { NoteStructure } from "./NoteStructure";
import { SourceCitationData } from "./SourceCitationData";

export interface SourceCitation {
  certaintyAssessment: string;
  sourceCitationData: SourceCitationData;
  eventTypeCitedFrom: EventTypeCitedFrom;
  multimediaLinks: MultimediaLink[];
  noteStructures: NoteStructure[];
  whereWithinSource: string;
  xref: string;
}
