import { Component, inject, Input } from '@angular/core';
import { ProfileModel } from '../../view-models/ProfileModel';
import { ActivatedRoute } from '@angular/router';
import { GedcomService } from '../gedcom.service';

@Component({
  selector: 'app-profile',
  standalone: false,
  templateUrl: './profile.component.html',
  styleUrl: './profile.component.css'
})
export class ProfileComponent {
  @Input() profile!: ProfileModel;
  private activatedRoute = inject(ActivatedRoute);

  constructor(private gedcomService: GedcomService) {
  }

  ngOnInit() {
    this.activatedRoute.params.subscribe((params) => {
      const individualXref = params['individualXref'];

      this.gedcomService.getProfile(individualXref).subscribe(
        (profile) => {
          this.profile = profile;
        },
        (error) => {
          console.error(error);
        });
    });
  }
}
