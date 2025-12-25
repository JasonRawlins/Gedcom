import { AssociationStructure } from "./AssociationStructure";
import { ChangeDate } from "./ChangeDate";
import { ChildToFamilyLink } from "./ChildToFamilyLink";
import { IndividualAttributeStructure } from "./IndividualAttributeStructure";
import { IndividualEventStructure } from "./IndividualEventStructure";
import { LdsIndividualOrdinance } from "./LdsIndividualOrdinance";
import { MultimediaLink } from "./MultimediaLink";
import { NoteStructure } from "./NoteStructure";
import { PersonalNameStructure } from "./PersonalNameStructure";
import { SourceCitation } from "./SourceCitation";
import { SpouseToFamilyLink } from "./SpouseToFamilyLink";
import { UserReferenceNumber } from "./UserReferenceNumber";

export interface IndividualRecord {
  aliases: string[];
  ancestorInterests: string[];
  ancestralFileNumber: string;
  associationStructures: AssociationStructure[];
  automatedRecordId: string;
  changeDate: ChangeDate;
  childToFamilyLinks: ChildToFamilyLink[];
  descendantInterests: string[];
  individualAttributeStructures: IndividualAttributeStructure[];
  individualEventStructures: IndividualEventStructure[];
  ldsIndividualOrdinances: LdsIndividualOrdinance[];
  multimediaLinks: MultimediaLink[];
  noteStructures: NoteStructure[];
  permanentRecordFileNumber: string;
  personalNameStructures: PersonalNameStructure[];
  restrictionNotice: string;
  sexValue: string;
  sourceCitations: SourceCitation[];
  spouseToFamilyLinks: SpouseToFamilyLink[];
  submitter: string;
  userReferenceNumbers: UserReferenceNumber[];
  xref: string;
}
