import { Component, inject, Input } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { GedcomService } from '../services/gedcom.service';
import { ProfileModel } from '../../view-models/ProfileModel';

@Component({
  selector: 'app-profile',
  standalone: false,
  templateUrl: './profile.component.html',
  styleUrl: './profile.component.css'
})
export class ProfileComponent {
  private activatedRoute = inject(ActivatedRoute);
  @Input() profile!: ProfileModel;

  constructor(private gedcomService: GedcomService) {
  }

  ngOnInit() {
    this.activatedRoute.params.subscribe(params => {
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
