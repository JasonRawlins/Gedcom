import { Note } from "./Note";
import { SourceRecordEvent } from "./SourceRecordEvent";

export interface SourceRecordData {
  eventsRecorded: SourceRecordEvent[];
  notes: Note[];
  responsibleAgency: string;
}
