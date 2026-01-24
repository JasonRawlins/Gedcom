import { Component, inject, Input } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { GedcomService } from '../services/gedcom.service';
import { IndividualModel } from '../../view-models/IndividualModel';
import { RepositoryModel } from '../../view-models/RepositoryModel';
import { SourceModel } from '../../view-models/SourceModel';
import { switchMap } from 'rxjs';

@Component({
  selector: 'app-source-information',
  standalone: false,
  templateUrl: './source-information.component.html',
  styleUrl: './source-information.component.css'
})
export class SourceInformationComponent {
  private activatedRoute = inject(ActivatedRoute);
  @Input() individual!: IndividualModel;
  @Input() repository!: RepositoryModel;
  @Input() source!: SourceModel;

  constructor(private gedcomService: GedcomService) {
  }

  ngOnInit() {
    this.activatedRoute.params.subscribe(params => {
      const sourceXref = params['sourceXref'];

      this.gedcomService.getSource(sourceXref).pipe(
        switchMap((source) => {
          this.source = source;
          return this.gedcomService.getRepository(source.repositoryXref);
        })
      ).subscribe(repository => {
          this.repository = repository;
        }
      );
    });

    this.activatedRoute.queryParams.subscribe((params) => {
      const individualXref = params['individualXref'];

      this.gedcomService.getIndividual(individualXref).subscribe(
        (individual) => {
          this.individual = individual;
        },
        (error) => {
          console.error(error);
        }
      );
    });
  }
}
