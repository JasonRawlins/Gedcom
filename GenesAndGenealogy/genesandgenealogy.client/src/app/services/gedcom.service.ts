import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { FamilyModel } from "../../view-models/FamilyModel";
import { IndividualModel } from "../../view-models/IndividualModel";
import { ProfileModel } from "../../view-models/ProfileModel";
import { RepositoryModel } from "../../view-models/RepositoryModel";
import { SourceModel } from "../../view-models/SourceModel";
import { TreeModel } from "../../view-models/TreeModel";

@Injectable()
export class GedcomService {
  
  constructor(private http: HttpClient) { }

  getFamily(famXref: string) {
    return this.http.get<FamilyModel>(`/gedcom/family/${famXref}`);
  }

  getIndividualFamilies(individualXref: string) {
    return this.http.get<FamilyModel[]>(`/gedcom/individual/${individualXref}/families`);
  }

  getIndividual(individualXref: string) {
    return this.http.get<IndividualModel>(`/gedcom/individual/${individualXref}`);
  }

  getIndividuals() {
    return this.http.get<IndividualModel[]>("/gedcom/individuals");
  }

  getProfile(individualXref: string) {
    return this.http.get<ProfileModel>(`/gedcom/profile/${individualXref}`);
  }

  getRepository(repositoryXref: string) {
    return this.http.get<RepositoryModel>(`/gedcom/repository/${repositoryXref}`);
  }

  getSource(sourceXref: string) {
    return this.http.get<SourceModel>(`/gedcom/source/${sourceXref}`);
  }

  getTree() {
    return this.http.get<TreeModel>(`/gedcom/tree`);
  }
}
