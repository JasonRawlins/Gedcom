import { Component, Input } from '@angular/core';
import { IndividualModel } from '../../view-models/IndividualModel';

@Component({
  selector: 'app-tree',
  standalone: false,
  templateUrl: './tree.component.html',
  styleUrl: './tree.component.css'
})
export class TreeComponent {
  @Input() individuals!: IndividualModel[];
}
