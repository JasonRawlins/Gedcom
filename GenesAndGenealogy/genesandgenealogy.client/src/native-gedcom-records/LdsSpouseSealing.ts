import { LdsOrdinanceStatus } from "./LdsOrdinanceStatus";
import { NoteStructure } from "./NoteStructure";
import { SourceCitation } from "./SourceCitation";

export interface LdsSpouseSealing {
  dateLdsOrdinance: string;
  ldsSpouseSealingDateStatus: LdsOrdinanceStatus;
  noteStructures: NoteStructure[];
  placeLivingOrdinance: string;
  sourceCitations: SourceCitation[];
  templeCode: string;
}
