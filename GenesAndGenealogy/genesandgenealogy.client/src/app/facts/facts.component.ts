import { Component, Input, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { ProfileModel } from '../../view-models/ProfileModel';
import { ProfileService } from '../services/profile.service';

@Component({
  selector: 'app-facts',
  standalone: false,
  templateUrl: './facts.component.html',
  styleUrl: './facts.component.css'
})
export class FactsComponent {
  profile$: Observable<ProfileModel | null>;

  constructor(private profileService: ProfileService) {
    this.profile$ = this.profileService.profile$;
  }
}
