import { Component, Input } from '@angular/core';
import { SourceModel } from '../../view-models/SourceModel';

@Component({
  selector: 'app-profile-sources',
  standalone: false,
  templateUrl: './profile-sources.component.html',
  styleUrl: './profile-sources.component.css'
})
export class ProfileSourcesComponent {
  @Input() sources!: SourceModel[]
}
