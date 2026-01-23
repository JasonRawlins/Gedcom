import { EventModel } from "./EventModel";
import { IndividualModel } from "./IndividualModel";

export interface FamilyModel {
  children: IndividualModel[];
  events: EventModel[];
  husband: IndividualModel;
  wife: IndividualModel;
  xref: string;
}
