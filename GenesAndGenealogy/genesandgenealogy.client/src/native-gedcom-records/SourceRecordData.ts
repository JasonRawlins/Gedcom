import { NoteStructure } from "./NoteStructure";
import { SourceRecordEvent } from "./SourceRecordEvent";

export interface SourceRecordData {
  eventsRecorded: SourceRecordEvent[];
  noteStructures: NoteStructure[];
  responsibleAgency: string;

}
