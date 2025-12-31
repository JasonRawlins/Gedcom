import { Component, Input } from '@angular/core';
import { IndividualModel } from '../../view-models/IndividualModel';

@Component({
  selector: 'app-profile-header',
  standalone: false,
  templateUrl: './profile-header.component.html',
  styleUrl: './profile-header.component.css'
})
export class ProfileHeaderComponent {
  @Input() individual!: IndividualModel;
}
