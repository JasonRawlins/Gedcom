import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { ProfileModel } from "../../view-models/ProfileModel";

@Injectable({ providedIn: 'root' })
export class ProfileService {
  private profile!: ProfileModel;

  constructor(private http: HttpClient) { }

  getProfile(individualXref: string) {
    this.http.get<ProfileModel>(`/gedcom/profile/${individualXref}`).subscribe(
      (profile) => {
        this.profile = profile;
      },
      (error) => {
        console.error(error);
      }
    );

    return this.profile;
  }
}
