import { LdsOrdinanceStatus } from "./LdsOrdinanceStatus";
import { Note } from "./Note";
import { SourceCitation } from "./SourceCitation";

export interface LdsSpouseSealing {
  dateLdsOrdinance: string;
  ldsSpouseSealingDateStatus: LdsOrdinanceStatus;
  notes: Note[];
  placeLivingOrdinance: string;
  sourceCitations: SourceCitation[];
  templeCode: string;
}
