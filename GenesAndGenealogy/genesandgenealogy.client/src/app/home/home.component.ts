import { Component, Input } from '@angular/core';
import { GedcomService } from '../services/gedcom.service';
import { ProfileModel } from '../../view-models/ProfileModel';

@Component({
  selector: 'app-home',
  standalone: false,
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent {
  @Input() profile!: ProfileModel;

  constructor(private gedcomService: GedcomService) {
  }

  ngOnInit() {
    this.gedcomService.getProfile("@I272718948910@").subscribe(
      (profile) => {
        this.profile = profile;
      },
      (error) => {
        console.error(error);
      });
  }
}
