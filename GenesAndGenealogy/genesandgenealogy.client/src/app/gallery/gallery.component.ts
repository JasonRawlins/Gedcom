import { Component } from '@angular/core';
import { Observable } from 'rxjs';
import { ProfileModel } from '../../view-models/ProfileModel';
import { ProfileService } from '../services/profile.service';

@Component({
  selector: 'app-gallery',
  standalone: false,
  templateUrl: './gallery.component.html',
  styleUrl: './gallery.component.css'
})
export class GalleryComponent {
  profile$: Observable<ProfileModel | null>;

  constructor(private profileService: ProfileService) {
    this.profile$ = this.profileService.profile$;
  }
}
