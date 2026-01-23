import { EventModel } from "./EventModel";

export interface IndividualModel {
  ancestryLink: string;
  automatedRecordId: string;
  birth: EventModel;
  death: EventModel;
  events: EventModel[];
  fullName: string;
  given: string;
  isEmpty: boolean;
  personalName: string;
  sex: string;
  submitter: string;
  surname: string;
  xref: string;
  xrefNumber: string;
}
