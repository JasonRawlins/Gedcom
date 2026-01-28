import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { BehaviorSubject, Observable } from "rxjs";
import { tap } from "rxjs/operators";
import { ProfileModel } from "../../view-models/ProfileModel";

@Injectable({ providedIn: 'root' })
export class ProfileService {
  private readonly profileSubject = new BehaviorSubject<ProfileModel | null>(null);

  // Public observable for all components
  profile$: Observable<ProfileModel | null> = this.profileSubject.asObservable();

  constructor(private http: HttpClient) { }

  loadProfile(individualXref: string): Observable<ProfileModel> {
    return this.http
      .get<ProfileModel>(`/gedcom/profile/${individualXref}`)
      .pipe(
        tap(profile => this.profileSubject.next(profile))
      );
  }

  // Optional synchronous access (use sparingly)
  get currentProfile(): ProfileModel | null {
    return this.profileSubject.value;
  }
}
