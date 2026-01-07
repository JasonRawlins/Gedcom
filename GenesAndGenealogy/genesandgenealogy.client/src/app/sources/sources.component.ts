import { Component, Input } from '@angular/core';
import { SourceModel } from '../../view-models/SourceModel';

@Component({
  selector: 'app-sources',
  standalone: false,
  templateUrl: './sources.component.html',
  styleUrl: './sources.component.css'
})
export class SourcesComponent {
  @Input() sources!: SourceModel[]
}
