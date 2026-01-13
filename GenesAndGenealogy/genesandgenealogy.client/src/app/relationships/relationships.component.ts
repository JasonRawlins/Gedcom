import { Component, Input } from '@angular/core';
import { FamilyModel } from '../../view-models/FamilyModel';

@Component({
  selector: 'app-relationships',
  standalone: false,
  templateUrl: './relationships.component.html',
  styleUrl: './relationships.component.css'
})
export class RelationshipsComponent {
  @Input() families!: FamilyModel[];
  @Input() individualXref!: string;
  @Input() parents!: FamilyModel;
}
