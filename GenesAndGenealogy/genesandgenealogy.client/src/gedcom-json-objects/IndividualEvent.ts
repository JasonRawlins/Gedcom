import { EventDetail } from "./EventDetail";

// The IndividualEvent and FamilyEvent are treated almost identically
// in code. The only difference is that an IndividualEvent as the ageAtEvent
// property. 
export interface IndividualEvent extends EventDetail {
  ageAtEvent: string;
}
