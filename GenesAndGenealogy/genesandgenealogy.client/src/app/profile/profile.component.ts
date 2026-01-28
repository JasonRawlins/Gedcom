import { Component, inject, Input, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs';
import { GedcomService } from '../services/gedcom.service';
import { ProfileModel } from '../../view-models/ProfileModel';
import { ProfileService } from '../services/profile.service';

@Component({
  selector: 'app-profile',
  standalone: false,
  templateUrl: './profile.component.html',
  styleUrl: './profile.component.css'
})
export class ProfileComponent implements OnInit {
  private activatedRoute = inject(ActivatedRoute);
  profile$!: Observable<ProfileModel | null>;

  constructor(private gedcomService: GedcomService, private profileService: ProfileService) {
  }

  ngOnInit() {
    this.profile$ = this.profileService.profile$;

    const xref = this.activatedRoute.snapshot.paramMap.get('individualXref');
    if (xref) {
      this.profileService.loadProfile(xref).subscribe();
    }

    //this.activatedRoute.params.subscribe(params => {
    //  const individualXref = params['individualXref'];

    //  this.gedcomService.getProfile(individualXref).subscribe(
    //    (profile) => {
    //      this.profile = profile;
    //    },
    //    (error) => {
    //      console.error(error);
    //    });
    //});
  }
}
