import { Component, Input } from '@angular/core';
import { IndividualModel } from '../../view-models/IndividualModel';

@Component({
  selector: 'app-relationship-card',
  standalone: false,
  templateUrl: './relationship-card.component.html',
  styleUrl: './relationship-card.component.css'
})
export class RelationshipCardComponent {
  @Input() individual!: IndividualModel;
  @Input() isChild!: boolean;
}
