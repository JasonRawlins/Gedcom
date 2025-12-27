import { EventModel } from "./EventModel";

export interface IndividualModel {
  ancestryLink: string;
  automatedRecordId: string;
  birth: EventModel;
  death: EventModel;
  given: string;
  personalName: string;
  sexValue: string;
  submitter: string;
  surname: string;
  xref: string;
}
