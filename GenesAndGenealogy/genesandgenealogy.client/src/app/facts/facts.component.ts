import { Component, Input } from '@angular/core';
import { IndividualModel } from '../../view-models/IndividualModel';
import { SourceModel } from '../../view-models/SourceModel';

@Component({
  selector: 'app-facts',
  standalone: false,
  templateUrl: './facts.component.html',
  styleUrl: './facts.component.css'
})
export class FactsComponent {
  @Input() individual!: IndividualModel;
  @Input() sources!: SourceModel[];
}
