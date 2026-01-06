import { Component, Input } from '@angular/core';
import { EventModel } from '../../view-models/EventModel';

@Component({
  selector: 'app-timeline',
  standalone: false,
  templateUrl: './timeline.component.html',
  styleUrl: './timeline.component.css'
})
export class TimelineComponent {
  @Input() events!: EventModel[];

}
