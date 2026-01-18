import { Component, Input } from '@angular/core';
import { IndividualModel } from '../../view-models/IndividualModel';
import { GedcomService } from '../services/gedcom.service';

@Component({
  selector: 'app-tree',
  standalone: false,
  templateUrl: './tree.component.html',
  styleUrl: './tree.component.css',
  providers: [GedcomService]
})
export class TreeComponent {
  @Input() individuals!: IndividualModel[];

  constructor(private gedcomService: GedcomService) {
  }

  ngOnInit() {
    this.getIndividuals();
  }

  getIndividuals() {
    this.gedcomService.getIndividuals().subscribe(
      (individualModels) => {
        this.individuals = individualModels;
      },
      (error) => {
        console.error(error);
      });
  }
}
