import { Component, Input } from '@angular/core';
import { MultimediaModel } from '../../view-models/MultimediaModel';

@Component({
  selector: 'app-multimedia-item',
  standalone: false,
  templateUrl: './multimedia-item.component.html',
  styleUrl: './multimedia-item.component.css'
})
export class MultimediaItemComponent {
  @Input() multimediaItem: MultimediaModel | null = null;
}
