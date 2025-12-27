import { EventTypeCitedFrom } from "./EventTypeCitedFrom";
import { MultimediaLink } from "./MultimediaLink";
import { Note } from "./Note";
import { SourceCitationData } from "./SourceCitationData";

export interface SourceCitation {
  certaintyAssessment: string;
  data: SourceCitationData;
  eventTypeCitedFrom: EventTypeCitedFrom;
  multimediaLinks: MultimediaLink[];
  notes: Note[];
  whereWithinSource: string;
  xref: string;
}
