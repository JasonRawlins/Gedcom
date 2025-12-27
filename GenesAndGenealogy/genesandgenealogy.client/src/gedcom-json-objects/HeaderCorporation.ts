import { Address } from "./Address";

export interface HeaderCorporation {
  address: Address;
  emails: string[];
  faxNumbers: string[];
  phoneNumbers: string[];
  webPages: string[];
}
