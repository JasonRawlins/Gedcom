import { LdsOrdinanceStatus } from "./LdsOrdinanceStatus";
import { Note } from "./Note";
import { SourceCitation } from "./SourceCitation";

export interface LdsIndividualOrdinance {
  dateLdsOrdinance: string;
  ldsBaptismDateStatus: LdsOrdinanceStatus;
  noteStructures: Note[];
  placeLivingOrdinance: string;
  sourceCitations: SourceCitation[];
  templeCode: string;
}
