import { Component, inject, Input } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { GedcomService } from '../services/gedcom.service';
import { SourceModel } from '../../view-models/SourceModel';

@Component({
  selector: 'app-source-information',
  standalone: false,
  templateUrl: './source-information.component.html',
  styleUrl: './source-information.component.css'
})
export class SourceInformationComponent {
  private activatedRoute = inject(ActivatedRoute);
  @Input() source!: SourceModel;

  constructor(private gedcomService: GedcomService) {
  }

  ngOnInit() {
    this.activatedRoute.params.subscribe(params => {
      const sourceXref = params['sourceXref'];

      this.gedcomService.getSource(sourceXref).subscribe(
        (source) => {
          this.source = source;
        },
        (error) => {
          console.error(error);
        }
      )
    });
  }
}
