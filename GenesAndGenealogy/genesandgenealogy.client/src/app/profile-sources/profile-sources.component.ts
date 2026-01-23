import { Component, inject, Input } from '@angular/core';
import { GedcomService } from '../services/gedcom.service';
import { IndividualModel } from '../../view-models/IndividualModel';
import { SourceModel } from '../../view-models/SourceModel';

@Component({
  selector: 'app-profile-sources',
  standalone: false,
  templateUrl: './profile-sources.component.html',
  styleUrl: './profile-sources.component.css'
})
export class ProfileSourcesComponent {
  @Input() individual!: IndividualModel;
  @Input() sources!: SourceModel[];

  constructor(private gedcomService: GedcomService) {
  }
}
