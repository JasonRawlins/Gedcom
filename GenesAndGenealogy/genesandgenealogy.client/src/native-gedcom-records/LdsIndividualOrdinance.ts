import { LdsOrdinanceStatus } from "./LdsOrdinanceStatus";
import { NoteStructure } from "./NoteStructure";
import { SourceCitation } from "./SourceCitation";

export interface LdsIndividualOrdinance {
  dateLdsOrdinance: string;
  ldsBaptismDateStatus: LdsOrdinanceStatus;
  noteStructures: NoteStructure[];
  placeLivingOrdinance: string;
  sourceCitations: SourceCitation[];
  templeCode: string;
}
