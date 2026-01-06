import { EventModel } from "./EventModel";

export interface IndividualModel {
  ancestryLink: string;
  automatedRecordId: string;
  birth: EventModel;
  death: EventModel;
  events: EventModel[];
  given: string;
  personalName: string;
  sex: string;
  submitter: string;
  surname: string;
  xref: string;
}
