import { Association } from "./Association";
import { ChangeDate } from "./ChangeDate";
import { ChildToFamilyLink } from "./ChildToFamilyLink";
import { IndividualAttribute } from "./IndividualAttributeStructure";
import { IndividualEvent } from "./IndividualEventStructure";
import { LdsIndividualOrdinance } from "./LdsIndividualOrdinance";
import { MultimediaLink } from "./MultimediaLink";
import { Note } from "./Note";
import { PersonalName } from "./PersonalName";
import { SourceCitation } from "./SourceCitation";
import { SpouseToFamilyLink } from "./SpouseToFamilyLink";
import { UserReferenceNumber } from "./UserReferenceNumber";

export interface Individual {
  aliases: string[];
  ancestorInterests: string[];
  ancestralFileNumber: string;
  associations: Association[];
  automatedRecordId: string;
  changeDate: ChangeDate;
  childToFamilyLinks: ChildToFamilyLink[];
  descendantInterests: string[];
  individualAttributeStructures: IndividualAttribute[];
  individualEventStructures: IndividualEvent[];
  ldsIndividualOrdinances: LdsIndividualOrdinance[];
  multimediaLinks: MultimediaLink[];
  notes: Note[];
  permanentRecordFileNumber: string;
  personalNames: PersonalName[];
  restrictionNotice: string;
  sex: string;
  sourceCitations: SourceCitation[];
  spouseToFamilyLinks: SpouseToFamilyLink[];
  submitter: string;
  userReferenceNumbers: UserReferenceNumber[];
  xref: string;
}
