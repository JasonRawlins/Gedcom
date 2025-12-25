import { AddressStructure } from "./AddressStructure";

export interface HeaderCorporation {
  addressEmails: string[];
  addressFaxNumbers: string[];
  addressStructure: AddressStructure;
  addressWebPages: string[];
  phoneNumbers: string[];
}
