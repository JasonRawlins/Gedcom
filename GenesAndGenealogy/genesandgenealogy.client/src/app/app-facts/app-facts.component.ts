import { Component, Input } from '@angular/core';
import { IndividualModel } from '../../view-models/IndividualModel';

@Component({
  selector: 'app-facts',
  standalone: false,
  templateUrl: './app-facts.component.html',
  styleUrl: './app-facts.component.css'
})
export class AppFactsComponent {
  @Input() individual!: IndividualModel;

}
