import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { FamilyModel } from "../view-models/FamilyModel";
import { IndividualModel } from "../view-models/IndividualModel";
import { ProfileModel } from "../view-models/ProfileModel";


@Injectable()
export class GedcomService {
  
  constructor(private http: HttpClient) { }

  getIndividuals() {
    return this.http.get<IndividualModel[]>("/gedcom/individuals");
  }

  getProfile(individualXref: string) {
    return this.http.get<ProfileModel>(`/gedcom/profile/${individualXref}`);
  }

  getIndividualFamilies(individualXref: string) {
    return this.http.get<FamilyModel[]>(`/gedcom/individual/${individualXref}/families`);
  }

  getFamily(famXref: string) {
    return this.http.get<FamilyModel>(`/gedcom/family/${famXref}`);
  }
}
