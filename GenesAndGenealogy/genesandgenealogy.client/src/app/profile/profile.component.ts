import { Component, Input } from '@angular/core';
import { ProfileModel } from '../../view-models/ProfileModel';

@Component({
  selector: 'app-profile',
  standalone: false,
  templateUrl: './profile.component.html',
  styleUrl: './profile.component.css'
})
export class ProfileComponent {
  @Input() profile!: ProfileModel;
}
