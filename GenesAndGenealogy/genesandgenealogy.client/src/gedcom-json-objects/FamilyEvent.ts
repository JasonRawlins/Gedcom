import { EventDetail } from './EventDetail';

// The IndividualEvent and FamilyEvent are identical execpt that the
// IndividualEvent has the ageAtEvent property. This is how they are represented
// in the specification. However, in code, I'm am treating them both identically
// and so they have to have the same interface. That is why the FamilyEvent
// extends the EventDetail interface but adds no additional properties. It feels
// kind of hack-y but I'm going with it. 
export interface FamilyEvent extends EventDetail {
}
